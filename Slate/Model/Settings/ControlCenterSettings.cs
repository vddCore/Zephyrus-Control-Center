using System.Text.Json.Serialization;
using Slate.Model.Settings.Components;

namespace Slate.Model.Settings
{
    public class ControlCenterSettings
    {
        [JsonPropertyName("application")]
        public ApplicationSettings Application { get; set; } = new();
        
        [JsonPropertyName("processor")]
        public ProcessorSettings Processor { get; set; } = new();
        
        [JsonPropertyName("graphics")]
        public GraphicsSettings Graphics { get; set; } = new();
    }
}