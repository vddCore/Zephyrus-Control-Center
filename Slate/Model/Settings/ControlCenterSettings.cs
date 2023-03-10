using System.Text.Json.Serialization;
using Slate.Infrastructure.Asus;
using Slate.Infrastructure.Settings;
using Slate.Model.Messaging;
using Slate.Model.Settings.Components;

namespace Slate.Model.Settings
{
    public class ControlCenterSettings : SettingsBase
    {
        [JsonPropertyName("selected_preset")]
        public PerformancePreset SelectedPreset { get; set; } = PerformancePreset.Balanced;

        [JsonPropertyName("application")]
        public ApplicationSettings Application { get; set; } = new();

        [JsonPropertyName("processor")]
        public ProcessorSettings Processor { get; set; } = new();

        protected override void OnSettingsModified(string? propertyName)
        {
            WithEventSuppressed(() =>
            {
                switch (propertyName)
                {
                    case nameof(SelectedPreset):
                    {
                        new PerformancePresetChangedMessage(SelectedPreset)
                            .Broadcast();
                        
                        break;
                    }
                }
            });
        }
    }
}