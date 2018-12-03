using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using InstantReview.Droid;
using InstantReview.ViewModels;
using Newtonsoft.Json;

namespace InstantReview.Login
{
    public class LoginHandler : ILoginHandler
    {
        private readonly ISettingsStorage storage;
        private static readonly ILog Log = LogManager.GetLogger<LoginPageViewModel>();

        private const string baseAddress = "http://165.227.140.152/";
        private const string registerPortal = "auth/signup";
        private const string loginPortal = "auth/login";



        public LoginHandler(ISettingsStorage storage)
        {
            this.storage = storage;
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

        public async Task<HttpResponseMessage> Register(string email, string password)
        {
            var infos = new LoginInfo { password = password, email = email };

            HttpResponseMessage response;
            using (var client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(infos), Encoding.UTF8, "application/json");
                response = await client.PostAsync(baseAddress + registerPortal, content, CancellationToken.None);
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
                response = await client.PostAsync(baseAddress + loginPortal, content, CancellationToken.None);
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
    }
}