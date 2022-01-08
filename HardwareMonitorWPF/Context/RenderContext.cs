using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace HardwareMonitorWPF.Context
{
    internal class RenderContext : IContext
    {
        private Dispatcher _dispatcher;

        public RenderContext() : this(Dispatcher.CurrentDispatcher) { }
        public RenderContext(Dispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }
        public void Invoke(Action action)
        {
            this._dispatcher.Invoke(action);
        }
    }
}
