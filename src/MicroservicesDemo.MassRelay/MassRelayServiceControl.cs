using MassTransit;
using Topshelf;

namespace MicroservicesDemo.MassRelay
{
    public class MassRelayServiceControl : ServiceControl
    {
        private readonly IBusControl _azureBus;
        private readonly IBusControl _rabbitMqBus;

        public MassRelayServiceControl(IBusControl azureBus, IBusControl rabbitMqBus)
        {
            _azureBus = azureBus;
            _rabbitMqBus = rabbitMqBus;
        }

        public bool Start(HostControl hostControl)
        {
            _azureBus.Start();
            _rabbitMqBus.Start();

            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            _azureBus.Stop();
            _rabbitMqBus.Stop();

            return true;
        }
    }
}