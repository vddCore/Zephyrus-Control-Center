namespace Slate.Asus.Acpi.FunctionSets
{
    public enum WmnbFunction
    {
        INIT = 0x54494E49, // 'T' 'I' 'N' 'I' | IIA0
        BSTS = 0x53545342, // 'S' 'T' 'S' 'B'
        SFUN = 0x4E554653, // 'N' 'U' 'F' 'S'
        WDOG = 0x474F4457, // 'G' 'O' 'D' 'W' | IIA0
        KBNI = 0x494E424B, // 'I' 'N' 'B' 'K' 
        SCDG = 0x47444353, // 'G' 'D' 'C' 'S' | IIA0, IIA1
        SPEC = 0x43455053, // 'C' 'E' 'P' 'S' | IIA0
        OSVR = 0x5256534F, // 'R' 'V' 'O' 'S' | IIA0
        VERS = 0x53524556, // 'S' 'R' 'E' 'V' | IIA0, IIA1
        GLCD = 0x44434C47, // 'D' 'C' 'L' 'G'
        ANVI = 0x49564E41, // 'I' 'V' 'N' 'A'
        MWGF = 0x4647574D, // 'F' 'G' 'W' 'M'
        DSTS = 0x53545344, // 'S' 'T' 'S' 'D'
        DEVS = 0x53564544  // 'S' 'V' 'E' 'D'
    }
}