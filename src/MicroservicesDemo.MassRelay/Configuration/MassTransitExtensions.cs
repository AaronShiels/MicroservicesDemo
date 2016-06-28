using System.Threading.Tasks;
using Autofac;
using MassTransit;
using MicroservicesDemo.DomainA.Messages;

namespace MicroservicesDemo.MassRelay.Configuration
{
    public static class MassTransitExtensions
    {
        public static void Relay(this IReceiveEndpointConfigurator epConfig, ILifetimeScope lifetimeScope, BusType destionationBusType)
        {
            epConfig.Handler<DoSomethingCommand>(async messageContext => await RelayMessage(messageContext.Message, lifetimeScope, destionationBusType));
        }

        private static async Task RelayMessage<T>(T message, ILifetimeScope scope, BusType destionationBusType) where T : class
        {
            using (var innserScope = scope.BeginLifetimeScope())
            {
                var destionationBus = innserScope.ResolveKeyed<IBus>(destionationBusType);
                await destionationBus.Publish(message);
            }
        }
    }
}