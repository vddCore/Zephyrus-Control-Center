using System;
using System.IO;
using Slate.Infrastructure.Asus.Acpi.Endpoints;
using Slate.Infrastructure.Asus.Acpi.FunctionSets;
using Slate.Infrastructure.Native;

namespace Slate.Infrastructure.Asus.Acpi
{
    public class AsusAcpiProxy : IDisposable
    {
        private readonly nint _objectHandle;

        public AsusDstsEndpoint DSTS { get; }
        public AsusDevsEndpoint DEVS { get; }

        public AsusAcpiProxy(string acpiDevicePath = @"\\.\\ATKACPI")
        {
            _objectHandle = Kernel32.CreateFile(
                acpiDevicePath,
                Kernel32.GENERIC_READ | Kernel32.GENERIC_WRITE,
                Kernel32.FILE_SHARE_READ | Kernel32.FILE_SHARE_WRITE,
                0,
                Kernel32.OPEN_EXISTING,
                Kernel32.FILE_ATTRIBUTE_NORMAL,
                0
            );

            if (_objectHandle < 0)
            {
                throw new IOException($"Failed to acquire ACPI interface at '{acpiDevicePath}'.");
            }

            DSTS = new AsusDstsEndpoint(this);
            DEVS = new AsusDevsEndpoint(this);
        }

        public byte[] InvokeWMI(WmnbFunction wmnbFunction, int outBufferSize = 20, params byte[] args)
        {
            var inBuffer = new byte[sizeof(int) * 2 + args.Length];
            var outBuffer = new byte[outBufferSize];

            Array.Copy(
                BitConverter.GetBytes((int)wmnbFunction), 0,
                inBuffer, 0, sizeof(int)
            );

            Array.Copy(
                BitConverter.GetBytes(args.Length), 0,
                inBuffer, 4, sizeof(int)
            );

            Array.Copy(args, 0, inBuffer, 8, args.Length);

            unsafe
            {
                fixed (byte* inPtr = inBuffer)
                fixed (byte* outPtr = outBuffer)
                {
                    Write(AsusIoCtl.InvokeWmnbFunction, inPtr, inBuffer.Length, outPtr, outBuffer.Length);
                }
            }

            return outBuffer;
        }

        private unsafe uint Write(AsusIoCtl ioctl, byte* input, int inputLength, byte* output, int outputLength)
        {
            var requestSuccessful = Kernel32.DeviceIoControl(
                _objectHandle,
                (nuint)ioctl,
                input,
                inputLength,
                output,
                outputLength,
                out var ret,
                0
            );

            if (!requestSuccessful)
            {
                throw new IOException($"Unable to write to ACPI interface: {Kernel32.GetLastError():X8}");
            }

            return ret;
        }

        public void Dispose()
        {
            Kernel32.CloseHandle(_objectHandle);
        }
    }
}