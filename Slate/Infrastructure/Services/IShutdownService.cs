using Glitonea.Mvvm;

namespace Slate.Infrastructure.Services
{
    public interface IShutdownService : IService
    {
        void RequestSystemReboot(string reason);
    }
}