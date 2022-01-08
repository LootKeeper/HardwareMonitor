using System.IO.Ports;

namespace SerialBridge
{
    public class SerialPortManager
    {
        public string[] GetAllSerialPorts()
        {
            return SerialPort.GetPortNames();   
        }
    }
}
