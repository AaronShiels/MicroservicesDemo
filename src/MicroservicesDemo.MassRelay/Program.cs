using Autofac;
using MicroservicesDemo.MassRelay.Configuration;
using Topshelf;

namespace MicroservicesDemo.MassRelay
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var container = ContainerConfiguration.Create();

            HostFactory.Run(hostConfig => { hostConfig.Service(hostSettings => container.Resolve<MassRelayServiceControl>()); });
        }
    }
}
