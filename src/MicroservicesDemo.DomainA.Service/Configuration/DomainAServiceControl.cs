using MicroservicesDemo.System.Diagnostics;
using System;
using Topshelf;

namespace MicroserviceDemo.DomainA.Service.Configuration
{
    public class DomainAServiceControl : ServiceControl
    {
        private readonly Heartbeat _heartbeat;

        public DomainAServiceControl(Heartbeat heartbeat)
        {
            _heartbeat = heartbeat;
        }

        public bool Start(HostControl hostControl)
        {
            Console.WriteLine($"[{DateTime.Now.ToString()}] Service A started.");

            _heartbeat.Start();

            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            Console.WriteLine($"[{DateTime.Now.ToString()}] Service A stopped.");

            _heartbeat.Stop();

            return true;
        }
    }
}
