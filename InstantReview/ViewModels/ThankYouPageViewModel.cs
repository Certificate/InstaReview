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
        private readonly MainPageViewModel mainPage;

        public ThankYouPageViewModel(ReviewDataCollector dataCollector, IDialogService dialogService, INavigation navigation, IConnectionHandler connectionHandler, MainPageViewModel mainPage)
        {
            this.dataCollector = dataCollector;
            this.dialogService = dialogService;
            this.navigation = navigation;
            this.connectionHandler = connectionHandler;
            this.mainPage = mainPage;
        }


        public ICommand HomePageCommand => new Command(NavigateToHome);

        private async void NavigateToHome()
        {
            // TODO: For debug purposes only
            dialogService.showAlert(dataCollector.GenerateReviewText());
            
            // Upload review.
            await connectionHandler.UploadReview();

            // When upload is complete, refresh reviews on main page
            ReviewDoneEvent?.Invoke(this, EventArgs.Empty);

            // Pop navigation stack back to home page
            await navigation.PopToRootAsync();
        }
    }
    
    
}