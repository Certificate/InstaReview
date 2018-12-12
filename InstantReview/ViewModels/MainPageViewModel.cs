using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using Common.Logging;
using InstantReview.Login;
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
        private readonly IConnectionHandler connectionHandler;
        private static readonly ILog Log = LogManager.GetLogger<MainPageViewModel>();

        public ObservableCollection<Review> ReviewsList { get; set; }

        public MainPageViewModel(
            IDialogService dialogService, 
            INavigation navigation, 
            IPageFactory pageFactory, 
            IReviewPageViewModel reviewPageViewModel, 
            IConnectionHandler connectionHandler,
            ThankYouPageViewModel thankYouPageViewModel)
        {
            this.dialogService = dialogService;
            this.navigation = navigation;
            this.pageFactory = pageFactory;
            this.reviewPageViewModel = reviewPageViewModel;
            this.connectionHandler = connectionHandler;
            reviewPageViewModel.ViewModelReadyEvent += IntentReceiver_ItemsReceivedEvent;
            ReviewsList = new ObservableCollection<Review>();
            GetReviewsByUser();
            thankYouPageViewModel.ReviewDoneEvent += ThankYouPageViewModelOnReviewDoneEvent;
        }

        private void ThankYouPageViewModelOnReviewDoneEvent(object sender, EventArgs e)
        {
            GetReviewsByUser();
        }

        public ICommand NewReviewCommand => new Command(NavigateToReviewPage);

        void IntentReceiver_ItemsReceivedEvent(object sender, EventArgs e)
        {
            NavigateToReviewPage();
        }


        public async void GetReviewsByUser()
        {
            var list = await connectionHandler.DownloadReviewList();
            foreach (var review in list)
            {
                ReviewsList.Add(review);
            }
            Log.Debug("Done adding items to list!");
        }




        // Navigation
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
