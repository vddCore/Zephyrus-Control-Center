using Slate.Infrastructure.Settings;

namespace Slate.Model.Settings.Components
{
    public class ProcessorSettings : SettingsComponent
    {
        public bool ManualFanControlEnabled { get; set; } = false;
        public byte ManualFanDutyCycle { get; set; } = 64;
    }
}