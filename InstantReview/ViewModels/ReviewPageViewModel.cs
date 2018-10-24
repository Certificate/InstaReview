using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using InstantReview.Receivers;
using Xamarin.Forms;

namespace InstantReview.ViewModels
{
    public class ReviewPageViewModel : IReviewPageViewModel
    {
        private static readonly ILog Log = LogManager.GetLogger<ReviewPageViewModel>();


        public ReviewPageViewModel(IShareIntentReceiver intentReceiver)
        {
            intentReceiver.ItemsReceivedEvent += IntentReceiverOnItemsReceivedEvent;
        }

        private void IntentReceiverOnItemsReceivedEvent(object sender, EventArgs e)
        {
            Log.Debug("Notified at ReviewViewModel");
        }
    }
}
