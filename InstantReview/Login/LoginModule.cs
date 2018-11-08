using Autofac;

namespace InstantReview.Login
{
    public class LoginModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<LoginHandler>().As<ILoginHandler>().SingleInstance();
        }
    }
}