using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Autofac;
using Common.Logging;
using InstantReview.Droid.Logging;
using InstantReview.Droid.Receivers;
using LogManager = Common.Logging.LogManager;

namespace InstantReview.Droid
{
    [Activity(
        Label = "InstaReview", 
        Icon = "@mipmap/icon", 
        Theme = "@style/MainTheme.Splash", 
        MainLauncher = true, 
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    [IntentFilter(new[] { Intent.ActionSend }, Categories = new[] { Intent.CategoryDefault }, DataMimeType = @"image/*")]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        internal static MainActivity Instance { get; private set; }
        private ShareIntentReceiver myReceiver;
        private IntentFilter intentFilter;
        private ImageOperations imageOperations;

        static MainActivity()
        {
            LogManager.Adapter = new LogCatFactoryAdapter(
                "InstaReview", LogLevel.Debug, true, true, true, "yyyy-MM-dd HH:mm:ss.fff");
        }

        protected override void OnCreate(Bundle bundle)
        {
            Instance = this;

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            intentFilter = new IntentFilter(Intent.ActionSend);
            myReceiver = new ShareIntentReceiver();
            imageOperations = new ImageOperations(myReceiver);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App(ContainerCreator.CreateContainerBuilder(this, imageOperations)));

            
            myReceiver.OnReceive(this, Intent);
        }
    }
}

