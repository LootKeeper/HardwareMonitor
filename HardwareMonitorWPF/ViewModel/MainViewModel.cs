using HardwareMonitorWPF.Commands;
using HardwareMonitorDLLWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using HardwareMonitorWPF.Scheduler;
using HardwareMonitorWPF.Context;
using SerialBridge;

namespace HardwareMonitorWPF.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public string CpuTemp
        {
            get => IsLoading ? "Loading..." : _cpuTemp;
            set { _cpuTemp = value; OnPropertyChanged(); }
        }
        private string _cpuTemp;

        public string GpuTemp
        {
            get => IsLoading ? "Loading..." : _gpuTemp;
            set { _gpuTemp = value; OnPropertyChanged(); }
        }
        private string _gpuTemp;

        public bool IsLoading
        {
            get => _isLoading;
            set { _isLoading = value; OnPropertyChanged(); }
        }
        private bool _isLoading;

        public string[] COMPorts
        {
            get => _comPorts;
            set { _comPorts = value; OnPropertyChanged(); }
        }
        private string[] _comPorts;

        public string SelectedCOMPort
        {
            get => _selectedCOMPort;
            set 
            { 
                _selectedCOMPort = value; 
                OnPropertyChanged(); 
                HandleConnectClick.RaiseCanExecuteChanged(); 
            }
        }
        private string _selectedCOMPort;

        public bool COMConnected
        {
            get => _comConnected;
            set 
            { 
                _comConnected = value; 
                OnPropertyChanged();
                HandleConnectClick.RaiseCanExecuteChanged();
            }
        }
        private bool _comConnected;

        public IDelegateCommand HandleConnectClick { get; set; }

        private IContext _context;
        private HardwareManager _hardwareManager;
        private ActionTaskManager _actionTaskManager;
        private SerialPortManager _serialPortManager;
        private SerialPortContext _serialPortContext;

        public MainViewModel()
        {
            HandleConnectClick = new DelegateCommand(ConnectToSerialPort, CanConnectToSerialPort);
            _context = new RenderContext();
            Setup();
        }

        private void Setup()
        {
            SetupTemperatureWatchers();
            SetupComPorts();
        }
        private void SetupTemperatureWatchers()
        {
            IsLoading = true;
            _hardwareManager = new HardwareManager();

            Task.Run(() =>
            {
                var cpuSensor = _hardwareManager.GetTemperatureSensor(HardwareType.CPU);
                var gpuSensor = _hardwareManager.GetTemperatureSensor(HardwareType.GpuNvidia) ?? _hardwareManager.GetTemperatureSensor(HardwareType.GpuAti);

                _actionTaskManager = new ActionTaskManager();
                _actionTaskManager.Add(new ActionTask(() =>
                {
                    _context.Invoke(() => { CpuTemp = cpuSensor.GetTemperature().ToString(); });
                }));

                if (gpuSensor != null)
                {
                    _actionTaskManager.Add(new ActionTask(() =>
                    {
                        _context.Invoke(() => { GpuTemp = gpuSensor.GetTemperature().ToString(); });
                    }));
                }
                IsLoading = false;
            });           
        }
        private void SetupComPorts()
        {
            _serialPortManager = new SerialPortManager();
            COMPorts = _serialPortManager.GetAllSerialPorts();
        }

        private void ConnectToSerialPort()
        {
            try
            {
                _serialPortContext = new SerialPortContext(SelectedCOMPort);
                _serialPortContext.Start();
                _actionTaskManager.Add(new ActionTask(() =>
                {
                    var data = $"cpu: {CpuTemp}. gpu: {GpuTemp}.";
                    _serialPortContext.Send(data);
                }));
                COMConnected = true;
            }
            catch(Exception ex)
            {
                COMConnected = false;
            }
        }

        private bool CanConnectToSerialPort()
        {
            return !String.IsNullOrEmpty(SelectedCOMPort) && !COMConnected;
        }
    }
}
