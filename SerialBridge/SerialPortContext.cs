using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialBridge
{
    public class SerialPortContext
    {
        private string _comPort;
        private SerialPort _port;
        public SerialPortContext(string comPort)
        {
            _comPort = comPort;
        }

        ~SerialPortContext()
        {
            Stop();
        }

        public void Start()
        {
            _port = new SerialPort(_comPort);
            _port.Open();
            _port.DataReceived += _port_DataReceived;
        }

        private void _port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var buffer = new byte[_port.ReadBufferSize];
            _port.Read(buffer, 0, _port.BytesToRead);
            var data = Convert.ToString(buffer);
            Console.WriteLine(data);
        }

        public void Send(string data)
        {
            if(_port.IsOpen)
            {
                _port.Write(data);
            }
        }

        public void Stop()
        {
            _port.Close();
        }
    }
}
