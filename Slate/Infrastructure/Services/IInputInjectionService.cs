using Glitonea.Mvvm;
using Slate.Model;

namespace Slate.Infrastructure.Services
{
    public interface IInputInjectionService : IService
    {
        void SimulateMediaKeyPress(MediaKey mediaKey);
    }
}