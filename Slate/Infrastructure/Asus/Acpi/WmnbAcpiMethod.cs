namespace Slate.Infrastructure.Asus.Acpi
{
    public enum WmnbAcpiMethod
    {
        MGWF = 0x4647574D, // 'F' 'G' 'W' 'M'
        DSTS = 0x53545344, // 'S' 'T' 'S' 'D'
        DEVS = 0x53564544 // 'S' 'V' 'E' 'D'
    }
}