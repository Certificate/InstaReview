using System;
using Autofac;
using InstantReview.Droid.Dialogs;
using InstantReview.Droid.Receivers;
using InstantReview.Receivers;


namespace InstantReview.Droid
{
    public static class ContainerCreator
    {
        public static ContainerBuilder CreateContainerBuilder(MainActivity activity, ImageOperations imageOperations)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<DialogService>().As<IDialogService>().SingleInstance();
            builder.RegisterType<ShareIntentReceiver>().As<IShareIntentReceiver>().SingleInstance();
            builder.RegisterType<ImageOperations>().As<IImageOperations>().SingleInstance();
            builder.RegisterInstance(imageOperations).AsImplementedInterfaces();

            return builder;
        }
    }
}
