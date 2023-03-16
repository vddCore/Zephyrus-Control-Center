using Slate.Infrastructure;
using Slate.Infrastructure.Asus;
using Slate.Infrastructure.Settings;
using Slate.Model.Messaging;

namespace Slate.Model.Settings.Components
{
    public class GraphicsAndDisplaySettings : SettingsComponent
    {
        public bool? IsEcoModeEnabled { get; set; }
        public bool? IsDisplayOverdriveEnabled { get; set; }
        public uint DisplayRefreshRate { get; set; } = 144;

        public MuxSwitchMode? MuxSwitchMode { get; set; }

        protected override void OnSettingsModified(string? propertyName)
        {
            switch (propertyName)
            {
                case nameof(IsEcoModeEnabled):
                {
                    new EcoModeChangedMessage(
                        IsEcoModeEnabled!.Value
                    ).Broadcast();

                    break;
                }

                case nameof(IsDisplayOverdriveEnabled):
                {
                    new DisplayOverdriveChangedMessage(
                        IsDisplayOverdriveEnabled!.Value
                    ).Broadcast();

                    break;
                }

                case nameof(DisplayRefreshRate):
                {
                    new DisplayRefreshRateChangedMessage(
                        DisplayRefreshRate
                    ).Broadcast();

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