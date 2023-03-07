using System;

namespace Slate.Infrastructure
{
    [AttributeUsage(AttributeTargets.Method)]
    public class RequiresAcpiSessionAttribute : Attribute
    {
    }
}