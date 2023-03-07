using System.Runtime.InteropServices;

namespace Slate.Infrastructure.Native
{
    internal static class OleAcc
    {
        private const string LibraryName = "oleacc.dll";

        [DllImport(LibraryName)]
        public static extern nint GetProcessHandleFromHwnd(nint hwnd);
    }
}