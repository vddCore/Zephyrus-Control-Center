using Slate.Infrastructure.Asus.Acpi.FunctionSets;

namespace Slate.Infrastructure.Asus.Acpi.Endpoints
{
    public class AsusDstsEndpoint : AsusAcpiEndpoint<DstsMethod>
    {
        public int CpuFanSpeed => (ReadInt32(DstsMethod.GetCpuFanSpeed) & 0xFFFF) * 100;
        public int GpuFanSpeed => (ReadInt32(DstsMethod.GetGpuFanSpeed) & 0xFFFF) * 100;

        public int CpuTemperatureCelsius => ReadInt32(DstsMethod.GetCpuTemperature) & 0xFFFF;
        public int GpuTemperatureCelsius => ReadInt32(DstsMethod.GetGpuTemperature) & 0xFFFF;

        public MuxSwitchMode MuxSwitchMode => (MuxSwitchMode)(ReadInt32(DstsMethod.GetMuxSwitchStatus) & 0xFFFF);

        public bool IsGraphicsPowerSavingEnabled => (ReadInt32(DstsMethod.GetEcoModeStatus) & 0xFFFF) != 0;
        public bool IsDisplayOverdriveEnabled => (ReadInt32(DstsMethod.GetDisplayOverdriveStatus) & 0xFFFF) != 0;

        internal AsusDstsEndpoint(AsusAcpiProxy proxy)
            : base(proxy, WmnbFunction.DSTS)
        {
        }

        public FanCurve ReadRawCpuFanCurve(PerformancePreset preset)
            => new(ReadBytes(   
                DstsMethod.GetCpuFanCurve,
                16,
                MapPresetToDstsParameter(preset)
            ));

        public FanCurve ReadRawGpuFanCurve(PerformancePreset preset)
            => new(ReadBytes(
                DstsMethod.GetGpuFanCurve,
                16,
                MapPresetToDstsParameter(preset)
            ));

        /**
         * ASUS fucked up and now I have to pay the price.
         * 
         * For some reason Silent and Performance are inverted for
         * GetCpuFanCurve and GetGpuFanCurve -- it's inconsistent
         * with SetPerformancePreset in DEVS.
         *
         * Of course not. That'd make way too much sense.
         * Am I glad I don't work in there.
         **/
        private byte MapPresetToDstsParameter(PerformancePreset preset)
        {
            return preset switch
            {
                PerformancePreset.Performance => (byte)PerformancePreset.Silent,
                PerformancePreset.Silent => (byte)PerformancePreset.Performance,
                _ => (byte)PerformancePreset.Balanced
            };
        }
    }
}