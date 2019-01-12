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

        private double questionFontSize, emptySpace;

        public QuestionPageViewModel(IDialogService dialogService, ReviewDataCollector dataCollector, IPageFactory pageFactory, INavigation navigation, ThankYouPageViewModel thankYouPageViewModel)
        {
            this.dialogService = dialogService;
            this.dataCollector = dataCollector;
            this.pageFactory = pageFactory;
            this.navigation = navigation;
            this.thankYouPageViewModel = thankYouPageViewModel;

            questionFontSize = 25;
            emptySpace = 20;
        }

        public bool TemporalContextValue { get; set; }

        public bool SpatialContextValue { get; set; }

        public bool SocialContextValue { get; set; }

        public bool Q1Toggled { get; set; }

        public bool Q2Toggled { get; set; }

        public bool Q3Toggled { get; set; }

        public bool Q4Toggled { get; set; }

        public bool Q5Toggled { get; set; }

        public bool Q6Toggled { get; set; }

        public bool Q7Toggled { get; set; }

        public bool Q8Toggled { get; set; }

        public bool Q9Toggled { get; set; }

        public double qFontSize {
            get { return questionFontSize; }
            set
            {
                if(questionFontSize != value)
                {
                    questionFontSize = value;
                }
            }
        }

        public double gapBetweenQuestions
        {
            get { return emptySpace; }
            set
            {
                if (emptySpace != value)
                {
                    emptySpace = value;
                }
            }
        }


        public ICommand CheckResultCommand => new Command(NavigateToThankYouPage);

        private bool AddQuestionsToDataCollector()
        {
            var success = false;
            Log.Debug("Adding questions to data collector");
            try
            {
                Console.WriteLine(Q1Toggled);
                if (Q1Toggled || Q2Toggled || Q3Toggled)
                {
                    dataCollector.Data.temporalContext = "Intensive";
                }
                else
                {
                    dataCollector.Data.temporalContext = "Allocative";
                }

                if (Q4Toggled || Q5Toggled || Q6Toggled)
                {
                    dataCollector.Data.socialContext = "Constraining";
                }
                else
                {
                    dataCollector.Data.socialContext = "Encouraging";
                }

                if (Q7Toggled)
                {
                    dataCollector.Data.spatialContext = "Visiting";
                }
                else if (Q8Toggled)
                {
                    dataCollector.Data.spatialContext = "Wandering";
                }
                else
                {
                    dataCollector.Data.spatialContext = "Traveling";
                }

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