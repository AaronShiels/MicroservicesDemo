using Autofac;
using MicroservicesDemo.System;

namespace MicroserviceDemo.DomainA.Service.Configuration
{
    public static class ContainerConfiguration
    {
        public static IContainer Create()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<SystemModule>();
            builder.RegisterType<DomainAServiceControl>();

            return builder.Build();
        }
    }
}
