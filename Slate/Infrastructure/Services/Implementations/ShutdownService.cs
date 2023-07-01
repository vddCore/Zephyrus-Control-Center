using System.Diagnostics;

namespace Slate.Infrastructure.Services.Implementations
{
    public class ShutdownService : IShutdownService
    {
        public void RequestSystemReboot(string reason)
        {
            Process.Start(
                new ProcessStartInfo("shutdown.exe", $"/r /f /t 0 /c \"{reason}\"")
                {
                    UseShellExecute = true
                }
            );
        }
    }
}