using System;
using System.Threading.Tasks;
using MassTransit;
using MicroservicesDemo.DomainA.Messages;
using Serilog;

namespace MicroservicesDemo.DomainA.Service.Consumers
{
    public class AddTaskConsumer : IConsumer<AddTaskCommand>
    {
        private readonly ILogger _log;

        public AddTaskConsumer(ILogger log)
        {
            _log = log;
        }

        public async Task Consume(ConsumeContext<AddTaskCommand> context)
        {
            _log.Information("Message consumed! {0}", context.Message);
        }
    }
}
