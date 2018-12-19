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

        public bool TemporalContextValue { get; set; }

        public bool SpatialContextValue { get; set; }

        public bool SocialContextValue { get; set; }


        public ICommand CheckResultCommand => new Command(NavigateToThankYouPage);

        private bool AddQuestionsToDataCollector()
        {
            var success = false;
            Log.Debug("Adding questions to data collector");
            try
            {
                dataCollector.Data.temporalContext = "Intensive";
                dataCollector.Data.spatialContext = "Visiting";
                dataCollector.Data.socialContext = "Constraining";
                success = true;
            }
            catch (Exception e)
            {
                Log.Error("Failed to add question data.", e);
            }
            return success;

        }

        public async void NavigateToThankYouPage()
        {
            if (AddQuestionsToDataCollector())
            {
                Log.Debug("Navigating to Reviews!");
                await navigation.PushAsyncSingle(CreateThankYouPage());
            }
            else
            {
                Log.Error("Could not continue forward.");
                dialogService.showAlert("Error", "Error while gathering data. Resuming to home page.", "Dismiss");
                await navigation.PopToRootAsync();

            }

        }

        private Page CreateThankYouPage()
        {
            return pageFactory.CreatePage<ThankYouPage, ThankYouPageViewModel>(thankYouPageViewModel);
        }
    }
}