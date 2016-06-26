using MassTransit;
using MicroservicesDemo.Diagnostics;
using Topshelf;

namespace MicroservicesDemo.DomainB.Service
{
    public class DomainBServiceControl : ServiceControl
    {
        private readonly IBusControl _busControl;
        private readonly ApplicationDiagnostics _diagnostics;

        public DomainBServiceControl(ApplicationDiagnostics diagnostics, IBusControl busControl)
        {
            _busControl = busControl;
            _diagnostics = diagnostics;
        }

        public bool Start(HostControl hostControl)
        {
            _diagnostics.Start();
            _busControl.Start();

            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            _diagnostics.Stop();
            _busControl.Stop();

            return true;
        }
    }
}
