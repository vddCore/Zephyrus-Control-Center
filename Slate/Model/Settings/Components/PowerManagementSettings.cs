using Slate.Infrastructure.Settings;

namespace Slate.Model.Settings.Components
{
    public class PowerManagementSettings : SettingsComponent
    {
        public bool IsProcessorBoostActiveOnAC { get; set; }
        public bool IsProcessorBoostActiveOnDC { get; set; }
    }
}