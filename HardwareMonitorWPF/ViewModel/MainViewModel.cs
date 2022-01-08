using HardwareMonitorWPF.Commands;
using OpenHardwareMonitorWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace HardwareMonitorWPF.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public string CpuTemp
        {
            get => _cpuTemp;
            set 
            { 
                _cpuTemp = value;
                OnPropertyChanged();
            }
        }
        private string _cpuTemp;

        public MainViewModel()
        {
            HandleClick = new DelegateCommand(HandleClickAction);
        }
        public ICommand HandleClick { get; set; }
        private void HandleClickAction()
        {
            var hardwareManager = new HardwareManager();
            hardwareManager.GetCpuSensor();
        }
    }
}
