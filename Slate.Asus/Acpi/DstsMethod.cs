namespace Slate.Asus.Acpi
{
    public enum DstsMethod
    {
        Unk_0x00010001 = 0x00010001,
        Unk_0x00010002 = 0x00010002,
        Unk_0x00010011 = 0x00010011,
        Unk_0x00010013 = 0x00010013,
        IsSmartAntennaSupported = 0x00010032,
        GetDisplayOverdriveStatus = 0x00050019,
        Unk_0x00080041 = 0x00080041,
        Unk_0x00080042 = 0x00080042,
        Unk_0x00080043 = 0x00080043,
        Unk_0x00080044 = 0x00080044,
        GetMuxSwitchStatus = 0x00090016,
        GetEcoModeStatus = 0x00090020,
        IsFnSwitchPromptSupported = 0x00100023,
        Unk_0x00100051 = 0x00100051,
        GetCpuFanSpeed = 0x00110013,
        GetGpuFanSpeed = 0x00110014,
        Unk_0x00110015 = 0x00110015,
        Unk_0x00110016 = 0x00110016,
        IsFanOverboostSupported = 0x00110018,
        Unk_0x00110022 = 0x00110022,
        Unk_0x00110023 = 0x00110023,
        GetCpuFanCurve = 0x00110024,
        GetGpuFanCurve = 0x00110025,
        Unk_0x00110026 = 0x00110026,
        Unk_0x00110027 = 0x00110027,
        /**
         * & with 0x00010000 == IsSupported
         * & with 0x00000010 == IsOverheating
         * & with 0x00000001 == IsRunning
         * Not present in G14 ACPI but...
         **/
        IsFanDustySupported = 0x0011001E, 
        Unk_0x00120057 = 0x00120057,
        Unk_0x00120061 = 0x00120061,
        IsChkUSBPortSupported = 0x00120069,
        GetFanCurveCount = 0x00120075,
        IsLowThermalPolicySupported = 0x00120078,
        Unk_0x00120079 = 0x00120079,
        Unk_0x00120093 = 0x00120093,
        GetCpuTemperature = 0x00120094,
        GetGpuTemperature = 0x00120097,
        Unk_0x0012006C = 0x0012006C,
        Unk_0x001200A0 = 0x001200A0,
        Unk_0x001200A1 = 0x001200A1,
        Unk_0x001200A2 = 0x001200A2,
        Unk_0x001200A3 = 0x001200A3,
        Unk_0x001200B0 = 0x001200B0,
        Unk_0x001200B1 = 0x001200B1,
        Unk_0x001200C1 = 0x001200C1,
        Unk_0x00130021 = 0x00130021,
        Unk_0x00130022 = 0x00130022,
        Unk_0x00130031 = 0x00130031,
        Unk_0x00050019 = 0x00050019,
        Unk_0x00050020 = 0x00050020,
        Unk_0x00060023 = 0x00060023,
        Unk_0x00060024 = 0x00060024,
        Unk_0x00060026 = 0x00060026,
        Unk_0x00060061 = 0x00060061,
    }
}