using Glitonea.Mvvm;

namespace Slate.Infrastructure.Services
{
    public interface IAsusHalService : IService
    {
        bool IsAcpiSessionOpen { get; }
        
        void OpenAcpiSession();
        int ReadCpuFanSpeed();
        int ReadGpuFanSpeed();
        float ReadCpuTemperatureCelsius();
        float ReadGpuTemperatureCelsius();
        void CloseAcpiSession();

    }
}