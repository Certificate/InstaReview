using System;
using System.IO;
using System.Windows.Input;
using Common.Logging;
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
        private static readonly ILog Log = LogManager.GetLogger<MainPageViewModel>();


        public MainPageViewModel(
            IDialogService dialogService, 
            INavigation navigation, 
            IPageFactory pageFactory, 
            IReviewPageViewModel reviewPageViewModel)
        {
            this.dialogService = dialogService;
            this.navigation = navigation;
            this.pageFactory = pageFactory;
            this.reviewPageViewModel = reviewPageViewModel;
            reviewPageViewModel.ViewModelReadyEvent += IntentReceiver_ItemsReceivedEvent;
        }

        public ICommand NewReviewCommand => new Command(NavigateToReviewPage);

        void IntentReceiver_ItemsReceivedEvent(object sender, EventArgs e)
        {
            NavigateToReviewPage();
        }


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
