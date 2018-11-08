using System;
using System.Windows.Input;
using Common.Logging;
using InstantReview.Droid;
using InstantReview.Login;
using Xamarin.Forms;

namespace InstantReview.ViewModels
{
    public class LoginPageViewModel : ILoginPageViewModel
    {
        public event EventHandler<EventArgs> LoginSuccessful;
        private static readonly ILog Log = LogManager.GetLogger<LoginPageViewModel>();
        private readonly ILoginHandler login;

        public LoginPageViewModel(ILoginHandler login)
        {
            this.login = login;
        }

        public ICommand LoginCommand => new Command(LogIn);

        private void LogIn()
        {
            login.SaveUsagePrivileges();
            LoginSuccessful?.Invoke(this, EventArgs.Empty);
        }
    }
}