using Slate.Infrastructure.Asus;
using Slate.Infrastructure.Settings;
using Slate.Model.Messaging;

namespace Slate.Model.Settings.Components
{
    public class ProcessorSettings : SettingsComponent
    {
        public bool ManualFanControlEnabled { get; set; } = false;
        public byte ManualFanDutyCycle { get; set; } = 64;

        public bool IsBoostActiveOnAC { get; set; } = true;
        public bool IsBoostActiveOnDC { get; set; } = false;
        
        public FanCurve? FanCurve { get; set; }

        protected override void OnSettingsModified(string? propertyName)
        {
            switch (propertyName)
            {
                case nameof(ManualFanDutyCycle) when ManualFanControlEnabled:
                case nameof(ManualFanControlEnabled):
                {
                    new ManualCpuFanControlChangedMessage(ManualFanControlEnabled, ManualFanDutyCycle)
                        .Broadcast();

                    break;
                }

                case nameof(FanCurve):
                {
                    new CpuFanCurveUpdatedMessage(FanCurve!)
                        .Broadcast();

                    break;
                }

                case nameof(IsBoostActiveOnAC):
                case nameof(IsBoostActiveOnDC):
                {
                    new CpuBoostModeChangedMessage(IsBoostActiveOnAC, IsBoostActiveOnDC)
                        .Broadcast();

                    break;
                }
            }
        }
    }
}