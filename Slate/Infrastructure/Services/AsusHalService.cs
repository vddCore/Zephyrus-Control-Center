using System;
using System.Diagnostics;
using System.Linq;
using Slate.Model;

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

            _proxy = new AsusAcpiProxy(@"\\.\\ATKACPI");
        }

        [RequiresAcpiSession]
        public int ReadCpuFanSpeed()
        {
            ThrowIfProxyNull();
            return (_proxy!.ReadInt32(AsusComponent.CpuFan) & 0xFFFF) * 100;
        }

        [RequiresAcpiSession]
        public int ReadGpuFanSpeed()
        {
            ThrowIfProxyNull();
            return (_proxy!.ReadInt32(AsusComponent.GpuFan) & 0xFFFF) * 100;
        }

        [RequiresAcpiSession]
        public void CloseAcpiSession()
        {
            ThrowIfProxyNull();
            _proxy!.Dispose();
        }

        public float ReadCpuTemperatureCelsius()
        {
            using (var counter = new PerformanceCounter("Thermal Zone Information", "Temperature", @"\_TZ.THRM", true))
            {
                return counter.NextValue() - 273;
            }
        }

        public float ReadGpuTemperatureCelsius()
        {
            return 0;
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