using Autofac;
using MicroservicesDemo.DomainA.Service.Configuration;
using Topshelf;

namespace MicroservicesDemo.DomainA.Service
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var container = ContainerConfiguration.Create();

            HostFactory.Run(hostConfig => { hostConfig.Service(hostSettings => container.Resolve<DomainAServiceControl>()); });
        }
    }
}