using Glitonea.Mvvm;
using Starlight.Asus;

namespace Slate.Infrastructure.Services
{
    public interface IAsusAnimeMatrixService : IService
    {
        void SetBrightness(BrightnessLevel level);
    }
}