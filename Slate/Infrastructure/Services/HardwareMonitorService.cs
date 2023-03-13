using System.ComponentModel;
using Glitonea.Mvvm.Messaging;
using Slate.Model.Messaging;

namespace Slate.Infrastructure.Services
{
    public class HardwareMonitorService : IHardwareMonitorService, INotifyPropertyChanged
    {
        private readonly IAsusHalService _asusHalService;
        
        public event PropertyChangedEventHandler? PropertyChanged;

        public int CpuFanRPM { get; private set; }
        public int CpuTemperature { get; private set; }
        
        public int GpuFanRPM { get; private set; }
        public int GpuTemperature { get; private set; }

        public HardwareMonitorService(IAsusHalService asusHalService)
        {
            _asusHalService = asusHalService;

            Message.Subscribe<GlobalTickMessage>(this, OnGlobalTick);
        }

        private void OnGlobalTick(GlobalTickMessage _)
        {
            if (!_asusHalService.IsAcpiSessionOpen)
                return;
            
            CpuFanRPM = _asusHalService.ReadCpuFanSpeed();
            CpuTemperature = (int)_asusHalService.ReadCpuTemperatureCelsius();
            
            GpuFanRPM = _asusHalService.ReadGpuFanSpeed();
            GpuTemperature = (int)_asusHalService.ReadGpuTemperatureCelsius();
        }
    }
}