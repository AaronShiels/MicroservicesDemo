using Serilog;

namespace MicroservicesDemo.DomainA.Service.Configuration
{
    public static class LogConfiguration
    {
        public static ILogger Create()
        {
            return Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.ColoredConsole()
                .WriteTo.RollingFile(@"logs\{Date}.txt")
                .CreateLogger();
        }
    }
}
