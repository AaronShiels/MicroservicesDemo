using Autofac;
using MicroserviceDemo.DomainA.Service.Configuration;
using Topshelf;

namespace MicroserviceDemo.DomainA.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = ContainerConfiguration.Create();

            HostFactory.Run(hostConfig =>
            {
                hostConfig.Service(hostSettings => container.Resolve<DomainAServiceControl>());
            });
        }
    }
}
