using Glitonea.Mvvm;

namespace Slate.Infrastructure.Services
{
    public interface IApplicationExecutionService : IService
    {
        void RunElevatedProcess(string path);
    }
}