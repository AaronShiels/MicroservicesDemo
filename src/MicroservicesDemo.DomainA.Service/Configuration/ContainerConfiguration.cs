using Autofac;
using Autofac.Extras.Attributed;
using MicroservicesDemo.DomainA.Service.Configuration;
using MicroservicesDemo.Messaging;
using MicroservicesDemo.System;
using Serilog;

namespace MicroserviceDemo.DomainA.Service.Configuration
{
    public static class ContainerConfiguration
    {
        public static IContainer Create()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<SystemModule>();
            builder.RegisterModule<MessagingModule>();

            builder.Register(ctx => LogConfiguration.Create())
                .SingleInstance()
                .As<ILogger>();

            builder.RegisterType<DomainAServiceControl>()
                .WithAttributeFilter();

            return builder.Build();
        }
    }
}
