using Microsoft.WindowsAzure.Storage;
using Serilog;

namespace MicroservicesDemo.DomainA.Service.Configuration
{
    public static class LogConfiguration
    {
        public static ILogger Create(string applicationId, string rollingFilePath, string azureStorageConnectionString)
        {
            var logConfig = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.WithProperty("Application ID", applicationId)
                .WriteTo.ColoredConsole();

            if (!string.IsNullOrEmpty(rollingFilePath))
                logConfig.WriteTo.RollingFile(rollingFilePath);

            if (!string.IsNullOrEmpty(azureStorageConnectionString))
            {
                var azureStorage = CloudStorageAccount.Parse(azureStorageConnectionString);
                logConfig.WriteTo.AzureTableStorage(azureStorage);
            }

            return Log.Logger = logConfig.CreateLogger();
        }
    }
}