using System;
using Common.Logging;
using InstantReview.Receivers;
using InstantReview.ViewModels;
using Xamarin.Forms;

namespace InstantReview.Droid
{
    public class ImageOperations : IImageOperations
    {
        private static readonly ILog Log = LogManager.GetLogger<MainPageViewModel>();
        private readonly IShareIntentReceiver intentReceiver;

        public event EventHandler<EventArgs> ImageReceivedEvent;

        public Image UserImage { get; set; }

        public ImageOperations(
            IShareIntentReceiver intentReceiver)
        {
            this.intentReceiver = intentReceiver;
            intentReceiver.ItemsReceivedEvent += OnItemsReceivedEvent;
        }

        private void OnItemsReceivedEvent(object sender, EventArgs e)
        {
            Log.Debug("Received event in ImageOperations!");
            UserImage = intentReceiver.UserImage;
            ImageReceivedEvent?.Invoke(this, EventArgs.Empty);
            Log.Debug("Invoked ImageReceivedEvent");
        }
    }
}