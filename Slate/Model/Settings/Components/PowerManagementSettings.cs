using Slate.Infrastructure.Settings;

namespace Slate.Model.Settings.Components
{
    public class PowerManagementSettings : SettingsComponent
    {
        public bool IsProcessorBoostActiveOnAC { get; set; } = true;
        public bool IsProcessorBoostActiveOnDC { get; set; } = false;
        public int BatteryChargingThreshold { get; set; } = 85; /* percent of course */
    }
}