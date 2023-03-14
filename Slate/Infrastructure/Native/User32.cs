using System.Runtime.InteropServices;

namespace Slate.Infrastructure.Native
{
    internal static class User32
    {
        private const string LibraryName = "user32.dll";

        public const int SM_CXSCREEN = 0;
        public const int SM_CYSCREEN = 1;

        public const int GWL_EX_STYLE = -20;
        public const int WS_EX_APPWINDOW = 0x0004000;
        public const int WS_EX_TOOLWINDOW = 0x00000080;

        public const nint HWND_BOTTOM = 1;
        public const nint HWND_NOTOPMOST = 2;
        public const nint HWND_TOP = 2;
        public const nint HWND_TOPMOST = -1;

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public nint left;
            public nint top;
            public nint right;
            public nint bottom;
        }
        
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public nint x;
            public nint y;
        }
        
        [DllImport(LibraryName)]
        public static extern int GetSystemMetrics(int nIndex);

        [DllImport(LibraryName, SetLastError = true)]
        public static extern int GetWindowLong(nint hWnd, int nIndex);
        
        [DllImport(LibraryName)]
        public static extern int SetWindowLong(nint hWnd, int nIndex, int dwNewLong);
    }
}