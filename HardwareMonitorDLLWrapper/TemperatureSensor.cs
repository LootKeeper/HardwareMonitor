using OpenHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareMonitorDLLWrapper
{
    public class TemperatureSensor
    {
        private IHardware _hardware;
        private ISensor _sensor;

        public TemperatureSensor(IHardware hardware)
        {
            this._hardware = hardware;
            _sensor = hardware.Sensors
                        .FirstOrDefault(s => s.SensorType == SensorType.Temperature && 
                        s.Name.Contains("CPU Package")) ?? 
                     hardware.Sensors.First(s => s.SensorType == SensorType.Temperature);
        }

        public TemperatureSensor(IHardware hardware, ISensor sensor)
        {
            this._hardware = hardware;
            this._sensor = sensor;
        }


        public bool HasSensor()
        {
            return _sensor != null;
        }

        public double GetTemperature()
        {
            if (this.HasSensor())
            {
                _hardware.Update();
                return Convert.ToInt32(_sensor.Value.GetValueOrDefault());
            }

            return 0.0;
        }
    }
}
