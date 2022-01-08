using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HardwareTypeDLL = OpenHardwareMonitor.Hardware;

namespace HardwareMonitorDLLWrapper
{
    public enum HardwareType
    {
        Mainboard,
        SuperIO,
        CPU,
        RAM,
        GpuNvidia,
        GpuAti,
        TBalancer,
        Heatmaster,
        HDD
    }

    public static class HardwareTypeCaster
    {
        public static HardwareTypeDLL.HardwareType Cast(HardwareType type)
        {
            return (HardwareTypeDLL.HardwareType)Enum.Parse(typeof(HardwareTypeDLL.HardwareType), type.ToString());
        }
    }
}
