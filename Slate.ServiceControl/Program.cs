using Mono.Options;

namespace Slate.ServiceControl
{
    internal static class Program
    {
        private static bool _showHelp;
        private static readonly List<string> _services = new();

        internal static int Main(string[] args)
        {
            var options = new OptionSet
            {
                {
                    "s|services=",
                    "Semicolon-separated list of services to stop, disable and then remove.",
                    v => _services.AddRange(v.Split(';'))
                },

                {
                    "h|help",
                    "This message.",
                    _ => _showHelp = true
                }
            };

            if (_showHelp)
            {
                options.WriteOptionDescriptions(Console.Out);
                return 0;
            }

            try
            {
                options.Parse(args);
            }
            catch (OptionException e)
            {
                options.WriteOptionDescriptions(Console.Out);
                return -1;
            }

            ServiceOperationResult operationResult;
            foreach (var service in _services)
            {
                if ((operationResult = ServiceController.StopService(service)) != ServiceOperationResult.Success
                    && operationResult != ServiceOperationResult.ServiceNotStarted)
                    return (int)operationResult;

                if ((operationResult = ServiceController.DeleteService(service)) != ServiceOperationResult.Success
                    && operationResult != ServiceOperationResult.InvalidServiceName)
                    return (int)operationResult;
            }

            return 0;
        }
    }
}