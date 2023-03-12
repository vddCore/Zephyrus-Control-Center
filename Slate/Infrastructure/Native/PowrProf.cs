using System;
using System.Runtime.InteropServices;

namespace Slate.Infrastructure.Native
{
    internal static class PowrProf
    {
        public const string LibraryName = "powrprof.dll";

        public static readonly Guid GUID_PROCESSOR_SETTINGS_SUBGROUP = new("54533251-82BE-4824-96C1-47B60B740D00");
        public static readonly Guid GUID_PROCESSOR_PERFORMANCE_BOOST_SETTING = new("BE337238-0D82-4146-A960-4F3749D470C7");
        
        [DllImport(LibraryName, CharSet = CharSet.Unicode)]
        public static extern nuint PowerGetActiveScheme(
            nint UserPowerKey, 
            [Out, MarshalAs(UnmanagedType.LPStruct)] out Guid ActivePolicyGuid
        );
        
        [DllImport(LibraryName, CharSet = CharSet.Unicode)]
        public static extern nuint PowerSetActiveScheme(
            nint RootPowerKey,
            [MarshalAs(UnmanagedType.LPStruct)] Guid SchemeGuid
        );
        
        [DllImport(LibraryName, CharSet = CharSet.Unicode)]
        public static extern nuint PowerWriteDCValueIndex(
            nint RootPowerKey,
            [MarshalAs(UnmanagedType.LPStruct)] Guid SchemeGuid,
            [MarshalAs(UnmanagedType.LPStruct)] Guid SubGroupOfPowerSettingsGuid,
            [MarshalAs(UnmanagedType.LPStruct)] Guid PowerSettingGuid,
            int AcValueIndex
        );

        [DllImport(LibraryName, CharSet = CharSet.Unicode)]
        public static extern nuint PowerWriteACValueIndex(
            nint RootPowerKey,
            [MarshalAs(UnmanagedType.LPStruct)] Guid SchemeGuid,
            [MarshalAs(UnmanagedType.LPStruct)] Guid SubGroupOfPowerSettingsGuid,
            [MarshalAs(UnmanagedType.LPStruct)] Guid PowerSettingGuid,
            int AcValueIndex
        );

        [DllImport(LibraryName, CharSet = CharSet.Unicode)]
        public static extern nuint PowerReadACValueIndex(
            nint RootPowerKey,
            [MarshalAs(UnmanagedType.LPStruct)] Guid SchemeGuid,
            [MarshalAs(UnmanagedType.LPStruct)] Guid SubGroupOfPowerSettingsGuid,
            [MarshalAs(UnmanagedType.LPStruct)] Guid PowerSettingGuid,
            out int AcValueIndex
        );

        [DllImport(LibraryName, CharSet = CharSet.Unicode)]
        public static extern nuint PowerReadDCValueIndex(
            nint RootPowerKey,
            [MarshalAs(UnmanagedType.LPStruct)] Guid SchemeGuid,
            [MarshalAs(UnmanagedType.LPStruct)] Guid SubGroupOfPowerSettingsGuid,
            [MarshalAs(UnmanagedType.LPStruct)] Guid PowerSettingGuid,
            out int AcValueIndex
        );
    }
}