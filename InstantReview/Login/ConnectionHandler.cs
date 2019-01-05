using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using InstantReview.Droid;
using InstantReview.ViewModels;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace InstantReview.Login
{
    public class ConnectionHandler : IConnectionHandler
    {
        private readonly ISettingsStorage storage;
        private readonly IDialogService dialogService;
        private readonly ReviewDataCollector dataCollector;
        private static readonly ILog Log = LogManager.GetLogger<LoginPageViewModel>();

        private const string baseAddress = "http://165.227.140.152/";
        private const string registerExtension = "auth/signup";
        private const string loginExtension = "auth/login";
        private const string uploadReviewExtension = "review/create";
        private const string uploadImageExtension = "review/image/upload";
        private const string reviewListExtension = "review/list";
        private const string downloadReviewExtension = "review/get/";
        private const string downloadImageExtension = "review/image/download/";
        private const string editReviewExtension = "review/edit";



        public ConnectionHandler(ISettingsStorage storage, ReviewDataCollector dataCollector, IDialogService dialogService)
        {
            this.storage = storage;
            this.dataCollector = dataCollector;
            this.dialogService = dialogService;
        }

        public void SaveUsagePrivileges(string token)
        {
            storage.SetValue("user", token);
        }

        public bool CheckUsagePrivileges()
        {
            var hasPrivilege = false;
            var readValue = storage.GetValue("user", string.Empty);
            if (readValue != String.Empty && CheckTokenValidity(readValue))
            {
                hasPrivilege = true;
            }
            return hasPrivilege;
        }

        public void DeletePrivileges()
        {
            storage.RemoveValue("user");
        }

        public async Task<HttpResponseMessage> Register(string email, string password, string name = "", string birthday = "", string gender = "")
        {
            var infos = new RegisterInfo { password = password, email = email, birthday = birthday, gender = gender, name = name};

            HttpResponseMessage response;
            using (var client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(infos), Encoding.UTF8, "application/json");
                response = await client.PostAsync(baseAddress + registerExtension, content, CancellationToken.None);
            }

            return response;
        }

        public async Task<HttpResponseMessage> Login(string email, string password)
        {
            var infos = new LoginInfo {password = password, email = email};

            HttpResponseMessage response;
            using (var client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(infos), Encoding.UTF8, "application/json");
                response = await client.PostAsync(baseAddress + loginExtension, content, CancellationToken.None);
            }

            return response;
        }

        public bool CheckTokenValidity(string token)
        {
            bool active = false;

            JwtSecurityTokenHandler reader = new JwtSecurityTokenHandler();
            var jwtToken = reader.ReadJwtToken(token);

            var expirationDate = jwtToken.ValidTo;
            var currentDateTime = DateTime.UtcNow;
            if (expirationDate >= currentDateTime)
            {
                Log.Debug("Token is still valid. ");
                active = true;
            }
            else
            {
                Log.Debug("Token not valid. Need to log in again.");
            }

            return active;
        }

        protected class LoginInfo
        {
            public string email;
            public string password;
        }
        
        protected class RegisterInfo
        {
            public string email;
            public string password;
            public string name;
            public string birthday;
            public string gender;
        }

        public async Task<Stream> DownloadImage(string filename)
        {
            using (var client = new HttpClient())
            {
                AddAuthorizationHeader(client);

                var response = await client.GetAsync(baseAddress + downloadImageExtension + filename);

                var stream = await response.Content.ReadAsStreamAsync();

                return stream;
            }
        }

        public async Task<EditableReview> DownloadReview(int id)
        {
            Log.Debug("Downloading review by id "+id);
            var success = false;
            EditableReview editable = new EditableReview();
            using (var client = new HttpClient())
            {
                AddAuthorizationHeader(client);

                try
                {
                    var response = await client.GetAsync(baseAddress + downloadReviewExtension+id, CancellationToken.None);
                    var responseJson = await response.Content.ReadAsStringAsync();
                    editable = JsonConvert.DeserializeObject<EditableReview>(responseJson);
                    success = true;

                }
                catch (Exception e)
                {
                    Log.Error("Error while downloading review. Reason: " + e);
                }
            }

            Log.Debug($"Review download status: {success}");
            return editable;

        }

        public async Task<bool> UploadReview()
        {
            Log.Debug("Uploading review");
            var success = false;
            var data = dataCollector.ToSerializedFormat();

            using (var client = new HttpClient())
            {
                AddAuthorizationHeader(client);
                var content = new StringContent(data, Encoding.UTF8, "application/json");

                try
                {
                    var response = await client.PostAsync(baseAddress + uploadReviewExtension, content, CancellationToken.None);
                    var responseJson = await response.Content.ReadAsStringAsync();
                    var deserialized = Newtonsoft.Json.JsonConvert.DeserializeObject<NewReviewResponse>(responseJson);
                    success = await SendFileToServer(dataCollector.Data.imagePath, deserialized.id);
                }
                catch (Exception e)
                {
                    Log.Error("Error while uploading review. Reason: "+e);
                }
            }

            Log.Debug($"Review upload status: {success}");
            dataCollector.InitializeDataCollector();
            return success;
        }

        public async Task<bool> UploadEditedReview(EditPageViewModel.EditedReview edited)
        {
            Log.Debug("Uploading edited review");
            var success = false;
            edited.appId = 1;
            var data = JsonConvert.SerializeObject(edited);

            using (var client = new HttpClient())
            {
                AddAuthorizationHeader(client);
                var content = new StringContent(data, Encoding.UTF8, "application/json");

                try
                {
                    var response = await client.PostAsync(baseAddress + editReviewExtension, content, CancellationToken.None);
                    var responseJson = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw new Exception("Response was not OK. Aborting. Reason: " + responseJson);
                    }

                    success = true;

                }
                catch (Exception e)
                {
                    Log.Error("Error while uploading edited review. Reason: " + e);
                }
            }

            Log.Debug($"Edited review upload status: {success}");
            dialogService.showAlert("Mission successful!", "Edited review uploaded successfully.", "Ok");
            return success;
        }



        private async Task<bool> SendFileToServer(string filePath, string id)
        {
            var success = false;
            var fileBytes = File.ReadAllBytes(filePath);
            using (var client = new HttpClient())
            {
                AddAuthorizationHeader(client);

                var content = new MultipartFormDataContent();
                content.Add(new ByteArrayContent(fileBytes), "screenshot", "testname.jpg");
                content.Add(new StringContent(id), "reviewId");

                var response = await client.PostAsync(baseAddress + uploadImageExtension, content, CancellationToken.None);
                if (response.IsSuccessStatusCode)
                {
                    success = true;
                }
            }

            return success;
        }

        public async Task<List<Review>> DownloadReviewList()
        {
            HttpResponseMessage response;
            List<Review> reviews;
            using (var client = new HttpClient())
            {
                AddAuthorizationHeader(client);
                response = await client.PostAsync(baseAddress + reviewListExtension, null, CancellationToken.None);
                var responseJson = await response.Content.ReadAsStringAsync();

                reviews = JsonConvert.DeserializeObject<List<Review>>(responseJson);
            }

            return reviews;

        }

        

        private void AddAuthorizationHeader(HttpClient client)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(storage.GetValue("user", string.Empty));
        }

        // Classes to deserialize response from server
        public class NewReviewResponse
        {
            public string id { get; set; }
        }
    }

}