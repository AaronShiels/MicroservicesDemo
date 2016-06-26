using Autofac;
using MicroservicesDemo.Diagnostics;
using MicroservicesDemo.Messaging;
using MicroservicesDemo.System;

namespace MicroservicesDemo.DomainB.Service.Configuration
{
    public static class ContainerConfiguration
    {
        public static IContainer Create()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<SystemModule>();
            builder.RegisterModule<MessagingModule>();
            builder.RegisterModule<DiagnosticsModule>();

            builder.RegisterType<DomainBServiceControl>();

            return builder.Build();
        }
    }
}
