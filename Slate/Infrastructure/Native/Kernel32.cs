using System.Runtime.InteropServices;

namespace Slate.Infrastructure.Native
{
    internal static class Kernel32
    {
        private const string LibraryName = "kernel32.dll";

        public const nuint GENERIC_READ = 0x80000000;
        public const nuint GENERIC_WRITE = 0x40000000;
        public const nuint OPEN_EXISTING = 3;
        public const nuint FILE_ATTRIBUTE_NORMAL = 0x80;
        public const nuint FILE_SHARE_READ = 0x01;
        public const nuint FILE_SHARE_WRITE = 0x02;

        [DllImport(LibraryName, CharSet = CharSet.Unicode)]
        public static extern nint CreateFile(
            [MarshalAs(UnmanagedType.LPWStr)] string lpFileName,
            nuint dwDesiredAccess,
            nuint dwShareMode,
            nint lpSecurityAttributes,
            nuint dwCreationDisposition,
            nuint dwFlagsAndAttributes,
            nint hTemplateFile
        );

        [DllImport(LibraryName, SetLastError = true)]
        public static extern unsafe bool DeviceIoControl(
            nint hDevice,
            nuint dwIoControlCode,
            byte* lpInBuffer,
            [MarshalAs(UnmanagedType.U4)] int nInBufferSize,
            byte* lpOutBuffer,
            [MarshalAs(UnmanagedType.U4)] int nOutBufferSize,
            out uint lpBytesReturned,
            nint lpOverlapped
        );

        [DllImport(LibraryName)]
        public static extern nint GetLastError();
        
        [DllImport(LibraryName)]
        public static extern bool CloseHandle(nint hObject);
    }
}