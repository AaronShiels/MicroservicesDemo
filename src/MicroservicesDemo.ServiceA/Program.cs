using Autofac;
using Topshelf;

namespace MicroservicesDemo.ServiceA
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = CreateContainer();

            HostFactory.Run(hostConfig =>
            {
                hostConfig.Service(hostSettings => container.Resolve<ServiceA>());
            });
        }

        private static IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ServiceA>();
            builder.RegisterType<Heartbeat>();

            return builder.Build();
        }
    }
}
