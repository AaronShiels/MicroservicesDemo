using System;
using System.Threading.Tasks;
using MassTransit;
using MicroservicesDemo.DomainA.Messages;
using Serilog;

namespace MicroservicesDemo.DomainA.Service.Consumers
{
    public class DoSomethingConsumer : IConsumer<DoSomethingCommand>
    {
        private readonly ILogger _log;
        private readonly IBus _bus;

        public DoSomethingConsumer(ILogger log, IBus bus)
        {
            _log = log;
            _bus = bus;
        }

        public async Task Consume(ConsumeContext<DoSomethingCommand> context)
        {
            var command = context.Message;

            _log.Debug("Received command {messageType}", command.GetType());

            _log.Information("{person} is performing {thing}", command.PersonDoingThing, command.ThingToDo);

            var quality = command.PersonDoingThing.Contains("Aaron") ? Quality.FullySick : Quality.Lame;

            _log.Information("{thing} was {quality}!", command.ThingToDo, quality);

            var @event = new SomethingDoneEvent
            {
                ThingThatWasDone = command.ThingToDo,
                PersonThatDidIt = command.PersonDoingThing,
                Quality = quality
            };

            await _bus.Publish(@event);

            _log.Debug("Published event {messageType}", @event.GetType());
        }
    }
}
