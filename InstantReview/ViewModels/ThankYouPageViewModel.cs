using System.Windows.Input;
using Xamarin.Forms;

namespace InstantReview.ViewModels
{
    public class ThankYouPageViewModel
    {
        private readonly ReviewDataCollector dataCollector;
        private readonly IDialogService dialogService;
        private readonly INavigation navigation;

        public ThankYouPageViewModel(ReviewDataCollector dataCollector, IDialogService dialogService, INavigation navigation)
        {
            this.dataCollector = dataCollector;
            this.dialogService = dialogService;
            this.navigation = navigation;
        }


        public ICommand HomePageCommand => new Command(CheckResults);

        private async void CheckResults()
        {
            dialogService.showAlert(dataCollector.GenerateReviewText());
            await navigation.PopToRootAsync();
        }
    }
    
    
}