using Glitonea.Mvvm;
using Glitonea.Mvvm.Messaging;
using Slate.Infrastructure.Services;
using Slate.Model.Messaging;

namespace Slate.ViewModel.Control
{
    public class GraphicsStatisticsDisplayViewModel : ViewModelBase
    {
        private readonly IAsusHalService _asusHalService;

        public int GraphicsFanRPM => _asusHalService.ReadGpuFanSpeed();
        public float GraphicsTemperature => _asusHalService.ReadGpuTemperatureCelsius();

        public GraphicsStatisticsDisplayViewModel(IAsusHalService asusHalService)
        {
            _asusHalService = asusHalService;
            
            Message.Subscribe<GlobalTickMessage>(this, OnGlobalTick);
        }
        
        private void OnGlobalTick(GlobalTickMessage _)
        {
            if (!_asusHalService.IsAcpiSessionOpen)
                return;
            
            OnPropertyChanged(nameof(GraphicsFanRPM));
            OnPropertyChanged(nameof(GraphicsTemperature));
        }
    }
}