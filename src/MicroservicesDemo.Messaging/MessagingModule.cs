using System.Reflection;
using Autofac;
using MassTransit;
using Module = Autofac.Module;

namespace MicroservicesDemo.Messaging
{
    public class MessagingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterConsumers(Assembly.GetEntryAssembly());
            builder.RegisterBus();
        }
    }
}