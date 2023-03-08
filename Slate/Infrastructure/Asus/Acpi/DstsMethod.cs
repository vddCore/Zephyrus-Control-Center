namespace Slate.Infrastructure.Asus.Acpi
{
    public enum DstsMethod
    {
        IIA0_0x00010002_Ret0 = 0x00010002,
        IIA0_0x00010011_Ret0 = 0x00010011,
        CpuFanRpm = 0x00110013,
        GpuFanRpm = 0x00110014,
        CpuTemperature = 0x00120094,
        GpuTemperature = 0x00120097,
        IIA0_0x00110024 = 0x00110024,
        IIA0_0x00110026 = 0x00110026,
        IIA0_0x00110027 = 0x00110027,
        IIA0_0x00080041_Ret0 = 0x00080041,
        IIA0_0x00080042_Ret0 = 0x00080042,
        IIA0_0x00080043_Ret0 = 0x00080043,
    }
}