using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Common.Logging;
using InstantReview.Login;
using InstantReview.Receivers;
using InstantReview.Views;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace InstantReview.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {


        private readonly IDialogService dialogService;
        private readonly INavigation navigation;
        private readonly IPageFactory pageFactory;

        private readonly IReviewPageViewModel reviewPageViewModel;
        private readonly EditPageViewModel editPageViewModel;
        private readonly IConnectionHandler connectionHandler;
        private static readonly ILog Log = LogManager.GetLogger<MainPageViewModel>();

        public ObservableCollection<Review> ReviewsList { get; set; }
        
        public int EditableReview { get; set; }


        public MainPageViewModel(
            IDialogService dialogService, 
            INavigation navigation, 
            IPageFactory pageFactory, 
            IReviewPageViewModel reviewPageViewModel, 
            IConnectionHandler connectionHandler,
            ThankYouPageViewModel thankYouPageViewModel,
            MasterPageViewModel masterPageViewModel,
            ILoginPageViewModel loginPageViewModel,
            EditPageViewModel editPageViewModel
            )
        {
            this.dialogService = dialogService;
            this.navigation = navigation;
            this.pageFactory = pageFactory;
            this.reviewPageViewModel = reviewPageViewModel;
            this.connectionHandler = connectionHandler;
            this.editPageViewModel = editPageViewModel;

            ReviewsList = new ObservableCollection<Review>();
            GetReviewsByUser();

            reviewPageViewModel.ViewModelReadyEvent += IntentReceiver_ItemsReceivedEvent;
            thankYouPageViewModel.ReviewDoneEvent += OnReviewDoneEvent;
            loginPageViewModel.LoginSuccessful += OnLoginStateChanged;
            MainPage.ItemSelected += OnReviewItemSelected;
        }

        private async void OnReviewItemSelected(object sender, EventArgs e)
        {
            var arg = (Review)((SelectedItemChangedEventArgs)e).SelectedItem;
            
            NavigateToEditPage(arg.id);
        }

        private void OnLoginStateChanged(object sender, EventArgs e)
        {
            ReviewsList.Clear();
            GetReviewsByUser();
        }

        private void OnReviewDoneEvent(object sender, EventArgs e)
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
            Log.Debug($"CollectionSize BEFORE: {ReviewsList.Count}");
            var counter = 0;
            foreach (var review in list)
            {
                if (ReviewsList.All(p => p.id != review.id))
                {
                    ReviewsList.Add(review);
                    counter++;
                }

                
            }
            Log.Debug("Done adding items to list!");
            Log.Debug($"CollectionSize AFTER: {ReviewsList.Count}");
            Log.Debug($"Added items: {counter}");
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


        public async void NavigateToEditPage(int id)
        {
            var status = await editPageViewModel.DownloadReview(id);

            var page = (EditPage)CreateEditPage();
            await navigation.PushAsyncSingle(page);

            

            
            
        }

        private Page CreateEditPage()
        {
            return pageFactory.CreatePage<EditPage, EditPageViewModel>(editPageViewModel);
        }
    }
}
