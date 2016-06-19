﻿using Autofac;
using MassTransit;
using MassTransit.AzureServiceBusTransport;
using Microsoft.ServiceBus;
using System;
using ILogger = Serilog.ILogger;

namespace MicroservicesDemo.Messaging.Bus
{
    public static class RegistrationExtensions
    {
        public static void RegisterBus(this ContainerBuilder builder)
        {
            builder.Register(ctx =>
            {
                var busHost = ctx.ResolveKeyed<string>("Bus.Host");
                var busKey = ctx.ResolveKeyed<string>("Bus.Key");
                var projectName = ctx.ResolveKeyed<string>("Project.Name");
                var logger = ctx.Resolve<ILogger>();

                return MassTransit.Bus.Factory.CreateUsingAzureServiceBus(busConfig =>
                {
                    var host = busConfig.Host(busHost, hostConfig =>
                    {
                        hostConfig.TokenProvider = TokenProvider.CreateSharedAccessSignatureTokenProvider("RootManageSharedAccessKey", busKey);
                        hostConfig.OperationTimeout = TimeSpan.FromSeconds(5);
                    });

                    busConfig.ReceiveEndpoint(host, projectName, epConfig =>
                    {
                        epConfig.LoadFrom(ctx);
                    });

                    if (logger != null)
                        busConfig.UseSerilog(logger);
                });
            })
                .SingleInstance()
                .As<IBusControl>()
                .As<IBus>();
        }
    }
}