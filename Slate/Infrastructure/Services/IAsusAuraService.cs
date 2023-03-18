using System.Drawing;
using Glitonea.Mvvm;
using Starlight.Asus.Aura;

namespace Slate.Infrastructure.Services
{
    public interface IAsusAuraService : IService
    {
        bool IsAvailable { get; }

        void WriteAuraSettings(
            AuraAnimation animation,
            Color primaryColor,
            Color secondaryColor,
            AuraAnimationSpeed speed
        );
    }
}