using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using Microsoft.Win32;
using Slate.Infrastructure.Asus;
using Slate.Infrastructure.Asus.Acpi;
using Slate.Infrastructure.Native;

namespace Slate.Infrastructure.Services
{
    public class AsusHalService : IAsusHalService
    {
        private const int AcpiId = 0x41435049; // 'A' 'C' 'P' 'I'
        private const string AsusAtkWmiEventName = "AsusAtkWmiEvent";
        private IWmiEventService _wmiEventService;
        private AsusAcpiProxy? _proxy;

        public bool IsAcpiSessionOpen => _proxy != null;

        public AsusHalService(IWmiEventService wmiEventService)
        {
            _wmiEventService = wmiEventService;
        }

        public void SubscribeToWmiEvent(Action<ManagementBaseObject> handler)
        {
            _wmiEventService.SubscribeTo(AsusAtkWmiEventName, handler);
        }

        public void UnsubscribeFromWmiEvent(Action<ManagementBaseObject> handler)
        {
            _wmiEventService.UnsubscribeFrom(AsusAtkWmiEventName, handler);
        }
        
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
        public void DumpWmiRegisters(Stream outStream)
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

        public int[] FetchFirmwareAcpiTableList(bool skipLicenseKeyTable = true)
        {
            var dataSize = Kernel32.EnumSystemFirmwareTables(AcpiId, null, 0);
            var data = new byte[dataSize];

            if (dataSize > 0)
            {
                if (Kernel32.EnumSystemFirmwareTables(AcpiId, data, dataSize) > 0)
                {
                    var ret = new int[dataSize / 4];
                    Buffer.BlockCopy(data, 0, ret, 0, (int)dataSize);

                    return ret.Where(
                        x => x.ToFourCharacterCode() != "SSDT" 
                             && (skipLicenseKeyTable && x.ToFourCharacterCode() != "MSDM")
                    ).Distinct().ToArray();
                }
            }

            return new int[0];
        }

        public void DumpFirmwareAcpiTable(int tableId, Stream outStream)
        {
            var dataSize = Kernel32.GetSystemFirmwareTable(
                AcpiId,
                tableId,
                null,
                0
            );

            var data = new byte[dataSize];
            Kernel32.GetSystemFirmwareTable(
                AcpiId,
                tableId,
                data,
                dataSize
            );

            outStream.Write(data);
        }

        public int[] FetchRegistryAcpiTableList(bool skipLicenseKeyTable)
        {
            using (var hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Default))
            {
                using (var subKey = hklm.OpenSubKey($"HARDWARE\\ACPI"))
                {
                    return subKey!
                        .GetSubKeyNames()
                        .Select(x => x.ToFourCharacterCodeInteger())
                        .Where(x => skipLicenseKeyTable && x.ToFourCharacterCode() != "MSDM")
                        .ToArray();
                }
            }
        }

        public void DumpRegistryAcpiTable(int tableId, Stream outStream)
        {
            var data = ReadAcpiTableFromRegistry(tableId.ToFourCharacterCode());
            outStream.Write(data);
        }

        private byte[] ReadAcpiTableFromRegistry(string fourcc)
        {
            // We drill into the registry for SSDTs & co.
            // precisely because GetSystemFirmwareTable
            // is absolute Dogshit. For fuck's sake, why
            // couldn't it be an enumerator?
            //
            var disposeList = new List<RegistryKey>();

            using (var hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Default))
            {
                using (var subKey = hklm.OpenSubKey($"HARDWARE\\ACPI\\{fourcc}"))
                {
                    var currentKey = subKey;
                    object? value;

                    do
                    {
                        var subKeyNames = currentKey!.GetSubKeyNames();

                        value = currentKey.GetValue("00000000");
                        if (value != null)
                            break;

                        if (subKeyNames.Length == 0)
                        {
                            // We've drilled as deep as we can and still no data.
                            // Unfortunate.
                            break;
                        }

                        currentKey = currentKey.OpenSubKey(subKeyNames[0]);
                        disposeList.Add(currentKey!);
                    } while (value == null);

                    foreach (var key in disposeList)
                        key.Dispose();

                    if (value == null)
                    {
                        throw new InvalidOperationException($"No registry data for {fourcc}.");
                    }

                    return (byte[])value;
                }
            }
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