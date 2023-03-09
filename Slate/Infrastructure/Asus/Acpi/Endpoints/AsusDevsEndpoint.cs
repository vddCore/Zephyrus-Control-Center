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
            ReadInt32(DevsMethod.InvokeEmbeddedController, (byte)request);
        }
    }
}