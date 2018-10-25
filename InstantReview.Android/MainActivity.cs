﻿using System;

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

            
            myReceiver.OnReceive(this, Intent);
        }
    }
}
