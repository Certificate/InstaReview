using System;
using Autofac;
using InstantReview.ViewModels;
using InstantReview.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace InstantReview
{
    public interface IContainerResolver
    {
        T Resolve<T>();
    }

    public partial class App : Application, IContainerResolver
    {
        public IContainer Container { get; }
        private INavigation Navigation { get; set; }

        private readonly MasterDetailPage masterDetailPage;
        private readonly NavigationPage navigationPage;

        public App(ContainerBuilder containerBuilder)
        {
            InitializeComponent();
            

            navigationPage = CreateNavigationPage();

            Container = CreateContainer(containerBuilder);

            navigationPage.PushAsync(CreateMainPage());
            masterDetailPage = CreateMasterDetailPage(navigationPage);
            MainPage = masterDetailPage;
            MainPage = new ThankYouPage();

        }

        private MasterDetailPage CreateMasterDetailPage(Page detailPage)
        {
            var masterPageVm = Container.Resolve<MasterPageViewModel>();

            //masterPageVm.NavigationRequested += OnMasterDetailNavigationRequested;
            //masterPageVm.LogOutRequested += UpdateNavigation;

            return new MasterDetailPage
            {
                Master = new MasterPage(masterPageVm),
                Detail = detailPage
            };
        }

        private Page CreateMainPage()
        {
            return new MainPage(Container.Resolve<MainPageViewModel>());
        }

        private NavigationPage CreateNavigationPage()
        {
            var page = new NavigationPage()
            {
                BarBackgroundColor = Color.Salmon,
                BarTextColor = Color.Black,
            };

            Navigation = page.Navigation;
            return page;
        }


        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private IContainer CreateContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<UiModule>();

            builder.RegisterInstance(Navigation).As<INavigation>();
            builder.RegisterInstance(this).As<IContainerResolver>();
            return builder.Build();
        }

        public T Resolve<T>()
        {
            return Container.Resolve<T>();
        }
    }
}
