using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using InstantReview.Droid;
using InstantReview.Receivers;
using Xamarin.Forms;

namespace InstantReview.ViewModels
{
    public class ReviewPageViewModel : IReviewPageViewModel
    {
        private static readonly ILog Log = LogManager.GetLogger<ReviewPageViewModel>();

        private readonly IImageOperations imageOperations;

        private Image userImage;

        public ReviewPageViewModel(IImageOperations ImageOperations)
        {
            this.imageOperations = ImageOperations;
            imageOperations.ImageReceivedEvent += ImageOperationsOnImageReceivedEvent;
            
        }

        private void ImageOperationsOnImageReceivedEvent(object sender, EventArgs e)
        {
            userImage = imageOperations.UserImage;
            Log.Debug("Got image to Review VM.");
        }
    }
}
