using System.Windows.Input;
using InstantReview.Login;
using Xamarin.Forms;

namespace InstantReview.ViewModels
{
    public class ThankYouPageViewModel
    {
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

        private async void NavigateToHome()
        {
            // TODO: For debug purposes only
            dialogService.showAlert(dataCollector.GenerateReviewText());
            
            // Upload review. No need to await in this case.
            connectionHandler.UploadReview();
            
            // Pop navigation stack back to home page
            await navigation.PopToRootAsync();
        }
    }
    
    
}