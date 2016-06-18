﻿using MassTransit;
using MicroservicesDemo.DomainA.Messages;
using MicroservicesDemo.System.Diagnostics;
using Topshelf;

namespace MicroserviceDemo.DomainA.Service.Configuration
{
    public class DomainAServiceControl : ServiceControl
    {
        private readonly IBusControl _busControl;
        private readonly ApplicationDiagnostics _diagnostics;

        public DomainAServiceControl(ApplicationDiagnostics diagnostics, IBusControl busControl)
        {
            _busControl = busControl;
            _diagnostics = diagnostics;
        }

        public bool Start(HostControl hostControl)
        {
            _diagnostics.Start();
            _busControl.Start();

           _busControl.Publish(new AddTaskCommand
            {
                Name = "Test",
                Severity = AddTaskCommand.TaskServerity.Medium
            });

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
