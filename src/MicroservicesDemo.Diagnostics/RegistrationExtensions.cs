using Autofac;
using Serilog;
using MicroservicesDemo.DomainA.Service.Configuration;

namespace MicroservicesDemo.Diagnostics
{
    internal static class RegistrationExtensions
    {
        public static void RegisterLogger(this ContainerBuilder builder)
        {
            builder.Register(ctx =>
            {
                var applicationId = ctx.ResolveKeyed<string>("Project.Name");
                var logLevel = ctx.ResolveKeyed<string>("Log.Level");
                var logFilePath = ctx.ResolveKeyed<string>("Log.FilePath");
                var azureStorageConnectionString = ctx.ResolveKeyed<string>("Log.AzureStorageConnection");

                return LogConfiguration.Create(applicationId, logLevel, logFilePath, azureStorageConnectionString);
            })
                   .SingleInstance()
                   .As<ILogger>();
        }
    }
}
