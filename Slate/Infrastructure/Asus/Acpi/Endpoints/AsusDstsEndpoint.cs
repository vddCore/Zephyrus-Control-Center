namespace Slate.Infrastructure.Asus.Acpi.Endpoints
{
    public class AsusDstsEndpoint : AsusAcpiEndpoint<DstsMethod>
    {
        internal AsusDstsEndpoint(AsusAcpiProxy proxy) 
            : base(proxy, WmnbAcpiMethod.DSTS)
        {
        }
    }
}