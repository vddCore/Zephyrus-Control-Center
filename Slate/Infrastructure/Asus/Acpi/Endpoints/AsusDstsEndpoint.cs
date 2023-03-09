using Slate.Infrastructure.Asus.Acpi.FunctionSets;

namespace Slate.Infrastructure.Asus.Acpi.Endpoints
{
    public class AsusDstsEndpoint : AsusAcpiEndpoint<DstsMethod>
    {
        public int CpuFanSpeed => (ReadInt32(DstsMethod.GetCpuFanSpeed) & 0xFFFF) * 100;
        public int GpuFanSpeed => (ReadInt32(DstsMethod.GetGpuFanSpeed) & 0xFFFF) * 100;
        
        public int CpuTemperatureCelsius => ReadInt32(DstsMethod.GetCpuTemperature) & 0xFFFF;
        public int GpuTemperatureCelsius => ReadInt32(DstsMethod.GetGpuTemperature) & 0xFFFF;

        public bool IsGraphicsOverdriveEnabled => (ReadInt32(DstsMethod.GetGraphicsOverdrive) & 0xFFFF) != 0;
        
        internal AsusDstsEndpoint(AsusAcpiProxy proxy) 
            : base(proxy, WmnbFunction.DSTS)
        {
        }
    }
}