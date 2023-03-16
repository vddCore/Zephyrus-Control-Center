namespace Slate.Infrastructure.Asus.Acpi
{
    public enum DevsMethod
    {
        SetDisplayOverdrive = 0x00050019,
        SetMuxSwitch = 0x00090016,
        SetEcoMode = 0x00090020,
        SimulateKeyPress = 0x00100021,
        Unk_0x00100022 = 0x00100022,
        SetCpuFanCurve = 0x00110024,
        SetGpuFanCurve = 0x00110025,
        SetCpuFanSpeedDirect = 0x00110022,
        SetGpuFanSpeedDirect = 0x00110023,
        SetBatteryChargingThreshold = 0x00120057,
        SetPerformancePreset = 0x00120075,
        Unk_0x00130022 = 0x00130022,
        
    }
}