using System.Runtime.InteropServices;
using System.Text;

namespace Slate.Infrastructure.Native
{
    internal static class PsApi
    {
        private const string LibraryName = "psapi.dll";

        [DllImport(LibraryName, EntryPoint = "GetProcessImageFileNameW")]
        private static extern uint GetProcessImageFileNameW_INTERNAL(
            nint hProcess,
            byte[] lpImageFileName, 
            [In, MarshalAs(UnmanagedType.U4)] int nSize
        );

        public static uint GetProcessImageFileName(nint hProcess, out string lpImageFileName, int nSize = 512)
        {
            lpImageFileName = string.Empty;
            
            var bytes = new byte[nSize];
            var ret = GetProcessImageFileNameW_INTERNAL(hProcess, bytes, nSize);

            if (ret > 0)
            {
                lpImageFileName = Encoding.Unicode.GetString(bytes).Substring(0, (int)ret);
            }
            
            return ret;
        }
    }
}