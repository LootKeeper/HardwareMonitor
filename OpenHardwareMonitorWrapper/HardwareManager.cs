using System;
using System.Collections.Generic;
using System.Text;
using OpenHardwareMonitor;
using OpenHardwareMonitor.Hardware;

namespace OpenHardwareMonitorWrapper
{
    public class HardwareManager
    {
        private Computer _computer;
        public HardwareManager()
        {
            _computer = new Computer();
        }

        public void GetCpuSensor()
        {
            _computer.Open();
            IHardware[] hardwares = _computer.Hardware;
            foreach (var hardware in hardwares)
            {
                var sensors = hardware.Sensors;
            }
        }
    }
}
