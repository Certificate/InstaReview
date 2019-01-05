using System;
using System.Windows.Input;
using Common.Logging;
using InstantReview.Receivers;
using InstantReview.Views;
using Xamarin.Forms;


namespace InstantReview.ViewModels
{
    public class ReviewPageViewModel : IReviewPageViewModel
    {
        private static readonly ILog Log = LogManager.GetLogger<ReviewPageViewModel>();
        public event EventHandler<EventArgs> ViewModelReadyEvent;
        private readonly IShareIntentReceiver intentReceiver;
        private readonly QuestionPageViewModel questionPageViewModel;
        private readonly IPageFactory pageFactory;
        private readonly INavigation navigation;
        private readonly ReviewDataCollector dataCollector;

        //TODO: Find out max image dimensions
        public string ImagePath { get; set; }

        public string AdditionalInfoText { get; set; }

        public string Category { get; set; }

        public ReviewPageViewModel(IShareIntentReceiver intentReceiver, IPageFactory pageFactory, INavigation navigation, QuestionPageViewModel questionPageViewModel, ReviewDataCollector dataCollector)
        {
            this.intentReceiver = intentReceiver;
            this.pageFactory = pageFactory;
            this.navigation = navigation;
            this.questionPageViewModel = questionPageViewModel;
            this.dataCollector = dataCollector;
            intentReceiver.ItemsReceivedEvent += IntentReceiverOnItemsReceivedEvent;
        }

        private void IntentReceiverOnItemsReceivedEvent(object sender, EventArgs e)
        {
            Log.Debug("Notified at ReviewViewModel");
            ImagePath = intentReceiver.ImagePath;
            Log.Debug(ImagePath);
            ViewModelReadyEvent?.Invoke(this, EventArgs.Empty);
        }
        
        public ICommand DoneCommand => new Command(NavigateToQuestionsPage);
        
        public async void NavigateToQuestionsPage()
        {
            Log.Debug("Initializing DataCollector");
            dataCollector.InitializeDataCollector();
            
            Log.Debug("Adding additional info text to data collector");
            dataCollector.Data.textReview = AdditionalInfoText;
            
            Log.Debug("Adding ImagePath");
            dataCollector.Data.imagePath = ImagePath;

            Log.Debug("Adding Category");
            dataCollector.Data.category.categoryName = Category;
            
            Log.Debug("Navigating to Reviews!");
            await navigation.PushAsyncSingle(CreateQuestionsPage());
        }

        private Page CreateQuestionsPage()
        {
            return pageFactory.CreatePage<QuestionPage, QuestionPageViewModel>(questionPageViewModel);
        }
    }
}
