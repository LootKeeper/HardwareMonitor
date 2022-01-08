using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareMonitorWPF.Scheduler
{
    internal interface IActionTask
    {
        bool IsRunning { get;}
        bool NeedToRun();
        void Run();
    }
}
