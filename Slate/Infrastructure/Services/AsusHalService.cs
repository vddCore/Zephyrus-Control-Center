using System;
using System.Diagnostics;
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
        public void SetCpuFanDutyCycle(byte dutyCycle)
        {
            ThrowIfProxyNull();
            _proxy!.DEVS.SetCpuFanDutyCycle(dutyCycle);
        }

        [RequiresAcpiSession]
        public void SetGpuFanDutyCycle(byte dutyCycle)
        {
            ThrowIfProxyNull();
            _proxy!.DEVS.SetGpuFanDutyCycle(dutyCycle);
        }

        [RequiresAcpiSession]
        public FanCurve ReadBuiltInCpuFanCurve(PerformancePreset performancePreset)
        {
            ThrowIfProxyNull();
            return _proxy!.DSTS.ReadRawCpuFanCurve(performancePreset);
        }

        [RequiresAcpiSession]
        public FanCurve ReadBuiltInGpuFanCurve(PerformancePreset performancePreset)
        {
            ThrowIfProxyNull();
            return _proxy!.DSTS.ReadRawGpuFanCurve(performancePreset);
        }

        [RequiresAcpiSession]
        public void WriteCpuFanCurve(FanCurve curve)
        {
            ThrowIfProxyNull();
            Debug.WriteLine(curve);
            _proxy!.DEVS.SetCpuFanCurve(curve);
        }

        [RequiresAcpiSession]
        public void WriteGpuFanCurve(FanCurve curve)
        {
            ThrowIfProxyNull();
            _proxy!.DEVS.SetGpuFanCurve(curve);
        }

        [RequiresAcpiSession]
        public void SetGraphicsMode(MuxSwitchMode mode)
        {
            ThrowIfProxyNull();
            _proxy!.DEVS.SetMuxSwitch(mode);
        }

        [RequiresAcpiSession]
        public MuxSwitchMode GetGraphicsMode()
        {
            ThrowIfProxyNull();
            return _proxy!.DSTS.MuxSwitchMode;
        }

        [RequiresAcpiSession]
        public void SetSwitchedGraphicsPowerSaving(bool enable)
        {
            ThrowIfProxyNull();

            if (GetGraphicsMode() == MuxSwitchMode.Discrete)
                throw new InvalidOperationException("This operation is not supported for discrete graphics mode.");

            _proxy!.DEVS.SetEcoMode(enable);
        }

        [RequiresAcpiSession]
        public bool GetSwitchedGraphicsPowerSaving()
        {
            ThrowIfProxyNull();
            return _proxy!.DSTS.IsGraphicsPowerSavingEnabled;
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