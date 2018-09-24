using System;
using Autofac;

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
