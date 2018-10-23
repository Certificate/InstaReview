using System;
using System.Windows.Input;
using Common.Logging;
using InstantReview.Droid;
using InstantReview.Receivers;
using InstantReview.Views;
using Xamarin.Forms;

namespace InstantReview.ViewModels
{
    public class MainPageViewModel
    {
        private readonly IDialogService dialogService;
        private readonly INavigation navigation;
        private readonly IPageFactory pageFactory;
        private readonly IReviewPageViewModel reviewPageViewModel;
        private readonly IImageOperations imageOperations;
        private static readonly ILog Log = LogManager.GetLogger<MainPageViewModel>();


        public MainPageViewModel(
            IDialogService dialogService, 
            INavigation navigation, 
            IPageFactory pageFactory, 
            IReviewPageViewModel reviewPageViewModel,
            IImageOperations imageOperations)
        {
            this.dialogService = dialogService;
            this.navigation = navigation;
            this.pageFactory = pageFactory;
            this.reviewPageViewModel = reviewPageViewModel;
            this.imageOperations = imageOperations;
            imageOperations.ImageReceivedEvent += ImageOperationsOnImageReceivedEvent;
        }

        private void ImageOperationsOnImageReceivedEvent(object sender, EventArgs e)
        {
            Log.Debug("Got <<<<<EVENT>>>>> from image op");
            NavigateToReviewPage();
        }

        public ICommand NewReviewCommand => new Command(NavigateToReviewPage);

        public async void NavigateToReviewPage(){
            Log.Debug("Navigating to Reviews!");
            await navigation.PushAsyncSingle(CreateReviewPage());
        }

        private Page CreateReviewPage()
        {
            return pageFactory.CreatePage<ReviewPage, IReviewPageViewModel>(reviewPageViewModel);
        }
    }
}
