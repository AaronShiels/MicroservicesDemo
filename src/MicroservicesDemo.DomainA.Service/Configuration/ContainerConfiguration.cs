﻿using Autofac;
using Autofac.Extras.Attributed;
using MicroservicesDemo.Messaging;
using MicroservicesDemo.System;
using Serilog;

namespace MicroservicesDemo.DomainA.Service.Configuration
{
    public static class ContainerConfiguration
    {
        public static IContainer Create()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<SystemModule>();
            builder.RegisterModule<MessagingModule>();

            builder.Register(ctx =>
            {
                var applicationId = ctx.ResolveKeyed<string>("Project.Name");
                var logLevel = ctx.ResolveKeyed<string>("Log.Level");
                var logFilePath = ctx.ResolveKeyed<string>("Log.FilePath");
                var azureStorageConnectionString = ctx.ResolveKeyed<string>("Log.AzureStorageConnection");
                return LogConfiguration.Create(applicationId, logLevel, logFilePath, azureStorageConnectionString);
            })
                   .SingleInstance()
                   .As<ILogger>();

            builder.RegisterType<DomainAServiceControl>()
                   .WithAttributeFilter();

            return builder.Build();
        }
    }
}