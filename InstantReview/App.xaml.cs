using System;
using Autofac;
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

        public App(ContainerBuilder containerBuilder)
        {
            InitializeComponent();

            Container = CreateContainer(containerBuilder);

            MainPage = new MainPage(Container.Resolve<MainPageViewModel>());
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
            builder.RegisterModule<ModuleRegistry>();

            builder.RegisterInstance(this).As<IContainerResolver>();

            return builder.Build();
        }

        public T Resolve<T>()
        {
            return Container.Resolve<T>();
        }
    }
}
