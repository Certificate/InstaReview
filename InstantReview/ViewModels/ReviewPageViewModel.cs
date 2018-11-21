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

        //TODO: Find out max image dimensions
        public string ImagePath { get; set; }

        public ReviewPageViewModel(IShareIntentReceiver intentReceiver, IPageFactory pageFactory, INavigation navigation, QuestionPageViewModel questionPageViewModel)
        {
            this.intentReceiver = intentReceiver;
            this.pageFactory = pageFactory;
            this.navigation = navigation;
            this.questionPageViewModel = questionPageViewModel;
            intentReceiver.ItemsReceivedEvent += IntentReceiverOnItemsReceivedEvent;
        }

        private void IntentReceiverOnItemsReceivedEvent(object sender, EventArgs e)
        {
            Log.Debug("Notified at ReviewViewModel");
            ImagePath = intentReceiver.ImagePath;
            Log.Debug(ImagePath);
            ViewModelReadyEvent?.Invoke(this, EventArgs.Empty);
        }
        
        public ICommand DoneCommand => new Command(NavigateToThankYouPage);
        
        public async void NavigateToThankYouPage(){
            Log.Debug("Navigating to Reviews!");
            await navigation.PushAsyncSingle(CreateQuestionsPage());
        }

        private Page CreateQuestionsPage()
        {
            return pageFactory.CreatePage<QuestionPage, QuestionPageViewModel>(questionPageViewModel);
        }
    }
}
