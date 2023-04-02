using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Win32;
using Slate.Asus.Native;

namespace Slate.Asus.Acpi
{
    public static class AcpiDumper
    {
        private const int AcpiId = 0x41435049; // 'A' 'C' 'P' 'I'

        public static int[] FetchFirmwareAcpiTableList(bool skipLicenseKeyTable = true)
        {
            var dataSize = Kernel32.EnumSystemFirmwareTables(AcpiId, null, 0);
            var data = new byte[dataSize];

            if (dataSize > 0)
            {
                if (Kernel32.EnumSystemFirmwareTables(AcpiId, data, dataSize) > 0)
                {
                    var ret = new int[dataSize / 4];
                    Buffer.BlockCopy(data, 0, ret, 0, (int)dataSize);

                    return ret.Where(
                        x => x.ToFourCharacterCode() != "SSDT" 
                             && (skipLicenseKeyTable && x.ToFourCharacterCode() != "MSDM")
                    ).Distinct().ToArray();
                }
            }

            return new int[0];
        }
        
        public static void DumpFirmwareAcpiTable(int tableId, Stream outStream)
        {
            var dataSize = Kernel32.GetSystemFirmwareTable(
                AcpiId,
                tableId,
                null,
                0
            );

            var data = new byte[dataSize];
            Kernel32.GetSystemFirmwareTable(
                AcpiId,
                tableId,
                data,
                dataSize
            );

            outStream.Write(data);
        }
        
        public static int[] FetchRegistryAcpiTableList(bool skipLicenseKeyTable)
        {
            using (var hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Default))
            {
                using (var subKey = hklm.OpenSubKey($"HARDWARE\\ACPI"))
                {
                    return subKey!
                        .GetSubKeyNames()
                        .Select(x => x.ToFourCharacterCodeInteger())
                        .Where(x => skipLicenseKeyTable && x.ToFourCharacterCode() != "MSDM")
                        .ToArray();
                }
            }
        }
        
        public static void DumpRegistryAcpiTable(int tableId, Stream outStream)
        {
            var data = ReadAcpiTableFromRegistry(tableId.ToFourCharacterCode());
            outStream.Write(data);
        }
        
        private static byte[] ReadAcpiTableFromRegistry(string fourcc)
        {
            // We drill into the registry for SSDTs & co.
            // precisely because GetSystemFirmwareTable
            // is absolute Dogshit. For fuck's sake, why
            // couldn't it be an enumerator?
            //
            var disposeList = new List<RegistryKey>();

            using (var hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Default))
            {
                using (var subKey = hklm.OpenSubKey($"HARDWARE\\ACPI\\{fourcc}"))
                {
                    var currentKey = subKey;
                    object? value;

                    do
                    {
                        var subKeyNames = currentKey!.GetSubKeyNames();

                        value = currentKey.GetValue("00000000");
                        if (value != null)
                            break;

                        if (subKeyNames.Length == 0)
                        {
                            // We've drilled as deep as we can and still no data.
                            // Unfortunate.
                            break;
                        }

                        currentKey = currentKey.OpenSubKey(subKeyNames[0]);
                        disposeList.Add(currentKey!);
                    } while (value == null);

                    foreach (var key in disposeList)
                        key.Dispose();

                    if (value == null)
                    {
                        throw new InvalidOperationException($"No registry data for {fourcc}.");
                    }

                    return (byte[])value;
                }
            }
        }
    }
}