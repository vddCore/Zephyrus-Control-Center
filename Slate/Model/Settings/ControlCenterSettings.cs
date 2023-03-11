using System.Text.Json.Serialization;
using Slate.Infrastructure.Asus;
using Slate.Infrastructure.Settings;
using Slate.Model.Messaging;
using Slate.Model.Settings.Components;

namespace Slate.Model.Settings
{
    public class ControlCenterSettings : SettingsBase
    {
        [JsonPropertyName("application")]
        public ApplicationSettings Application { get; set; } = new();

        [JsonPropertyName("processor")]
        public ProcessorSettings Processor { get; set; } = new();
    }
}