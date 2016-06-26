using System.Timers;
using Serilog;

namespace MicroservicesDemo.Diagnostics
{
    public class ApplicationDiagnostics
    {
        private readonly ILogger _log;
        private readonly Timer _timer;
        private const int Interval = 5000;

        public ApplicationDiagnostics(ILogger log)
        {
            _log = log;
            _timer = new Timer(Interval) {AutoReset = true};
            _timer.Elapsed += Callback;
        }

        public void Start(bool beginHeartbeat = true)
        {
            _log.Information("Application started.");

            _timer.Start();
        }

        public void Stop()
        {
            _log.Information("Application stopped.");

            _timer.Stop();
        }

        private void Callback(object o, ElapsedEventArgs e)
        {
            _log.Debug("Application heartbeat.");
        }
    }
}