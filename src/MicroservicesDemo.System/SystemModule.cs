using Autofac;

namespace MicroservicesDemo.System
{
    public class SystemModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterSettings();
        }
    }
}