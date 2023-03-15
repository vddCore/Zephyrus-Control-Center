using System.Drawing;
using Avalonia.Controls;
using Slate.Infrastructure.Native;

namespace Slate.Infrastructure
{
    public static class SystemParameters
    {
        public static Size PrimaryScreenSize => new(
            User32.GetSystemMetrics(User32.SM_CXSCREEN),
            User32.GetSystemMetrics(User32.SM_CYSCREEN)
        );

        public static Size WorkingAreaSize
        {
            get
            {
                var rect = new User32.RECT();

                User32.SystemParametersInfoW(User32.SPI_GETWORKAREA, 0, ref rect, 0);

                return new(rect.right - rect.left, rect.bottom - rect.top);
            }
        }
    }
}