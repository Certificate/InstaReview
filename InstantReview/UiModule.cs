using System;
using Autofac;
using InstantReview.ViewModels;

namespace InstantReview
{
    public class UiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<MainPageViewModel>().AsSelf().SingleInstance();
            builder.RegisterType<QuestionPageViewModel>().AsSelf().SingleInstance();
            builder.RegisterType<ThankYouPageViewModel>().AsSelf().SingleInstance();
            builder.RegisterType<LoginPageViewModel>().As<ILoginPageViewModel>().SingleInstance();
            builder.RegisterType<MasterPageViewModel>().AsSelf().SingleInstance();
            builder.RegisterType<PageFactory>().As<IPageFactory>().SingleInstance();
            builder.RegisterType<ReviewPageViewModel>().As<IReviewPageViewModel>().SingleInstance();

            // Not really part of UI but what the heck
            builder.RegisterType<ReviewDataCollector>().As<IReviewDataCollector>().SingleInstance();
        }
    }
}
