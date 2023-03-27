using Slate.Infrastructure.Settings;
using Slate.Model.Messaging;
using Starlight.Asus;

namespace Slate.Model.Settings.Components
{
    public class AniMeMatrixSettings : SettingsComponent
    {
        public BrightnessLevel Brightness { get; set; } = BrightnessLevel.Bright;

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
            }
        }
    }
}