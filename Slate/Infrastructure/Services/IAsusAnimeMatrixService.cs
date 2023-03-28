using Glitonea.Mvvm;
using Starlight.Asus;
using Starlight.Asus.AnimeMatrix;

namespace Slate.Infrastructure.Services
{
    public interface IAsusAnimeMatrixService : IService
    {
        void SetBrightness(BrightnessLevel level);
        void SetBuiltInAnimationStatus(bool enabled);
        void SetBuiltInAnimation(AnimeMatrixBuiltIn builtIn);
    }
}