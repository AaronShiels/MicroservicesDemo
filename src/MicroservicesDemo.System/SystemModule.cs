using Autofac;
using Autofac.Extras.Attributed;
using MicroservicesDemo.System.Diagnostics;
using MicroservicesDemo.System.Settings;

namespace MicroservicesDemo.System
{
    public class SystemModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterSettings();

            builder.RegisterType<ApplicationDiagnostics>()
                .WithAttributeFilter();
        }
    }
}
