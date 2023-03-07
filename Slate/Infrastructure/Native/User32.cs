using System.Runtime.InteropServices;
using System.Text;

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
        
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate bool EnumWindowsProc(nint hwnd, nint lParam);

        [DllImport(LibraryName)]
        public static extern bool EnumWindows(EnumWindowsProc wndProc, nint lParam);

        [DllImport(LibraryName)]
        public static extern int GetSystemMetrics(int nIndex);

        [DllImport(LibraryName, EntryPoint = "GetWindowTextW")]
        private static extern int GetWindowText_INTERNAL(nint hwnd, byte[] lpString, int nMaxCount);

        [DllImport(LibraryName, EntryPoint = "RealGetWindowClassW")]
        private static extern int RealGetWindowClassW_INTERNAL(nint hwnd, byte[] ptszClassName, int cchClassNameMax);
        
        public static int GetWindowText(nint hwnd, out string lpString, int nMaxCount = 512)
        {
            lpString = string.Empty;
            var bytes = new byte[nMaxCount];
            var ret = GetWindowText_INTERNAL(hwnd, bytes, nMaxCount);

            if (ret > 0)
            {
                lpString = Encoding.Unicode.GetString(bytes).Substring(0, ret);
            }

            return ret;
        }
        
        public static int GetWindowClass(nint hwnd, out string ptszClassName, int cchClassNameMax = 512)
        {
            ptszClassName = string.Empty;
            var bytes = new byte[cchClassNameMax];
            var ret = RealGetWindowClassW_INTERNAL(hwnd, bytes, cchClassNameMax);

            if (ret > 0)
            {
                ptszClassName = Encoding.Unicode.GetString(bytes).Substring(0, ret);
            }

            return ret;
        }
        
        [DllImport(LibraryName, SetLastError = true)]
        public static extern int GetWindowLong(nint hWnd, int nIndex);
        
        [DllImport(LibraryName)]
        public static extern int SetWindowLong(nint hWnd, int nIndex, int dwNewLong);
        
    }
}