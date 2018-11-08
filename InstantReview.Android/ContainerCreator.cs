using System;
using Autofac;
using InstantReview.Droid.Dialogs;
using InstantReview.Droid.Receivers;
using InstantReview.Receivers;


namespace InstantReview.Droid
{
    public static class ContainerCreator
    {
        public static ContainerBuilder CreateContainerBuilder(MainActivity activity, ShareIntentReceiver myReceiver)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<DialogService>().As<IDialogService>().SingleInstance();
            builder.RegisterType<UriTool>().AsSelf().SingleInstance();
            builder.RegisterType<SettingsStorage>().As<ISettingsStorage>().SingleInstance();
            builder.RegisterType<ShareIntentReceiver>().As<IShareIntentReceiver>().SingleInstance();
            builder.RegisterInstance(myReceiver).AsImplementedInterfaces();

            return builder;
        }
    }
}
