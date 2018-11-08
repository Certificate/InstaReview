﻿using System;
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
            builder.RegisterType<LoginPageViewModel>().AsSelf().SingleInstance();
            builder.RegisterType<MasterPageViewModel>().AsSelf().SingleInstance();
            builder.RegisterType<PageFactory>().As<IPageFactory>().SingleInstance();
            builder.RegisterType<ReviewPageViewModel>().As<IReviewPageViewModel>().SingleInstance();
        }
    }
}
