using System;
using System.Net;
using System.Net.Http;
using System.Windows.Input;
using Common.Logging;
using InstantReview.Login;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace InstantReview.ViewModels
{
    public class RegisterPageViewModel : BaseViewModel
    {
        private static readonly ILog Log = LogManager.GetLogger<RegisterPageViewModel>();

        public event EventHandler<EventArgs> RegisterSuccessful;
        private readonly IDialogService dialogService;
        private readonly IConnectionHandler connectionHandler;
        private readonly INavigation navigation;
        private readonly IPageFactory pageFactory;

        public RegisterPageViewModel(IDialogService dialogService, IConnectionHandler connectionHandler, INavigation navigation, IPageFactory pageFactory)
        {
            this.dialogService = dialogService;
            this.connectionHandler = connectionHandler;
            this.navigation = navigation;
            this.pageFactory = pageFactory;
        }

        public ICommand RegisterCommand => new Command(RegisterUser);

        private async void RegisterUser()
        {
            var success = false;
            try
            {
                if (!Password.Equals(CheckPassword))
                {
                    throw new ArgumentException("Passwords do not match");
                }

                var response = await connectionHandler.Register(Email, Password, Name, Birthday.ToString("MM/dd/yyyy"), Gender);
                var responseBody = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception("Response was not OK. Aborting. Reason: "+ responseBody);
                }

                var token = JsonConvert.DeserializeObject<Response>(await response.Content.ReadAsStringAsync());

                var isActive = connectionHandler.CheckTokenValidity(token.token);

                if (isActive)
                {
                    connectionHandler.SaveUsagePrivileges(token.token);
                    success = true;
                }
            }
            catch (ArgumentException e)
            {
                Log.Error("Password mismatch!", e);
                dialogService.showAlert("Passwords do not match!");
            }
            catch (Exception e)
            {
                Log.Error("Error while logging in!", e);
            }
            finally
            {
                Password = string.Empty;
                OnPropertyChanged(nameof(Password));
                CheckPassword = string.Empty;
                OnPropertyChanged(nameof(CheckPassword));
            }

            if (success)
            {
                Log.Debug("Logged in successfully!");
                Email = string.Empty;
                OnPropertyChanged(nameof(Email));
                dialogService.showRegisteredDialog();
                RegisterSuccessful?.Invoke(this, EventArgs.Empty);
            }
        }

        protected class Response
        {
            public string token;
        }

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string CheckPassword { get; set; } = string.Empty;

        public string Name { get; set; }
        
        public string Gender { get; set; }
        
        public DateTime Birthday { get; set; }
    }
}