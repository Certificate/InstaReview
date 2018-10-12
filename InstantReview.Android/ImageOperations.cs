using System;
using Common.Logging;
using InstantReview.Droid.Receivers;
using InstantReview.Receivers;
using InstantReview.ViewModels;
using Xamarin.Forms;

namespace InstantReview.Droid
{
    public class ImageOperations : IImageOperations
    {
        private static readonly ILog Log = LogManager.GetLogger<MainPageViewModel>();
        private IShareIntentReceiver intentReceiver;


        public ImageOperations(IShareIntentReceiver intentReceiver)
        {
            this.intentReceiver = intentReceiver;
            intentReceiver.ItemsReceivedEvent += OnItemsReceivedEvent;
        }

        private void OnItemsReceivedEvent(object sender, EventArgs e)
        {
            Log.Debug("Received event in ViewModel!");
        }

        
    }
}