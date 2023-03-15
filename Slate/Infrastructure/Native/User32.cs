using System;
using System.Runtime.InteropServices;

namespace Slate.Infrastructure.Native
{
    internal static class User32
    {
        private const string LibraryName = "user32.dll";

        public const int SM_CXSCREEN = 0;
        public const int SM_CYSCREEN = 1;

        public const int SPI_GETWORKAREA = 0x0030;

        public const int GWL_EX_STYLE = -20;
        public const int WS_EX_APPWINDOW = 0x0004000;
        public const int WS_EX_TOOLWINDOW = 0x00000080;

        public const nint HWND_BOTTOM = 1;
        public const nint HWND_NOTOPMOST = 2;
        public const nint HWND_TOP = 2;
        public const nint HWND_TOPMOST = -1;

        public const uint ERROR_INSUFFICIENT_BUFFER = 122;

        [Flags]
        public enum DISPLAYCONFIG_TOPOLOGY_ID : uint
        {
            DISPLAYCONFIG_TOPOLOGY_INTERNAL = 1,
            DISPLAYCONFIG_TOPOLOGY_CLONE = 2,
            DISPLAYCONFIG_TOPOLOGY_EXTEND = 4,
            DISPLAYCONFIG_TOPOLOGY_EXTERNAL = 8,
        }

        [Flags]
        public enum QDC
        {
            ALL_PATHS = 0x00000001,
            ONLY_ACTIVE_PATHS = 0x00000002,
            DATABASE_CURRENT = 0x00000004,
            VIRTUAL_MODE_AWARE = 0x00000010,
            INCLUDE_HMD = 0x00000020,
        }

        [Flags]
        public enum SDC
        {
            TOPOLOGY_INTERNAL = 0x00000001,
            TOPOLOGY_CLONE = 0x00000002,
            TOPOLOGY_EXTEND = 0x00000004,
            TOPOLOGY_EXTERNAL = 0x00000008,
            TOPOLOGY_SUPPLIED = 0x00000010,

            USE_DATABASE_CURRENT = TOPOLOGY_INTERNAL
                                   | TOPOLOGY_CLONE
                                   | TOPOLOGY_EXTEND
                                   | TOPOLOGY_EXTERNAL,

            USE_SUPPLIED_DISPLAY_CONFIG = 0x00000020,
            VALIDATE = 0x00000040,
            APPLY = 0x00000080,
            NO_OPTIMIZATION = 0x00000100,
            SAVE_TO_DATABASE = 0x00000200,
            ALLOW_CHANGES = 0x00000400,
            PATH_PERSIST_IF_REQUIRED = 0x00000800,
            FORCE_MODE_ENUMERATION = 0x00001000,
            ALLOW_PATH_ORDER_CHANGES = 0x00002000,
            VIRTUAL_MODE_AWARE = 0x00008000,
        }

        public enum DISPLAYCONFIG_MODE_INFO_TYPE : uint
        {
            SOURCE = 1,
            TARGET,
            DESKTOP_IMAGE,
        }

        public enum DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY : uint
        {
            OTHER = unchecked((uint)-1),
            HD15 = 0,
            SVIDEO,
            COMPOSITE_VIDEO,
            COMPONENT_VIDEO,
            DVI,
            HDMI,
            LVDS,
            D_JPN,
            SDI,
            DISPLAYPORT_EXTERNAL,
            DISPLAYPORT_EMBEDDED,
            UDI_EXTERNAL,
            UDI_EMBEDDED,
            SDTVDONGLE,
            MIRACAST,
            INDIRECT_WIRED,
            INDIRECT_VIRTUAL,
            INTERNAL = 0x80000000,
        }

        public enum DISPLAYCONFIG_ROTATION : uint
        {
            IDENTITY = 1,
            ROTATE90,
            ROTATE180,
            ROTATE270,
        }

        public enum DISPLAYCONFIG_SCALING : uint
        {
            IDENTITY = 1,
            CENTERED,
            STRETCHED,
            ASPECTRATIOCENTEREDMAX,
            CUSTOM,
            PREFERRED = 128,
        }

        public enum DISPLAYCONFIG_SCANLINE_ORDERING : uint
        {
            UNSPECIFIED = 0,
            PROGRESSIVE = 1,
            INTERLACED = 2,
            INTERLACED_UPPERFIELDFIRST = INTERLACED,
            INTERLACED_LOWERFIELDFIRST = 3,
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 4)]
        public struct DISPLAYCONFIG_PATH_SOURCE_INFO
        {
            public ulong adapterId;
            public uint id;
            public Union union;

            public uint statusFlags;

            [StructLayout(LayoutKind.Explicit)]
            public struct Union
            {
                [FieldOffset(0)]
                public uint modeInfoIdx;

                [FieldOffset(0)]
                public ushort cloneGroupId;

                [FieldOffset(2)]
                public ushort sourceModeInfoIdx;
            }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct DISPLAYCONFIG_RATIONAL
        {
            public uint Numerator;
            public uint Denominator;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 4)]
        public struct DISPLAYCONFIG_PATH_TARGET_INFO
        {
            public ulong adapterId;
            public uint id;
            public Union union;

            public DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY outputTechnology;
            public DISPLAYCONFIG_ROTATION rotation;
            public DISPLAYCONFIG_SCALING scaling;
            public DISPLAYCONFIG_RATIONAL refreshRate;
            public DISPLAYCONFIG_SCANLINE_ORDERING scanLineOrdering;

            [MarshalAs(UnmanagedType.Bool)]
            public bool targetAvailable;

            public uint statusFlags;

            [StructLayout(LayoutKind.Explicit)]
            public struct Union
            {
                [FieldOffset(0)]
                public uint modeInfoIdx;

