using System.Drawing;
using Slate.Infrastructure.Native;

namespace Slate.Infrastructure
{
    public class SystemParameters
    {
        public static Size PrimaryScreenSize => new(
            User32.GetSystemMetrics(User32.SM_CXSCREEN),
            User32.GetSystemMetrics(User32.SM_CYSCREEN)
        );
    }
}