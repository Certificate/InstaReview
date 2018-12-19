using System;
using System.Windows.Input;
using Common.Logging;
using InstantReview.Login;
using InstantReview.Views;
using Xamarin.Forms;

namespace InstantReview.ViewModels
{
    public class MasterPageViewModel
    {
        private static readonly ILog Log = LogManager.GetLogger<MasterPageViewModel>();

        public event EventHandler<EventArgs> LogoutSuccessful;
        public event EventHandler<EventArgs> NavigationEvent;

        private readonly IConnectionHandler _connection;
        private readonly INavigation navigation;
        private readonly IPageFactory pageFactory;
        private readonly AccountDetailsViewModel accountDetails;

        public MasterPageViewModel(IConnectionHandler connection, INavigation navigation, IPageFactory pageFactory, AccountDetailsViewModel accountDetails)
        {
            this._connection = connection;
            this.navigation = navigation;
            this.pageFactory = pageFactory;
            this.accountDetails = accountDetails;
        }

        public ICommand AccountDetailsCommand => new Command(NavigateToAccountDetails);

        private async void NavigateToAccountDetails()
        {
            await navigation.PushAsyncSingle(CreateAccountDetailsPage());
        }

        private Page CreateAccountDetailsPage()
        {
            NavigationEvent?.Invoke(this, EventArgs.Empty);
            return pageFactory.CreatePage<AccountDetailsView, AccountDetailsViewModel>(accountDetails);
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