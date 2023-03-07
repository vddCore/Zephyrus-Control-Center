using Glitonea.Mvvm;
using Glitonea.Mvvm.Messaging;
using Slate.Infrastructure.Services;
using Slate.Model.Messaging;

namespace Slate.ViewModel.Control
{
    public class ProcessorStatisticsDisplayViewModel : ViewModelBase
    {
        private readonly IAsusHalService _asusHalService;

        public int ProcessorFanRPM => _asusHalService.ReadCpuFanSpeed();
        public float ProcessorTemperature => _asusHalService.ReadCpuTemperatureCelsius();

        public ProcessorStatisticsDisplayViewModel(IAsusHalService asusHalService)
        {
            _asusHalService = asusHalService;
            
            Message.Subscribe<GlobalTickMessage>(this, OnGlobalTick);
        }
        
        private void OnGlobalTick(GlobalTickMessage _)
        {
            if (!_asusHalService.IsAcpiSessionOpen)
                return;
            
            OnPropertyChanged(nameof(ProcessorFanRPM));
            OnPropertyChanged(nameof(ProcessorTemperature));
        }
    }
}