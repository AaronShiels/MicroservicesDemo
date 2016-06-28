using System;
using System.Configuration;
using Autofac;
using MassTransit;
using MassTransit.AzureServiceBusTransport;
using MicroservicesDemo.DomainA.Messages;
using Microsoft.ServiceBus;
using Serilog;
using Serilog.Events;
using HandlerExtensions = MassTransit.HandlerExtensions;

namespace MicroservicesDemo.MassRelay.Configuration
{
    public class ContainerConfiguration
    {
        public static IContainer Create()
        {
            var builder = new ContainerBuilder();

            //Settings
            foreach (var key in ConfigurationManager.AppSettings.AllKeys)
                builder.RegisterInstance(ConfigurationManager.AppSettings[key])
                       .Named<string>(key);

            //Logger
            builder.Register(ctx =>
            {
                var applicationId = ctx.ResolveKeyed<string>("Project.Name");
                var logLevel = ctx.ResolveKeyed<string>("Log.Level");
                var logFilePath = ctx.ResolveKeyed<string>("Log.FilePath");

                var resolvedLogLevel = (LogEventLevel) Enum.Parse(typeof (LogEventLevel), logLevel);

                var logConfig = new LoggerConfiguration().MinimumLevel.Is(resolvedLogLevel)
                                                         .Enrich.WithProperty("Application ID", applicationId)
                                                         .WriteTo.ColoredConsole();

                if (!string.IsNullOrEmpty(logFilePath))
                    logConfig.WriteTo.RollingFile(logFilePath);

                return Log.Logger = logConfig.CreateLogger();
            })
                   .SingleInstance()
                   .As<ILogger>();

            //Azure bus
            builder.Register(ctx =>
            {
                var busHost = ctx.ResolveKeyed<string>("Bus.Azure.Host");
                var busKeyName = ctx.ResolveKeyed<string>("Bus.Azure.KeyName");
                var busKey = ctx.ResolveKeyed<string>("Bus.Azure.Key");
                var projectName = ctx.ResolveKeyed<string>("Project.Name");
                var logger = ctx.Resolve<ILogger>();

                return Bus.Factory.CreateUsingAzureServiceBus(busConfig =>
                {
                    var host = busConfig.Host(busHost, hostConfig =>
                    {
                        hostConfig.TokenProvider = TokenProvider.CreateSharedAccessSignatureTokenProvider(busKeyName, busKey);
                        hostConfig.OperationTimeout = TimeSpan.FromSeconds(5);
                    });

                    busConfig.ReceiveEndpoint(host, projectName, epConfig =>
                    {
                        epConfig.Relay(ctx.Resolve<ILifetimeScope>(), BusType.RabbitMq);
                    });

                    busConfig.UseSerilog(logger);
                });
            })
                   .SingleInstance()
                   .Keyed<IBusControl>(BusType.Azure)
                   .Keyed<IBus>(BusType.Azure);

            //RabbitMq bus
            builder.Register(ctx =>
            {
                var busHost = ctx.ResolveKeyed<string>("Bus.RabbitMq.Host");
                var username = ctx.ResolveKeyed<string>("Bus.RabbitMq.Username");
                var password = ctx.ResolveKeyed<string>("Bus.RabbitMq.Password");
                var projectName = ctx.ResolveKeyed<string>("Project.Name");
                var logger = ctx.Resolve<ILogger>();

                return Bus.Factory.CreateUsingRabbitMq(busConfig =>
                {
                    var host = busConfig.Host(new Uri(busHost), hostConfig =>
                    {
                        hostConfig.Username(username);
                        hostConfig.Password(password);
                        hostConfig.Heartbeat(5);
                    });

                    busConfig.ReceiveEndpoint(host, projectName, epConfig =>
                    {
                        epConfig.Relay(ctx.Resolve<ILifetimeScope>(), BusType.Azure);
                    });

                    busConfig.UseRetry(Retry.Immediate(5));
                    busConfig.UseSerilog(logger);
                });
            })
                   .SingleInstance()
                   .Keyed<IBusControl>(BusType.RabbitMq)
                   .Keyed<IBus>(BusType.RabbitMq);

            //Service Control
            builder.Register(ctx =>
            {
                var rabbitMqBus = ctx.ResolveKeyed<IBusControl>(BusType.RabbitMq);
                var azureBus = ctx.ResolveKeyed<IBusControl>(BusType.Azure);

                return new MassRelayServiceControl(azureBus, rabbitMqBus);
            });

            return builder.Build();
        }
    }
}