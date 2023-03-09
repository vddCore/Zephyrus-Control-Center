using System;

namespace Slate.Infrastructure.Asus.Acpi
{
    [AttributeUsage(AttributeTargets.Method)]
    public class RequiresAcpiSessionAttribute : Attribute
    {
    }
}