using System;

namespace Slate.Infrastructure.Asus.Acpi
{
    public abstract class AsusAcpiEndpoint<T> where T : struct
    {
        private readonly AsusAcpiProxy _proxy;

        public WmnbAcpiMethod Method { get; }

        protected AsusAcpiEndpoint(AsusAcpiProxy proxy, WmnbAcpiMethod method)
        {
            _proxy = proxy;
            Method = method;
        }

        public int ReadInt32(T method, params byte[] args)
        {
            var methodData = BitConverter.GetBytes((int)Convert.ChangeType(method, TypeCode.Int32));
            var methodArguments = new byte[sizeof(int) + args.Length];
            
            Array.Copy(methodData, 0, methodArguments, 0, sizeof(int));
            Array.Copy(args, 0, methodArguments, sizeof(int), args.Length);
            
            return BitConverter.ToInt32(
                _proxy.Invoke(Method, args: methodArguments)
            );
        }

        public bool ReadBoolean(T method, params byte[] args)
        {
            return ReadInt32(method, args) != 0;
        }
    }
}