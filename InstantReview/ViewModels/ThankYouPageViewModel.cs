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


        public ICommand HomePageCommand => new Command(CheckResults);

        private async void CheckResults()
        {
            dialogService.showAlert(dataCollector.GenerateReviewText());
            connectionHandler.UploadReview();
            await navigation.PopToRootAsync();
        }
    }
    
    
}