using System;
using Common.Logging;
using InstantReview.Receivers;
using Xamarin.Forms;


namespace InstantReview.ViewModels
{
    public class ReviewPageViewModel : IReviewPageViewModel
    {
        private static readonly ILog Log = LogManager.GetLogger<ReviewPageViewModel>();
        public event EventHandler<EventArgs> ViewModelReadyEvent;
        private readonly IShareIntentReceiver intentReceiver;

        //TODO: Find out max image dimensions
        public string ImagePath { get; set; }

        public ReviewPageViewModel(IShareIntentReceiver intentReceiver)
        {
            this.intentReceiver = intentReceiver;
            intentReceiver.ItemsReceivedEvent += IntentReceiverOnItemsReceivedEvent;
        }

        private void IntentReceiverOnItemsReceivedEvent(object sender, EventArgs e)
        {
            Log.Debug("Notified at ReviewViewModel");
            Log.Debug(ImagePath);
            ImagePath = intentReceiver.ImagePath;
            ViewModelReadyEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}
