using System;
using System.IO;
using System.Management;
using Glitonea.Mvvm;
using Slate.Infrastructure.Asus;

namespace Slate.Infrastructure.Services
{
    public interface IAsusHalService : IService
    {
        bool IsAcpiSessionOpen { get; }

        void SubscribeToWmiEvent(Action<ManagementBaseObject> handler);
        void UnsubscribeFromWmiEvent(Action<ManagementBaseObject> handler);
        void OpenAcpiSession();
        int ReadCpuFanSpeed();
        int ReadGpuFanSpeed();
        float ReadCpuTemperatureCelsius();
        float ReadGpuTemperatureCelsius();
        void SetPerformancePreset(PerformancePreset preset);
        void SetCpuFanDutyCycle(byte dutyCycle);
        void SetGpuFanDutyCycle(byte dutyCycle);
        FanCurve ReadBuiltInCpuFanCurve(PerformancePreset performancePreset);
        FanCurve ReadBuiltInGpuFanCurve(PerformancePreset performancePreset);
        void WriteCpuFanCurve(FanCurve curve);
        void WriteGpuFanCurve(FanCurve curve);
        void SetGraphicsMode(MuxSwitchMode mode);
        MuxSwitchMode GetGraphicsMode();
        void SetEcoMode(bool enable);
        bool GetEcoMode();
        void SetDisplayOverdrive(bool enable);
        bool GetDisplayOverdrive();
        void SetBatteryChargeTarget(int value);
        void SetPlatformPowerTargets(byte totalSystemPpt, byte cpuPpt);
        void DumpWmiRegisters(Stream outStream);
        int[] FetchFirmwareAcpiTableList(bool skipLicenseKeyTable = true);
        void DumpFirmwareAcpiTable(int tableId, Stream outStream);
        int[] FetchRegistryAcpiTableList(bool skipLicenseKeyTable = true);
        void DumpRegistryAcpiTable(int tableId, Stream outStream);
        void CloseAcpiSession();

    }
}