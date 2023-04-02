using System;
using Slate.Asus.Acpi.FunctionSets;

namespace Slate.Asus.Acpi
{
    public abstract class AsusAcpiEndpoint<T> where T : struct
    {
        private readonly AsusAcpiProxy _proxy;

        public WmnbFunction Method { get; }

        protected AsusAcpiEndpoint(AsusAcpiProxy proxy, WmnbFunction method)
        {
            _proxy = proxy;
            Method = method;
        }

        public byte[] ReadBytes(T method, int count, params byte[] args)
        {
            var methodData = BitConverter.GetBytes((int)Convert.ChangeType(method, TypeCode.Int32));
            var methodArguments = new byte[sizeof(int) + args.Length];
            
            Array.Copy(methodData, 0, methodArguments, 0, sizeof(int));
            Array.Copy(args, 0, methodArguments, sizeof(int), args.Length);

            return _proxy.InvokeWMI(Method, count, args: methodArguments);
        }

        public int ReadInt32(T method, params byte[] args)
        {
            return BitConverter.ToInt32(
                ReadBytes(method, sizeof(int), args)
            );
        }

        public bool ReadBoolean(T method, params byte[] args)
        {
            return ReadInt32(method, args) != 0;
        }
    }
}