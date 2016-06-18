using System;
using System.Timers;

namespace MicroservicesDemo.ServiceA
{
    public class Heartbeat
    {
        private Timer _timer;
        private const int Interval = 5000;

        public Heartbeat()
        {
            _timer = new Timer(Interval);
            _timer.AutoReset = true;
            _timer.Elapsed += Callback;
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }

        private void Callback(object o, ElapsedEventArgs e)
        {
            Console.WriteLine($"[{DateTime.Now.ToLongDateString()}] ping.");
        }
    }
}
