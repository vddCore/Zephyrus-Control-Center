using Glitonea.Mvvm;

namespace Slate.Infrastructure.Services
{
    public interface IHardwareMonitorService : IService
    {
        int ProcessorFanRPM { get; }
        int ProcessorTemperature { get; }
        
        int GraphicsFanRPM { get; }
        int GraphicsTemperature { get; }
    }
}