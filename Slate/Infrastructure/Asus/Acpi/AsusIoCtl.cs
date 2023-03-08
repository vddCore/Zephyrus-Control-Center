namespace Slate.Infrastructure.Asus.Acpi
{
    public enum AsusIoCtl
    {
        AssignEvent = 0x00222400,
        InvokeAcpiFunction = 0x00222404,
        GetNotifyCode = 0x00222408,
        InvokeWmnbFunction = 0x0022240C
    }
}