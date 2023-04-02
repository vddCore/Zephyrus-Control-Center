namespace Slate.Asus.Acpi
{
    public enum DevsMethod
    {
        SetDisplayOverdrive = 0x00050019,
        SetMuxSwitch = 0x00090016,
        SetEcoMode = 0x00090020,
        Unk_0x00010003 = 0x00010003, // ACPI: CWAP
        Unk_0x00010012 = 0x00010012, // ACPI: WLED
        Unk_0x00010013 = 0x00010013, // ACPI: BLED
        SimulateKeyPress = 0x00100021,
        Unk_0x00100022 = 0x00100022,
        SetCpuFanCurve = 0x00110024,
        SetGpuFanCurve = 0x00110025,
        SetCpuFanSpeedDirect = 0x00110022,
        SetGpuFanSpeedDirect = 0x00110023,
        SetBatteryChargeTarget = 0x00120057,
        SetPerformancePreset = 0x00120075,
        SetTotalPPT = 0x001200A0,
        SetCpuEDC = 0x001200A1,
        SetCpuTDC = 0x001200A2,
        Unk_0x001200A3 = 0x001200A3, // ACPI: PLON
        SetCpuPPT = 0x001200B0,      // ACPI: PLAO
        Unk_0x001200B1 = 0x001200B1, // ACPI: PLPS
        Unk_0x001200C0 = 0x001200C0, // no-op
        Unk_0x001200C1 = 0x001200C1, // ACPI: PLFW
        Unk_0x001200C2 = 0x001200C2, // no-op
        Unk_0x00130022 = 0x00130022, // ACPI: APSC
    }
}