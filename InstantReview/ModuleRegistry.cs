using System;
using Autofac;
using InstantReview.ViewModels;

namespace InstantReview
{
    public class ModuleRegistry : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MainPageViewModel>().AsSelf().SingleInstance();
        }
    }
}
