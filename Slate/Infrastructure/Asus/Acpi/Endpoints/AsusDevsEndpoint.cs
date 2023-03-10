using Slate.Infrastructure.Asus.Acpi.FunctionSets;

namespace Slate.Infrastructure.Asus.Acpi.Endpoints
{
    public class AsusDevsEndpoint : AsusAcpiEndpoint<DevsMethod>
    {        
        public AsusDevsEndpoint(AsusAcpiProxy proxy) 
            : base(proxy, WmnbFunction.DEVS)
        {
        }

        public void InvokeEmbeddedController(EmbeddedControllerRequest request)
        {
            _ = ReadInt32(DevsMethod.InvokeEmbeddedController, (byte)request);
        }

        public void SetPerformancePreset(PerformancePreset preset)
        {
            _ = ReadInt32(DevsMethod.SetPerformancePreset, (byte)preset);
        }

        public void SetCpuFanDutyCycle(byte dutyCycle)
        {
            _ = ReadInt32(DevsMethod.SetCpuFanSpeedDirect, dutyCycle);
        }
    }
}