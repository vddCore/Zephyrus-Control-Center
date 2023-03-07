using System;
using System.IO;
using Slate.Infrastructure.Native;

namespace Slate.Infrastructure
{
    public class AsusAcpiProxy : IDisposable
    {
        private readonly nint _objectHandle;

        private const nuint AsusControlCode = 0x0022240C;

        private const int AsusDSTS = 0x53545344; // 'S' 'T' 'S' 'D'
        private const int AsusDEVS = 0x53564544; // 'S' 'V' 'E' 'D'

        public AsusAcpiProxy(string wmiDevicePath)
        {
            _objectHandle = Kernel32.CreateFile(
                wmiDevicePath,
                Kernel32.GENERIC_READ | Kernel32.GENERIC_WRITE,
                Kernel32.FILE_SHARE_READ | Kernel32.FILE_SHARE_WRITE,
                0,
                Kernel32.OPEN_EXISTING,
                Kernel32.FILE_ATTRIBUTE_NORMAL,
                0
            );

            if (_objectHandle < 0)
            {
                throw new IOException($"Failed to acquire ACPI interface at '{wmiDevicePath}'.");
            }
        }

        public int ReadInt32(AsusComponent component)
        {
            return BitConverter.ToInt32(
                Invoke(AsusDSTS, BitConverter.GetBytes((int)component))
            );
        }

        public bool ReadBoolean(AsusComponent component)
        {
            return ReadInt32(component) != 0;
        }

        public byte[] Invoke(int acpiMethod, params byte[] args)
        {
            var inBuffer = new byte[sizeof(int) * 2 + args.Length];
            var outBuffer = new byte[20];

            Array.Copy(
                BitConverter.GetBytes(acpiMethod), 0,
                inBuffer, 0, sizeof(int)
            );

            Array.Copy(
                BitConverter.GetBytes(args.Length), 0,
                inBuffer, 4, sizeof(int)
            );

            Array.Copy(args, 0, inBuffer, 8, args.Length);

            Write(AsusControlCode, inBuffer, outBuffer);

            return outBuffer;
        }

        public uint Write(nuint controlCode, byte[] input, byte[] output)
        {
            var requestSuccessful = Kernel32.DeviceIoControl(
                _objectHandle,
                controlCode,
                input,
                input.Length,
                output,
                output.Length,
                out var ret,
                0
            );

            if (!requestSuccessful)
            {
                throw new IOException("Unable to write to ACPI interface.");
            }

            return ret;
        }

        public void Dispose()
        {
            Kernel32.CloseHandle(_objectHandle);
        }
    }
}