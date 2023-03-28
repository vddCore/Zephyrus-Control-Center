using Slate.Infrastructure.Settings;
using Slate.Model.Messaging;
using Starlight.Asus;
using Starlight.Asus.AnimeMatrix;

namespace Slate.Model.Settings.Components
{
    public class AniMeMatrixSettings : SettingsComponent
    {
        public BrightnessLevel Brightness { get; set; } = BrightnessLevel.Bright;

        public bool BuiltInAnimationEnabled { get; set; } = true;
        public AnimeMatrixBuiltIn.Running RunningBuiltIn { get; set; } = AnimeMatrixBuiltIn.Running.RogLogoGlitch;
        public AnimeMatrixBuiltIn.Sleeping SleepingBuiltIn { get; set; } = AnimeMatrixBuiltIn.Sleeping.Starfield;
        public AnimeMatrixBuiltIn.Shutdown ShutdownBuiltIn { get; set; } = AnimeMatrixBuiltIn.Shutdown.GlitchOut;
        public AnimeMatrixBuiltIn.Startup StartupBuiltIn { get; set; } = AnimeMatrixBuiltIn.Startup.GlitchConstruction;

        protected override void OnSettingsModified(string? propertyName)
        {
            switch (propertyName)
            {
                case nameof(Brightness):
                {
                    new AniMeMatrixBrightnessChangedMessage(Brightness)
                        .Broadcast();

                    break;
                }

                case nameof(BuiltInAnimationEnabled):
                case nameof(StartupBuiltIn):
                case nameof(RunningBuiltIn):
                case nameof(SleepingBuiltIn):
                case nameof(ShutdownBuiltIn):
                {
                    new AniMeMatrixBuiltInsChangedMessage(
                        BuiltInAnimationEnabled,
                        new AnimeMatrixBuiltIn(
                            RunningBuiltIn,
                            SleepingBuiltIn,
                            ShutdownBuiltIn,
                            StartupBuiltIn
                        )
                    ).Broadcast();
                    
                    break;
                }
            }
        }
    }
}