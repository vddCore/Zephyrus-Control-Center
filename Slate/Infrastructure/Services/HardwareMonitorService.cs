using System.ComponentModel;
using Glitonea.Mvvm.Messaging;
using Slate.Model.Messaging;

namespace Slate.Infrastructure.Services
{
    public class HardwareMonitorService : IHardwareMonitorService, INotifyPropertyChanged
    {
        private readonly IAsusHalService _asusHalService;
        
        public event PropertyChangedEventHandler? PropertyChanged;

        public int ProcessorFanRPM { get; private set; }
        public int ProcessorTemperature { get; private set; }
        
        public int GraphicsFanRPM { get; private set; }
        public int GraphicsTemperature { get; private set; }

        public HardwareMonitorService(IAsusHalService asusHalService)
        {
            _asusHalService = asusHalService;

            Message.Subscribe<GlobalTickMessage>(this, OnGlobalTick);
        }

        private void OnGlobalTick(GlobalTickMessage _)
        {
            if (!_asusHalService.IsAcpiSessionOpen)
                return;
            
            ProcessorFanRPM = _asusHalService.ReadCpuFanSpeed();
            ProcessorTemperature = (int)_asusHalService.ReadCpuTemperatureCelsius();
            
            GraphicsFanRPM = _asusHalService.ReadGpuFanSpeed();
            GraphicsTemperature = (int)_asusHalService.ReadGpuTemperatureCelsius();
        }
    }
}