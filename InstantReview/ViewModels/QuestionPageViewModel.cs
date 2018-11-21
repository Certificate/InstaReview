using System.Windows.Input;
using Xamarin.Forms;

namespace InstantReview.ViewModels
{
    public class QuestionPageViewModel
    {

        private readonly IDialogService dialogService;

        public QuestionPageViewModel(IDialogService dialogService)
        {
            this.dialogService = dialogService;
        }

        public bool Q1Toggled { get; set; }

        public bool Q2Toggled { get; set; }

        public bool Q3Toggled { get; set; }

        public bool Q4Toggled { get; set; }


        public ICommand CheckResultCommand => new Command(CheckResults);

        private void CheckResults()
        {
            string print = $"Q1: {Q1Toggled} Q2: {Q2Toggled} Q3: {Q3Toggled} Q4: {Q4Toggled}";
            dialogService.showAlert(print);
        }
    }
}