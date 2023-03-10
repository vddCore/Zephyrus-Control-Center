using System.Text.Json.Serialization;
using Slate.Infrastructure.Asus;
using Slate.Model.Settings.Components;

namespace Slate.Model.Settings
{
    public class ControlCenterSettings
    {
        [JsonPropertyName("selected_preset")]
        public PerformancePreset SelectedPreset { get; set; } = PerformancePreset.Balanced;

        [JsonPropertyName("application")]
        public ApplicationSettings Application { get; set; } = new();
        
        [JsonPropertyName("processor")]
        public ProcessorSettings Processor { get; set; } = new();
        
        [JsonPropertyName("graphics")]
        public GraphicsSettings Graphics { get; set; } = new();
    }
}