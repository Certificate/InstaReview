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

        private readonly IConnectionHandler _connection;

        public MasterPageViewModel(IConnectionHandler connection)
        {
            this._connection = connection;
        }

        public ICommand LogOutCommand => new Command(LogOut);

        private void LogOut()
        {
            _connection.DeletePrivileges();
            Log.Debug("Log Out!");
            LogoutSuccessful?.Invoke(this, EventArgs.Empty);
        }
    }
}