using System.Diagnostics;
using System.IO;

namespace Slate.Infrastructure.Services.Implementations
{
    public class ApplicationExecutionService : IApplicationExecutionService
    {
        public void RunElevatedProcess(string path)
        {
            Process.Start(new ProcessStartInfo
            {
                UseShellExecute = true,
                WorkingDirectory = Path.GetDirectoryName(path),
                FileName = path,
                Verb = "runas"
            });
        }
    }
}