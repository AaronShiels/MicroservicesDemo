using Autofac;
using MicroservicesDemo.System.Diagnostics;

namespace MicroservicesDemo.System
{
    public class SystemModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Heartbeat>();
        }
    }
}
