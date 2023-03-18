using System;
using System.Drawing;
using Starlight.Asus.Aura;

namespace Slate.Infrastructure.Services
{
    public class AsusAuraService : IAsusAuraService, IDisposable
    {
        private readonly AuraDevice? _auraDevice;

        public bool IsAvailable => _auraDevice != null;

        public AsusAuraService()
        {
            try
            {
                _auraDevice = new AuraDevice();
            }
            catch
            {
                /* Ignore any exceptions. */
            }
        }

        public void WriteAuraSettings(
            AuraAnimation animation,
            Color primaryColor,
            Color secondaryColor,
            AuraAnimationSpeed speed
        )
        {
            ThrowIfUnavailable();
            
            _auraDevice!.Mode.Animate(
                0,
                animation,
                primaryColor,
                secondaryColor,
                speed
            );
        }

        public void Dispose()
            => _auraDevice?.Dispose();

        private void ThrowIfUnavailable()
        {
            if (!IsAvailable)
                throw new InvalidOperationException("ASUS Aura device was not found on your system.");
        }
    }
}