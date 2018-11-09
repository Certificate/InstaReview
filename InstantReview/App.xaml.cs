using System;
using System.Linq;
using Autofac;
using InstantReview.Droid;
using InstantReview.Login;
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
        private readonly ILoginHandler login;

        private bool UsagePrivilege => login.CheckUsagePrivileges();

        public App(ContainerBuilder containerBuilder)
        {
            InitializeComponent();
            

            navigationPage = CreateNavigationPage();

            Container = CreateContainer(containerBuilder);

            login = Container.Resolve<ILoginHandler>();
            Container.Resolve<ILoginPageViewModel>().LoginSuccessful += OnLoginStateChanged;

            navigationPage.PushAsync(!UsagePrivilege
                ? new LoginPage(Container.Resolve<ILoginPageViewModel>())
                : CreateMainPage());
            
            masterDetailPage = CreateMasterDetailPage(navigationPage);
            MainPage = masterDetailPage;
        }

        private void OnLoginStateChanged(object sender, EventArgs e)
        {
            UpdateNavigation(this, EventArgs.Empty);
        }

        private void UpdateNavigation(object sender, EventArgs eventArgs)
        {
            // Recreate main page to update translations
            Device.BeginInvokeOnMainThread(async () =>
            {
                foreach (var page in Navigation.NavigationStack)
                {
                    (page as IDisposable)?.Dispose();
                }

                for (var i = 0; i < Navigation.ModalStack.Count; i++)
                {
                    var page = await Navigation.PopModalAsync();
                    (page as IDisposable)?.Dispose();
                }

                Navigation.InsertPageBefore(!UsagePrivilege
                    ? new LoginPage(Container.Resolve<ILoginPageViewModel>())
                    : CreateMainPage(), Navigation.NavigationStack.First());
                await Navigation.PopToRootAsync();

                if (!UsagePrivilege)
                {
                    masterDetailPage.IsPresented = false;
                }
            });
        }

        private MasterDetailPage CreateMasterDetailPage(Page detailPage)
        {
            var masterPageVm = Container.Resolve<MasterPageViewModel>();

            masterPageVm.LogoutSuccessful += UpdateNavigation;

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
                BarTextColor = Color.FloralWhite,
                
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
            builder.RegisterModule<LoginModule>();

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
