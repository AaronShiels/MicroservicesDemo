using System;
using Microsoft.WindowsAzure.Storage;
using Serilog;
using Serilog.Events;

namespace MicroservicesDemo.DomainA.Service.Configuration
{
    public static class LogConfiguration
    {
        public static ILogger Create(string applicationId, string logLevel, string rollingFilePath, string azureStorageConnectionString)
        {
            var resolvedLogLevel = MapLogLevel(logLevel);

            var logConfig = new LoggerConfiguration()
                .MinimumLevel.Is(resolvedLogLevel)
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

        private static LogEventLevel MapLogLevel(string logLevelString)
        {
            LogEventLevel logLevel;
            return Enum.TryParse(logLevelString, true, out logLevel) ? logLevel : LogEventLevel.Information;
        }
    }
}