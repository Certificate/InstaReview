using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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
        private readonly INavigation navigation;
        private readonly ReviewDataCollector dataCollector;
        private readonly ThankYouPageViewModel thankYouPageViewModel;


        public int ReviewId { get; set; }

        public EditableReview OriginalReview { get; set; }

        public string ImagePath { get; set; }

        public string AdditionalInfoText { get; set; }

        public string Category { get; set; }

        public Image ReviewImage { get; set; }

        public ImageSource ImgSrc { get; set; }

        public string TemporalContext { get; set; }
        public string SpatialContext { get; set; }
        public string SocialContext { get; set; }


        public ICommand EditReviewCommand => new Command(GenerateAndUploadChangedReview);


        public EditPageViewModel(IConnectionHandler connectionHandler, ReviewDataCollector dataCollector, ThankYouPageViewModel thankYouPageViewModel, INavigation navigation)
        {
            this.connectionHandler = connectionHandler;
            this.dataCollector = dataCollector;
            this.thankYouPageViewModel = thankYouPageViewModel;
            this.navigation = navigation;
        }

        public async Task<bool> DownloadReview(int id)
        {
            ReviewId = id;
            OriginalReview = await connectionHandler.DownloadReview(id);

            var stream = await connectionHandler.DownloadImage(OriginalReview.images[0].fileName);

            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                var bytes = memoryStream.ToArray();
                ImgSrc = ImageSource.FromStream(() => new MemoryStream(bytes));
            }

            AdditionalInfoText = OriginalReview.textReview;
            Category = "TBD";
            TemporalContext = OriginalReview.temporalContext;
            SpatialContext = OriginalReview.spatialContext;
            SocialContext = OriginalReview.socialContext;
            return true;
        }

        public async void GenerateAndUploadChangedReview()
        {
            bool willUpload = false;

            var abc = OriginalReview;

            var original = new Review
            {
                id = ReviewId,
                category = OriginalReview.category,
                socialContext = OriginalReview.socialContext,
                spatialContext = OriginalReview.spatialContext,
                temporalContext = OriginalReview.temporalContext,
                textReview = OriginalReview.textReview
            };



            var edited = new EditedReview { id = ReviewId};

            if (!original.category.categoryName.Equals(Category))
            {
                edited.categoryName = Category;
                willUpload = true;
            }
            else
            {
                edited.categoryName = original.category.categoryName;
            }

            if (!original.socialContext.Equals(SocialContext))
            {
                edited.socialContext = SocialContext;
                willUpload = true;
            }
            else
            {
                edited.socialContext = original.socialContext;
            }

            if (!original.spatialContext.Equals(SpatialContext))
            {
                edited.spatialContext = SpatialContext;
                willUpload = true;
            }
            else
            {
                edited.spatialContext = original.spatialContext;
            }

            if (!original.temporalContext.Equals(TemporalContext))
            {
                edited.temporalContext = TemporalContext;
                willUpload = true;
            }
            else
            {
                edited.temporalContext = original.temporalContext;
            }

            if (!original.textReview.Equals(AdditionalInfoText))
            {
                edited.textReview = AdditionalInfoText;
                willUpload = true;
            }
            else
            {
                edited.textReview = original.textReview;
            }

            if (willUpload)
            {
                await connectionHandler.UploadEditedReview(edited);
            }


            await navigation.PopToRootAsync();
            thankYouPageViewModel.InvokeEvent();
        }

        private string GetCategoryNameFromId(int id)
        {

            switch (id)
            {
                case 0:
                    return "Functional Error";
                case 1:
                    return "Lagging";
                case 2:
                    return "Unattractive Interface Design";
                case 3:
                    return "Uninteresting Content";
                case 4:
                    return "App Quits Unexpectedly";
                case 5:
                    return "App Freeze";
                case 6:
                    return "Lose Data";
                case 7:
                    return "Feature missing";
                case 8:
                    return "Feature should be removed";
                case 9:
                    return "Feature not working as expected";
                case 10:
                    return "Difficult to use";
                case 11:
                    return "Not working on particular system version";
                case 12:
                    return "Not working on particular device";
                case 13:
                    return "Poor Connection with Wifi";
                case 14:
                    return "Poor Connection with Mobile Network";
                case 15:
                    return "Hidden Cost";
                case 16:
                    return "Too Expensive";
                case 17:
                    return "Privacy and ethics issues";
                case 18:
                    return "Cost too much energy or memory";
                default:
                    return "Not Specific";
            }
        }

        public class EditedReview
        {
            public int id { get; set; }
            public int appId { get; set; }
            public string temporalContext { get; set; }
            public string spatialContext { get; set; }
            public string socialContext { get; set; }
            public string categoryName { get; set; }
            public string textReview { get; set; }
        }

    }
}
