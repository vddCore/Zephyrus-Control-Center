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
        
        public void SetGpuFanDutyCycle(byte dutyCycle)
        {
            _ = ReadInt32(DevsMethod.SetGpuFanSpeedDirect, dutyCycle);
        }

        public void SetCpuFanCurve(FanCurve curve)
        {
            _ = ReadInt32(DevsMethod.SetCpuFanCurve, curve.RawData);
        }

        public void SetGpuFanCurve(FanCurve curve)
        {
            _ = ReadInt32(DevsMethod.SetGpuFanCurve, curve.RawData);
        }
    }
}