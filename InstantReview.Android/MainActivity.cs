using System;
using System.IO;
using System.Threading.Tasks;
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
        LaunchMode = LaunchMode.SingleInstance,
        ScreenOrientation = ScreenOrientation.Portrait,
        ConfigurationChanges = ConfigChanges.ScreenSize)]
    [IntentFilter(new[] { Intent.ActionSend }, Categories = new[] { Intent.CategoryDefault }, DataMimeType = @"image/*")]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        internal static MainActivity Instance { get; private set; }
        private ShareIntentReceiver myReceiver;


        static MainActivity()
        {
            LogManager.Adapter = new LogCatFactoryAdapter(
                "InstaReview", LogLevel.Debug, true, true, true, "yyyy-MM-dd HH:mm:ss.fff");
        }

        protected override void OnNewIntent(Intent intent)
        {
            myReceiver.OnReceive(this, intent);
        }

        protected override void OnCreate(Bundle bundle)
        {
            Instance = this;

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            myReceiver = new ShareIntentReceiver();

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App(ContainerCreator.CreateContainerBuilder(this, myReceiver)));

            myReceiver.RequestPermision();
            myReceiver.OnReceive(this, Intent);
        }
        
        // Field, property, and method for Picture Picker
        public static readonly int PickImageId = 1000;

        public TaskCompletionSource<Stream> PickImageTaskCompletionSource { set; get; }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent intent)
        {
            base.OnActivityResult(requestCode, resultCode, intent);

            if (requestCode == PickImageId)
            {
                if ((resultCode == Result.Ok) && (intent != null))
                {
                    Android.Net.Uri uri = intent.Data;
                    myReceiver.ImageFromUri(uri);
                }
            }
        }

    }
}

