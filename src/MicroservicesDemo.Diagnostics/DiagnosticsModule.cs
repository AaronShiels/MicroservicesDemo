using Autofac;

namespace MicroservicesDemo.Diagnostics
{
    public class DiagnosticsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterLogger();

            builder.RegisterType<ApplicationDiagnostics>();
        }
    }
}
