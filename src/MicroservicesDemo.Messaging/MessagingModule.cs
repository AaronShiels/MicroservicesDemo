using Autofac;
using MassTransit;
using MicroservicesDemo.Messaging.Bus;
using System.Reflection;
using Module = Autofac.Module;

namespace MicroservicesDemo.Messaging
{
    public class MessagingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterConsumers(Assembly.GetExecutingAssembly());
            builder.RegisterBus();
        }
    }
}
