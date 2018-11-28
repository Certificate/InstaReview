using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Common.Logging;
using InstantReview.Views;
using Xamarin.Forms;

namespace InstantReview.ViewModels
{
    public class QuestionPageViewModel
    {
        private static readonly ILog Log = LogManager.GetLogger<QuestionPageViewModel>();

        private readonly IDialogService dialogService;
        private readonly ReviewDataCollector dataCollector;
        private readonly IPageFactory pageFactory;
        private readonly INavigation navigation;
        private readonly ThankYouPageViewModel thankYouPageViewModel;

        public QuestionPageViewModel(IDialogService dialogService, ReviewDataCollector dataCollector, IPageFactory pageFactory, INavigation navigation, ThankYouPageViewModel thankYouPageViewModel)
        {
            this.dialogService = dialogService;
            this.dataCollector = dataCollector;
            this.pageFactory = pageFactory;
            this.navigation = navigation;
            this.thankYouPageViewModel = thankYouPageViewModel;
        }

        public bool Q1Toggled { get; set; }

        public bool Q2Toggled { get; set; }

        public bool Q3Toggled { get; set; }

        public bool Q4Toggled { get; set; }


        public ICommand CheckResultCommand => new Command(NavigateToThankYouPage);

        private void CheckResults()
        {
            string print = $"Q1: {Q1Toggled} Q2: {Q2Toggled} Q3: {Q3Toggled} Q4: {Q4Toggled}";
            dialogService.showAlert(print);
        }

        private bool AddQuestionsToDataCollector()
        {
            var success = false;
            Log.Debug("Adding questions to data collector");
            try
            {
                dataCollector.Data.Question1 = Q1Toggled.ToString();
                dataCollector.Data.Question2 = Q2Toggled.ToString();
                dataCollector.Data.Question3 = Q3Toggled.ToString();
                dataCollector.Data.Question4 = Q4Toggled.ToString();
                success = true;
            }
            catch (Exception e)
            {
                Log.Error("Failed to add question data.", e);
            }
            return success;

        }

        public void NavigateToThankYouPage()
        {
            AddQuestionsToDataCollector();
            
            Log.Debug("Navigating to Reviews!");
            navigation.PushAsyncSingle(CreateThankYouPage());
        }

        private Page CreateThankYouPage()
        {
            return pageFactory.CreatePage<ThankYouPage, ThankYouPageViewModel>(thankYouPageViewModel);
        }
    }
}