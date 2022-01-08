using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareMonitorWPF.Context
{
    internal interface IContext
    {
        void Invoke(Action action);
    }
}
