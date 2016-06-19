using Microsoft.WindowsAzure.Storage;
using Serilog;

namespace MicroservicesDemo.DomainA.Service.Configuration
{
    public static class LogConfiguration
    {
        public static ILogger Create(string rollingFilePath, string azureStorageConnectionString)
        {
            var azureStorage = CloudStorageAccount.Parse(azureStorageConnectionString);

            return Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.ColoredConsole()
                .WriteTo.RollingFile(rollingFilePath)
                .WriteTo.AzureTableStorage(azureStorage)
                .CreateLogger();
        }
    }
}
