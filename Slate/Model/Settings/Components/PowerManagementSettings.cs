using Slate.Infrastructure.Settings;
using Slate.Model.Messaging;

namespace Slate.Model.Settings.Components
{
    public class PowerManagementSettings : SettingsComponent
    {
        public bool IsProcessorBoostActiveOnAC { get; set; } = true;
        public bool IsProcessorBoostActiveOnDC { get; set; } = false;
        
        public int BatteryChargeLimit { get; set; } = 85; /* percent of course */
        public byte TotalSystemPPT { get; set; } = 90;
        public byte ProcessorPPT { get; set; } = 60;

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

                case nameof(TotalSystemPPT):
                case nameof(ProcessorPPT):
                {
                    WithEventSuppressed(() =>
                    {
                        if (ProcessorPPT > TotalSystemPPT)
                            ProcessorPPT = TotalSystemPPT;
                    });
                    
                    new PowerTargetsChangedMessage(
                        TotalSystemPPT,
                        ProcessorPPT
                    ).Broadcast();
                    
                    break;
                }
            }
        }
    }
}