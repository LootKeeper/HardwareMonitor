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
        private IHardware hardware;
        private ISensor sensor;

        public TemperatureSensor(IHardware hardware)
        {
            this.hardware = hardware;
            sensor = hardware.Sensors.First(s => s.SensorType == SensorType.Temperature && sensor.Name.Contains("CPU Package")) ?? hardware.Sensors.First(s => s.SensorType == SensorType.Temperature);
        }

        public TemperatureSensor(IHardware hardware, ISensor sensor)
        {
            this.hardware = hardware;
            this.sensor = sensor;
        }


        public bool HasSensor()
        {
            return sensor != null;
        }

        public double GetTemperature()
        {
            if (this.HasSensor())
            {
                hardware.Update();
                return sensor.Value.GetValueOrDefault();
            }

            return 0.0;
        }
    }
}
