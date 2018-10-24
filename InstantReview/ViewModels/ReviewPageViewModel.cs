using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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

        private string ImagePath { get; set; }

        public Image Paska { get; set; }

        public ReviewPageViewModel(IShareIntentReceiver intentReceiver)
        {
            this.intentReceiver = intentReceiver;
            intentReceiver.ItemsReceivedEvent += IntentReceiverOnItemsReceivedEvent;
        }

        private void IntentReceiverOnItemsReceivedEvent(object sender, EventArgs e)
        {
            Log.Debug("Notified at ReviewViewModel");
            ImagePath = intentReceiver.ImagePath;
            Log.Debug(ImagePath);
            userImage = new Image();
            setImageSource(ImagePath);
            Paska = intentReceiver.UserImage;
            Task.Delay(TimeSpan.FromSeconds(4)).Wait();
            ViewModelReadyEvent?.Invoke(this, EventArgs.Empty);
            Log.Debug("Invoked VM Ready Event.");
        }

        public Image userImage;

        private void setImageSource(string path){
            
            userImage.Source = ImageSource.FromFile(path);
        }
        
    }
}
