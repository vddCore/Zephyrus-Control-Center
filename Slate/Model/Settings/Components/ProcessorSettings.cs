using Slate.Infrastructure.Asus;
using Slate.Infrastructure.Settings;
using Slate.Model.Messaging;

namespace Slate.Model.Settings.Components
{
    public class ProcessorSettings : SettingsComponent
    {
        public bool IsBoostActiveOnAC { get; set; } = true;
        public bool IsBoostActiveOnDC { get; set; } = false;
        
        public FanCurve? FanCurve { get; set; }

        protected override void OnSettingsModified(string? propertyName)
        {
            switch (propertyName)
            {
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