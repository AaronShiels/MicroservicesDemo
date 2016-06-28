using System.Threading.Tasks;
using MassTransit;
using MicroservicesDemo.DomainA.Messages;

namespace MicroservicesDemo.MassRelay.Consumers
{
    public abstract class RelayConsumer : IConsumer<DoSomethingCommand>
    {
        protected readonly IBus DestinationBus;

        public virtual async Task Consume(ConsumeContext<DoSomethingCommand> context)
        {
            await DestinationBus.Publish(context.Message);
        }
    }
}