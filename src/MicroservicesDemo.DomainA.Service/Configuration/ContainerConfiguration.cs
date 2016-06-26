using Autofac;
using MicroservicesDemo.Messaging;
using MicroservicesDemo.System;
using MicroservicesDemo.Diagnostics;

namespace MicroservicesDemo.DomainA.Service.Configuration
{
    public static class ContainerConfiguration
    {
        public static IContainer Create()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<SystemModule>();
            builder.RegisterModule<MessagingModule>();
            builder.RegisterModule<DiagnosticsModule>();

            builder.RegisterType<DomainAServiceControl>();

            return builder.Build();
        }
    }
}