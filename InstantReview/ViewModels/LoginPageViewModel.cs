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
        private readonly IDialogService dialogService;
        private static readonly ILog Log = LogManager.GetLogger<LoginPageViewModel>();
        private static readonly HttpClient client = new HttpClient();
        private readonly IConnectionHandler _connectionHandler;

        public LoginPageViewModel(IConnectionHandler connectionHandler, IDialogService dialogService)
        {
            this._connectionHandler = connectionHandler;
            this.dialogService = dialogService;
        }

        public ICommand LoginCommand => new Command(StartLoginProcess);

        public ICommand RegisterCommand => new Command(RegisterUser);

        private async void RegisterUser()
        {
            var success = false;
            try
            {
                var response = await _connectionHandler.Register(Username, password);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception("Response was not OK. Aborting.");
                }

                var token = JsonConvert.DeserializeObject<Response>(await response.Content.ReadAsStringAsync());

                var isActive = _connectionHandler.CheckTokenValidity(token.token);

                if (isActive)
                {
                    _connectionHandler.SaveUsagePrivileges(token.token);
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
                dialogService.showRegisteredDialog();
                LoginSuccessful?.Invoke(this, EventArgs.Empty);
            }
        }

        private async void StartLoginProcess()
        {
            var success = false;
            try
            {
                var response = await _connectionHandler.Login(Username, password);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception("Response was not OK. Aborting.");
                }

                var token = JsonConvert.DeserializeObject<Response>(await response.Content.ReadAsStringAsync());

                var isActive = _connectionHandler.CheckTokenValidity(token.token);

                if (isActive)
                {
                    _connectionHandler.SaveUsagePrivileges(token.token);
                    dialogService.ShowLoginToast();
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