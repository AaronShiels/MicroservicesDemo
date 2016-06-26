using System.Threading.Tasks;
using MassTransit;
using MicroservicesDemo.DomainA.Messages;
using Serilog;

namespace MicroservicesDemo.DomainB.Service.Consumers
{
    public class SomethingDoneConsumer : IConsumer<SomethingDoneEvent>
    {
        private readonly ILogger _log;
        private readonly IBus _bus;

        public SomethingDoneConsumer(ILogger log, IBus bus)
        {
            _log = log;
            _bus = bus;
        }

        public async Task Consume(ConsumeContext<SomethingDoneEvent> context)
        {
            var @event = context.Message;

            _log.Debug("Received event {messageType}", @event.GetType());

            _log.Information("{thing} was done by {person}", @event.ThingThatWasDone, @event.PersonThatDidIt);

            var cash = (int)@event.Quality * 1000;

            _log.Information("{person} was given {cash} because {thing} was {quality}", @event.PersonThatDidIt, cash, @event.ThingThatWasDone, @event.Quality);
        }
    }
}
