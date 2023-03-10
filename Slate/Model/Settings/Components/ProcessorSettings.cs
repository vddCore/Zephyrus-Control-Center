using Slate.Infrastructure.Settings;
using Slate.Model.Messaging;

namespace Slate.Model.Settings.Components
{
    public class ProcessorSettings : SettingsComponent
    {
        public bool ManualFanControlEnabled { get; set; } = false;
        public byte ManualFanDutyCycle { get; set; } = 64;
        
        public byte[] FanCurve { get; set; } = new byte[8];

        protected override void OnSettingsModified(string? propertyName)
        {
            WithEventSuppressed(() =>
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
                        new CpuFanCurveUpdatedMessage(FanCurve)
                            .Broadcast();

                        break;
                    }
                }
            });
        }
    }
}