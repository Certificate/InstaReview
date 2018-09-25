using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Autofac;

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

