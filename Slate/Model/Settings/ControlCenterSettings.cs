using Slate.Infrastructure.Settings;
using Slate.Model.Settings.Components;

namespace Slate.Model.Settings
{
    public class ControlCenterSettings : SettingsBase
    {
        public ApplicationSettings Application { get; set; } = new();
        public ProcessorSettings Processor { get; set; } = new();
    }
}