using System;
using System.Collections.Generic;
using System.Text;
using OpenHardwareMonitor;
using OpenHardwareMonitor.Hardware;

namespace HardwareMonitorDLLWrapper
{
    public class HardwareManager
    {
        private Computer _computer;
        public HardwareManager()
        {
            _computer = new Computer();
            _computer.GPUEnabled = true;
            _computer.CPUEnabled = true;
            _computer.Open();
        }

        ~HardwareManager()
        {
            if(_computer != null)
            {
                _computer.Close();
            }
        }

        public TemperatureSensor GetTemperatureSensor(HardwareType type)
        {
            var hardware = GetHardware(type);
            if(hardware == null)
            {
                return null;
            }
            return new TemperatureSensor(hardware);
        }

        private IHardware GetHardware(HardwareType type)
        {
            var hardwareType = HardwareTypeCaster.Cast(type);
            foreach(var hardware in _computer.Hardware)
            {
                if(hardware.HardwareType == hardwareType)
                {
                    hardware.Update();
                    return hardware;
                }
            }

            return null;
        }
    }
}
