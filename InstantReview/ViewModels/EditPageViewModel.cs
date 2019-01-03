using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using InstantReview.Login;
using Xamarin.Forms;
using Image = InstantReview.Login.Image;

namespace InstantReview.ViewModels
{
    public class EditPageViewModel : BaseViewModel
    {
        private static readonly ILog Log = LogManager.GetLogger<EditPageViewModel>();

        private readonly IConnectionHandler connectionHandler;


        public int ReviewId { get; set; }

        public EditableReview review { get; set; }

        public string ImagePath { get; set; }

        public string AdditionalInfoText { get; set; }

        public string Category { get; set; }

        public Image ReviewImage { get; set; }

        public ImageSource ImgSrc { get; set; }

        public string TemporalContext { get; set; }
        public string SpatialContext { get; set; }
        public string SocialContext { get; set; }





        public EditPageViewModel(IConnectionHandler connectionHandler)
        {
            this.connectionHandler = connectionHandler;
        }

        public async Task<bool> DownloadReview(int id)
        {
            ReviewId = id;
            review = await connectionHandler.DownloadReview(id);

            var stream = await connectionHandler.DownloadImage(review.images[0].fileName);

            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                var bytes = memoryStream.ToArray();
                ImgSrc = ImageSource.FromStream(() => new MemoryStream(bytes));
            }

            AdditionalInfoText = review.textReview;
            Category = "TBD";
            TemporalContext = review.temporalContext;
            SpatialContext = review.spatialContext;
            SocialContext = review.socialContext;
            return true;
        }
    }
}
