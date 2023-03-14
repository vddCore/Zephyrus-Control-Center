using Slate.Infrastructure;
using Slate.Infrastructure.Settings;
using Slate.Model.Messaging;

namespace Slate.Model.Settings.Components
{
    public class GraphicsAndDisplaySettings : SettingsComponent
    {
        public bool? IsEcoModeEnabled { get; set; }
        public MuxSwitchMode? MuxSwitchMode { get; set; }

        protected override void OnSettingsModified(string? propertyName)
        {
            switch (propertyName)
            {
                case nameof(IsEcoModeEnabled):
                {
                    new EcoModeChangedMessage(IsEcoModeEnabled!.Value)
                        .Broadcast();

                    break;
                }

                case nameof(MuxSwitchMode):
                {
                    new MuxSwitchModeChangedMessage(
                        MuxSwitchMode!.Value
                    ).Broadcast();

                    break;
                }
            }
        }
    }
}