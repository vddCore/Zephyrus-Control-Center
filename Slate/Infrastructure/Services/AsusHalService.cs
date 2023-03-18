using System;
using System.Diagnostics;
using System.IO;
using System.Text;
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
        public void SetEcoMode(bool enable)
        {
            ThrowIfProxyNull();

            if (GetGraphicsMode() == MuxSwitchMode.Discrete)
                throw new InvalidOperationException("This operation is not supported for discrete graphics mode.");

            _proxy!.DEVS.SetEcoMode(enable);
        }

        [RequiresAcpiSession]
        public bool GetEcoMode()
        {
            ThrowIfProxyNull();
            return _proxy!.DSTS.IsGraphicsPowerSavingEnabled;
        }

        [RequiresAcpiSession]
        public bool GetDisplayOverdrive()
        {
            ThrowIfProxyNull();
            return _proxy!.DSTS.IsDisplayOverdriveEnabled;
        }

        [RequiresAcpiSession]
        public void SetDisplayOverdrive(bool enable)
        {
            ThrowIfProxyNull();
            _proxy!.DEVS.SetDisplayOverdrive(enable);
        }

        [RequiresAcpiSession]
        public void SetBatteryChargeTarget(int value)
        {
            ThrowIfProxyNull();

            if (value > 100 || value < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(value),
                    "Battery charge target must be a number between 0-100."
                );
            }

            _proxy!.DEVS.SetBatteryChargeTarget((byte)value);
        }

        [RequiresAcpiSession]
        public void SetPlatformPowerTargets(byte totalSystemPpt, byte cpuPpt)
        {
            ThrowIfProxyNull();

            if (cpuPpt > totalSystemPpt)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(cpuPpt),
                    "CPU PPT must be less than or equal to total system PPT."
                );
            }

            _proxy!.DEVS.SetTotalPPT(totalSystemPpt);
            _proxy!.DEVS.SetCpuPPT(cpuPpt);
        }

        [RequiresAcpiSession]
        public void CloseAcpiSession()
        {
            ThrowIfProxyNull();
            _proxy!.Dispose();
        }

        [RequiresAcpiSession]
        public void DumpAcpiRegisters(Stream outStream)
        {
            ThrowIfProxyNull();

            var names = Enum.GetNames(typeof(DstsMethod));

            using (var sw = new StreamWriter(outStream, leaveOpen: true))
            {
                foreach (var name in names)
                {
                    string value = "[failed to retrieve -- probably requires arguments]";

                    try
                    {
                        var key = (DstsMethod)Enum.Parse(typeof(DstsMethod), name);

                        switch (key)
                        {
                            case DstsMethod.Unk_0x00060024:
                            case DstsMethod.GetCpuFanCurve:
                            case DstsMethod.GetGpuFanCurve:
                            case DstsMethod.Unk_0x00110026:
                            case DstsMethod.Unk_0x00110027:
                            {
                                var bytes = _proxy!.DSTS.ReadBytes(key, 40);
                                var sb = new StringBuilder();
                                
                                foreach (var b in bytes)
                                    sb.Append($"{b:X2} ");

                                value = sb.ToString().Trim();
                                break;
                            }

                            default:
                                value = $"{_proxy!.DSTS.ReadInt32(key):X8}";
                                break;
                        }
                    }
                    catch
                    {
                        // Ignore.
                    }

                    sw.WriteLine(
                        $"{name}: {value}"
                    );
                }
            }
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