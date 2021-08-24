using System;
using System.Threading;

namespace ScriptLauncher.TimerFeatures
{
    public class TimerManager
    {
        private readonly Timer _timer;
        private readonly Action _action;

        private DateTime TimerStarted { get; }

        public TimerManager(Action action)
        {
            _action = action;
            var autoResetEvent = new AutoResetEvent(false);
            _timer = new Timer(Execute, autoResetEvent, 1000, 2000);
            TimerStarted = DateTime.Now;
        }

        private void Execute(object stateInfo)
        {
            _action();

            if ((DateTime.Now - TimerStarted).Seconds > 60)
            {
                _timer.Dispose();
            }
        }
    }
}