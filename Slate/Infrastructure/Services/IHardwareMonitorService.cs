using Glitonea.Mvvm;

namespace Slate.Infrastructure.Services
{
    public interface IHardwareMonitorService : IService
    {
        int CpuFanRPM { get; }
        int CpuTemperature { get; }
        
        int GpuFanRPM { get; }
        int GpuTemperature { get; }
    }
}