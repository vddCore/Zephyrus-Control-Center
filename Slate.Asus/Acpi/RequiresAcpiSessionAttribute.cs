using System;

namespace Slate.Asus.Acpi
{
    [AttributeUsage(AttributeTargets.Method)]
    public class RequiresAcpiSessionAttribute : Attribute
    {
    }
}