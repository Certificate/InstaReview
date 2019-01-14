using Autofac;

namespace InstantReview.Login
{
    public class LoginModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ConnectionHandler>().As<IConnectionHandler>().SingleInstance();
        }
    }
}