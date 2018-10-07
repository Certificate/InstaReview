using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Autofac;
using Common.Logging;
using InstantReview.Droid.Logging;
using LogManager = Common.Logging.LogManager;

namespace InstantReview.Droid
{
    [Activity(
        Label = "InstantReview", 
        Icon = "@mipmap/icon", 
        Theme = "@style/MainTheme.Splash", 
        MainLauncher = true, 
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        internal static MainActivity Instance { get; private set; }
        
        static MainActivity()
        {
            LogManager.Adapter = new LogCatFactoryAdapter(
                "MaintenanceTool", LogLevel.Debug, true, true, true, "yyyy-MM-dd HH:mm:ss.fff");
        }

        protected override void OnCreate(Bundle bundle)
        {
            Instance = this;
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(bundle);

            var builder = new ContainerBuilder();


            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App(ContainerCreator.CreateContainerBuilder(this)));
        }
    }
}

