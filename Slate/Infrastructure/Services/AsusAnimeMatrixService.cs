using System;
using Starlight.Asus;
using Starlight.Asus.AnimeMatrix;

namespace Slate.Infrastructure.Services
{
    public class AsusAnimeMatrixService : IAsusAnimeMatrixService, IDisposable
    {
        private readonly AnimeMatrixDevice _device;

        public AsusAnimeMatrixService()
        {
            _device = new AnimeMatrixDevice();
        }
        
        public void SetBrightness(BrightnessLevel level) 
            => _device.SetBrightness(level);

        public void SetBuiltInAnimation(bool enable, AnimeMatrixBuiltIn builtIn) 
            => _device.SetBuiltInAnimation(enable, builtIn);

        public void Dispose()
        {
            _device.Dispose();
        }
    }
}