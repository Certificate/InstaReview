using System;
using System.Windows.Input;
using InstantReview.Login;
using Xamarin.Forms;

namespace InstantReview.ViewModels
{
    public class ThankYouPageViewModel
    {
        public event EventHandler<EventArgs> ReviewDoneEvent;

        private readonly ReviewDataCollector dataCollector;
        private readonly IConnectionHandler connectionHandler;
        private readonly IDialogService dialogService;
        private readonly INavigation navigation;

        public ThankYouPageViewModel(ReviewDataCollector dataCollector, IDialogService dialogService, INavigation navigation, IConnectionHandler connectionHandler)
        {
            this.dataCollector = dataCollector;
            this.dialogService = dialogService;
            this.navigation = navigation;
            this.connectionHandler = connectionHandler;
        }


        public ICommand HomePageCommand => new Command(NavigateToHome);

        public void InvokeEvent()
        {
            ReviewDoneEvent?.Invoke(this, EventArgs.Empty);
        }

        private async void NavigateToHome()
        {
            
            // Upload review.
            await connectionHandler.UploadReview();

            // When upload is complete, refresh reviews on main page
            ReviewDoneEvent?.Invoke(this, EventArgs.Empty);

            // Pop navigation stack back to home page
            await navigation.PopToRootAsync();
        }
    }
    
    
}