using System;
using System.Windows.Input;
using Common.Logging;
using InstantReview.Login;
using Xamarin.Forms;

namespace InstantReview.ViewModels
{
    public class MasterPageViewModel
    {
        private static readonly ILog Log = LogManager.GetLogger<MasterPageViewModel>();

        public event EventHandler<EventArgs> LogoutSuccessful;

        private readonly ILoginHandler login;

        public MasterPageViewModel(ILoginHandler login)
        {
            this.login = login;
        }

        public ICommand LogOutCommand => new Command(LogOut);

        private void LogOut()
        {
            login.DeletePrivileges();
            Log.Debug("Log Out!");
            LogoutSuccessful?.Invoke(this, EventArgs.Empty);
        }
    }
}