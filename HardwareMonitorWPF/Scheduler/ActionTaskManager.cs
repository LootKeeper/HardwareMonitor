using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HardwareMonitorWPF.Scheduler
{
    internal class ActionTaskManager
    {
        private IActionTask[] _tasks;
        private Timer _timer;

        public ActionTaskManager()
        {
            _tasks = new IActionTask[0];
        }
        public void Add(IActionTask task)
        {
            _tasks = _tasks.Append(task).ToArray();
            SetupTimer();
        }

        public void RemoveAll()
        {
            _tasks = new IActionTask[0];
            _timer.Dispose();
        }

        private void SetupTimer()
        {
            if(_timer == null)
            {
                _timer = new Timer(RunAll, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
            }
        }

        private void RunAll(object state)
        {
            foreach(IActionTask task in _tasks)
            {
                if(task.NeedToRun() && !task.IsRunning)
                {
                    task.Run();
                }
            }
        }
    }
}
