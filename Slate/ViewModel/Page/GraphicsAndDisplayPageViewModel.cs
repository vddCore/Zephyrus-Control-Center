using Glitonea.Mvvm;
using Slate.Infrastructure.Services;

namespace Slate.ViewModel.Page
{
    public class GraphicsAndDisplayPageViewModel : ViewModelBase
    {
        public GraphicsAndDisplayPageViewModel(
            ISettingsService settingsService,
            IAsusHalService asusHalService,
            IHardwareMonitorService hardwareMonitor)
        {
        }
    }
}