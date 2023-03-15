using Glitonea.Mvvm;

namespace Slate.Infrastructure.Services
{
    public interface IDisplayManagementService : IService
    {
        bool SetInternalDisplayRefreshRate(uint refreshRate);
        uint GetInternalDisplayRefreshRate();
    }
}