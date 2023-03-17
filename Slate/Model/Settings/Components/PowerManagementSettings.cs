using Slate.Infrastructure.Settings;
using Slate.Model.Messaging;

namespace Slate.Model.Settings.Components
{
    public class PowerManagementSettings : SettingsComponent
    {
        public bool IsProcessorBoostActiveOnAC { get; set; } = true;
        public bool IsProcessorBoostActiveOnDC { get; set; } = false;
        
        public int BatteryChargeLimit { get; set; } = 85; /* percent of course */
        public byte TotalSystemPPT { get; set; } = 30;
        public byte ProcessorPPT { get; set; }

        protected override void OnSettingsModified(string? propertyName)
        {
            switch (propertyName)
            {
                case nameof(IsProcessorBoostActiveOnAC):
                case nameof(IsProcessorBoostActiveOnDC):
                {
                    new CpuBoostModeChangedMessage(
                        IsProcessorBoostActiveOnAC,
                        IsProcessorBoostActiveOnDC
                    ).Broadcast();
                    
                    break;
                }

                case nameof(BatteryChargeLimit):
                {
                    new BatteryChargeLimitChangedMessage(
                        BatteryChargeLimit
                    ).Broadcast();
                    
                    break;
                }
            }
        }
    }
}