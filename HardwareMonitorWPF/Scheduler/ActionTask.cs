using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareMonitorWPF.Scheduler
{
    internal class ActionTask : IActionTask
    {
        public bool IsRunning => _isRunning;
        private bool _isRunning;

        private Action _action;
        private TimeSpan _interval;
        private DateTime _lastRun;

        public ActionTask(Action action): this(action, TimeSpan.FromSeconds(1))
        {
        }

        public ActionTask(Action action, TimeSpan interval)
        {
            this._action = action;
            this._interval = interval;
        }

        public bool NeedToRun()
        {
            return DateTime.UtcNow > _lastRun + _interval;
        }

        public void Run()
        {
            _action();
            _lastRun = DateTime.UtcNow;
        }
    }
}
