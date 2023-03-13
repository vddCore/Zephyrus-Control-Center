using Glitonea.Mvvm;
using Glitonea.Mvvm.Messaging;
using Slate.Infrastructure.Services;
using Slate.Model.Messaging;

namespace Slate.ViewModel.Control
{
    public class CoreStatisticsDisplayViewModel : ViewModelBase
    {
        protected IHardwareMonitorService HardwareMonitor { get; }

        public CoreStatisticsDisplayViewModel(IHardwareMonitorService hardwareMonitor)
        {
            HardwareMonitor = hardwareMonitor;
        }
    }
}