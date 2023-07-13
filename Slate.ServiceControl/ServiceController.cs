using System.Management;

namespace Slate.ServiceControl
{
    internal static class ServiceController
    {
        public static ServiceOperationResult SetStartMode(string name, ServiceStartMode startMode) 
            => (ServiceOperationResult)InvokeServiceMethod(name, "ChangeStartMode", startMode.ToString());

        public static ServiceOperationResult StopService(string name)
            => (ServiceOperationResult)InvokeServiceMethod(name, "StopService");

        public static ServiceOperationResult DeleteService(string name)
            => (ServiceOperationResult)InvokeServiceMethod(name, "Delete");
        
        private static object InvokeServiceMethod(string serviceName, string methodName, params object[] args)
        {
            using var managementObject = new ManagementObject($"Win32_Service.Name=\"{serviceName}\"");
            return managementObject.InvokeMethod(methodName, args);
        }
    }
}