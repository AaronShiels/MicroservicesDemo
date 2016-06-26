using Autofac;
using MicroservicesDemo.DomainB.Service.Configuration;
using Topshelf;

namespace MicroservicesDemo.DomainB.Service
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var container = ContainerConfiguration.Create();

            HostFactory.Run(hostConfig => { hostConfig.Service(hostSettings => container.Resolve<DomainBServiceControl>()); });
        }
    }
}
