using System.Windows.Input;
using Xamarin.Forms;

namespace InstantReview.ViewModels
{
    public class ThankYouPageViewModel
    {
        private readonly ReviewDataCollector dataCollector;
        private readonly IDialogService dialogService;

        public ThankYouPageViewModel(ReviewDataCollector dataCollector, IDialogService dialogService)
        {
            this.dataCollector = dataCollector;
            this.dialogService = dialogService;
        }


        public ICommand HomePageCommand => new Command(CheckResults);

        private void CheckResults()
        {
            dialogService.showAlert(dataCollector.GenerateReviewText());
        }
    }
    
    
}