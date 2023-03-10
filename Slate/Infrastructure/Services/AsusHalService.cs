using System;
using Slate.Infrastructure.Asus;
using Slate.Infrastructure.Asus.Acpi;

namespace Slate.Infrastructure.Services
{
    public class AsusHalService : IAsusHalService
    {
        private AsusAcpiProxy? _proxy;

        public bool IsAcpiSessionOpen => _proxy != null;

        public void OpenAcpiSession()
        {
            if (_proxy != null)
            {
                throw new InvalidOperationException("An existing ASUS ACPI session is already active.");
            }

            _proxy = new AsusAcpiProxy();
        }

        [RequiresAcpiSession]
        public int ReadCpuFanSpeed()
        {
            ThrowIfProxyNull();
            return _proxy!.DSTS.CpuFanSpeed;
        }
        
        [RequiresAcpiSession]
        public float ReadCpuTemperatureCelsius()
        {
            ThrowIfProxyNull();
            return _proxy!.DSTS.CpuTemperatureCelsius;
        }
        
        [RequiresAcpiSession]
        public int ReadGpuFanSpeed()
        {
            ThrowIfProxyNull();
            return _proxy!.DSTS.GpuFanSpeed;
        }

        [RequiresAcpiSession]
        public float ReadGpuTemperatureCelsius()
        {
            ThrowIfProxyNull();
            return _proxy!.DSTS.GpuTemperatureCelsius;
        }

        [RequiresAcpiSession]
        public void SetPerformancePreset(PerformancePreset preset)
        {
            ThrowIfProxyNull();
            _proxy!.DEVS.SetPerformancePreset(preset);
        }

        [RequiresAcpiSession]
        public void CloseAcpiSession()
        {
            ThrowIfProxyNull();
            _proxy!.Dispose();
        }

        private void ThrowIfProxyNull()
        {
            if (!IsAcpiSessionOpen)
            {
                throw new InvalidOperationException("There is no ASUS ACPI session open at the moment.");
            }
        }
    }
}