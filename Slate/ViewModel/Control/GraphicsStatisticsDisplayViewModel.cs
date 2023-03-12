using Glitonea.Mvvm;
using Glitonea.Mvvm.Messaging;
using Slate.Infrastructure.Services;
using Slate.Model.Messaging;

namespace Slate.ViewModel.Control
{
    public class GraphicsStatisticsDisplayViewModel : ViewModelBase
    {
        protected IHardwareMonitorService HardwareMonitor { get; }

        public GraphicsStatisticsDisplayViewModel(IHardwareMonitorService hardwareMonitor)
        {
            HardwareMonitor = hardwareMonitor;
        }
    }
}