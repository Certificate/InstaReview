using System;
using System.Windows.Input;
using Common.Logging;
using InstantReview.Droid;
using InstantReview.Login;
using System.Net.Http;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using Xamarin.Forms;
using System.IdentityModel;
using System.IdentityModel.Tokens.Jwt;
using System.IO.IsolatedStorage;
using System.Net;

namespace InstantReview.ViewModels
{
    public class LoginPageViewModel : BaseViewModel ,ILoginPageViewModel
    {
        public event EventHandler<EventArgs> LoginSuccessful;
        private static readonly ILog Log = LogManager.GetLogger<LoginPageViewModel>();
        private static readonly HttpClient client = new HttpClient();
        private readonly ILoginHandler loginHandler;

        public LoginPageViewModel(ILoginHandler loginHandler)
        {
            this.loginHandler = loginHandler;
        }

        public ICommand LoginCommand => new Command(StartLoginProcess);

        private async void StartLoginProcess()
        {
            var success = false;
            try
            {
                var response = await loginHandler.Login(Username, password);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception("Response was not OK. Aborting.");
                }

                var token = JsonConvert.DeserializeObject<Response>(await response.Content.ReadAsStringAsync());

                var isActive = loginHandler.CheckTokenValidity(token.token);

                if (isActive)
                {
                    loginHandler.SaveUsagePrivileges(token.token);
                    success = true;
                }
            }
            catch (Exception e)
            {
                Log.Error("Error while logging in!", e);
            }
            finally
            {
                Password = string.Empty;
                OnPropertyChanged(nameof(Password));
            }

            if (success)
            {
                Log.Debug("Logged in successfully!");
                Username = string.Empty;
                OnPropertyChanged(nameof(Username));
                LoginSuccessful?.Invoke(this, EventArgs.Empty);
            }
        }

        

        protected class Response
        {
            public string token;
        }

        private string username;

        public string Username
        {
            get { return username; }
            set
            {
                username = value;
            }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
            }
        }
    }
}