                [FieldOffset(0)]
                public ushort desktopModeInfoIdx;

                [FieldOffset(2)]
                public ushort targetModeInfoIdx;
            }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct DISPLAYCONFIG_PATH_INFO
        {
            public DISPLAYCONFIG_PATH_SOURCE_INFO sourceInfo;
            public DISPLAYCONFIG_PATH_TARGET_INFO targetInfo;

            public uint flags;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct DISPLAYCONFIG_2DREGION
        {
            public uint cx;
            public uint cy;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 4)]
        public struct DISPLAYCONFIG_VIDEO_SIGNAL_INFO
        {
            public ulong pixelRate;
            public DISPLAYCONFIG_RATIONAL hSyncFreq;
            public DISPLAYCONFIG_RATIONAL vSyncFreq;
            public DISPLAYCONFIG_2DREGION activeSize;
            public DISPLAYCONFIG_2DREGION totalSize;
            public int videoStandard;
            public ushort vSyncFreqDivider;
            public DISPLAYCONFIG_SCANLINE_ORDERING scanLineOrdering;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DISPLAYCONFIG_TARGET_MODE
        {
            public DISPLAYCONFIG_VIDEO_SIGNAL_INFO targetVideoSignalInfo;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct DISPLAYCONFIG_SOURCE_MODE
        {
            public uint width;
            public uint height;
            public int pixelFormat;
            public POINT position;
        }

        [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Auto, Pack = 4)]
        public struct DISPLAYCONFIG_MODE_INFO
        {
            [FieldOffset(0)]
            public DISPLAYCONFIG_MODE_INFO_TYPE infoType;

            [FieldOffset(4)]
            public uint id;

            [FieldOffset(8)]
            public ulong adapterId;

            [FieldOffset(16)]
            public DISPLAYCONFIG_TARGET_MODE targetMode;

            [FieldOffset(16)]
            public DISPLAYCONFIG_SOURCE_MODE sourceMode;

            [FieldOffset(16)]
            public DISPLAYCONFIG_DESKTOP_IMAGE_INFO desktopImageInfo;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct DISPLAYCONFIG_DESKTOP_IMAGE_INFO
        {
            public POINT PathSourceSize;
            public RECT DesktopImageRegion;
            public RECT DesktopImageClip;
        }

        [DllImport(LibraryName, ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern bool SystemParametersInfoW(
            uint uiAction,
            uint uiParam,
            ref RECT pvParam,
            uint fWinIni
        );

        [DllImport(LibraryName)]
        public static extern int GetSystemMetrics(int nIndex);

        [DllImport(LibraryName)]
        public static extern int GetWindowLong(nint hWnd, int nIndex);

        [DllImport(LibraryName)]
        public static extern int SetWindowLong(nint hWnd, int nIndex, int dwNewLong);

        [DllImport(LibraryName, SetLastError = false, ExactSpelling = true)]
        private static extern int QueryDisplayConfig(
            QDC flags,
            ref uint numPathArrayElements,
            [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)]
            DISPLAYCONFIG_PATH_INFO[] pathArray,
            ref uint numModeInfoArrayElements,
            [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)]
            DISPLAYCONFIG_MODE_INFO[] modeInfoArray,
            out DISPLAYCONFIG_TOPOLOGY_ID currentTopologyId
        );

        [DllImport(LibraryName, SetLastError = false, ExactSpelling = true)]
        private static extern int QueryDisplayConfig(
            QDC flags,
            ref uint numPathArrayElements,
            [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)]
            DISPLAYCONFIG_PATH_INFO[] pathArray,
            ref uint numModeInfoArrayElements,
            [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)]
            DISPLAYCONFIG_MODE_INFO[] modeInfoArray,
            nint currentTopologyId = default
        );

        [DllImport(LibraryName, SetLastError = false, ExactSpelling = true)]
        public static extern int SetDisplayConfig(
            uint numPathArrayElements,
            [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0), Optional]
            DISPLAYCONFIG_PATH_INFO[] pathArray,
            uint numModeInfoArrayElements,
            [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2), Optional]
            DISPLAYCONFIG_MODE_INFO[] modeInfoArray,
            SDC flags
        );

        [DllImport(LibraryName, SetLastError = false, ExactSpelling = true)]
        public static extern int GetDisplayConfigBufferSizes(
            QDC flags,
            out uint numPathArrayElements,
            out uint numModeInfoArrayElements
        );


        public static int QueryDisplayConfig(
            QDC flags,
            out DISPLAYCONFIG_PATH_INFO[] pathArray,
            out DISPLAYCONFIG_MODE_INFO[] modeInfoArray,
            out DISPLAYCONFIG_TOPOLOGY_ID currentTopologyId
        )
        {
            int err;

            do
            {
                err = GetDisplayConfigBufferSizes(flags, out var cPath, out var cMode);

                pathArray = new DISPLAYCONFIG_PATH_INFO[err != 0 ? 0 : cPath];
                modeInfoArray = new DISPLAYCONFIG_MODE_INFO[err != 0 ? 0 : cMode];
                currentTopologyId = 0;

                if (err != 0)
                {
                    return err;
                }

                if (flags.HasFlag(QDC.DATABASE_CURRENT))
                {
                    err = QueryDisplayConfig(
                        flags,
                        ref cPath,
                        pathArray,
                        ref cMode,
                        modeInfoArray,
                        out currentTopologyId
                    );
                }
                else
                {
                    err = QueryDisplayConfig(
                        flags,
                        ref cPath,
                        pathArray,
                        ref cMode,
                        modeInfoArray
                    );
                }
                if (err == 0 || err != ERROR_INSUFFICIENT_BUFFER) return err;
            } while (true);
        }
    }
}