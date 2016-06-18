using System;
using Topshelf;

namespace MicroservicesDemo.ServiceA
{
    public class ServiceA : ServiceControl
    {
        private readonly Heartbeat _heartbeat;

        public ServiceA(Heartbeat heartbeat)
        {
            _heartbeat = heartbeat;
        }

        public bool Start(HostControl hostControl)
        {
            Console.WriteLine($"[{DateTime.Now.ToLongDateString()}] Service A started.");

            _heartbeat.Start();

            return true;
        }
        
        public bool Stop(HostControl hostControl)
        {
            Console.WriteLine($"[{DateTime.Now.ToLongDateString()}] Service A stopped.");

            _heartbeat.Stop();

            return true;
        }
    }
}
