using OpenHardwareMonitorWrapper;

public class Program
{    public static void Main(string[] args)
    {
        var manager = new HardwareManager();
        manager.GetCpuSensor();
    }

}

