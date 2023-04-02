using Slate.Asus;
using Slate.Infrastructure.Settings;
using Slate.Model.Messaging;

namespace Slate.Model.Settings.Components
{
    public class FansSettings : SettingsComponent
    {
        public FanCurve? CpuFanCurve { get; set; }
        public FanCurve? GpuFanCurve { get; set; }

        public PerformancePreset PerformancePreset { get; set; } = PerformancePreset.Balanced;
        public bool IsManualOverrideEnabled { get; set; } = false;
        public byte ManualCpuFanDutyCycle { get; set; } = 64;
        public byte ManualGpuFanDutyCycle { get; set; } = 64;

        protected override void OnSettingsModified(string? propertyName)
        {
            switch (propertyName)
            {
                case nameof(PerformancePreset):
                {
                    new PerformancePresetUpdatedMessage(PerformancePreset)
                        .Broadcast();
                    
                    break;
                }
                    
                case nameof(CpuFanCurve):
                {
                    new CpuFanCurveUpdatedMessage(CpuFanCurve!)
                        .Broadcast();

                    break;
                }

                case nameof(GpuFanCurve):
                {
                    new GpuFanCurveUpdatedMessage(GpuFanCurve!)
                        .Broadcast();

                    break;
                }

                case nameof(IsManualOverrideEnabled):
                case nameof(ManualCpuFanDutyCycle) when IsManualOverrideEnabled:
                case nameof(ManualGpuFanDutyCycle) when IsManualOverrideEnabled:
                {
                    new ManualFanOverrideUpdatedMessage(
                        IsManualOverrideEnabled,
                        ManualCpuFanDutyCycle,
                        ManualGpuFanDutyCycle
                    ).Broadcast();

                    break;
                }
            }
        }
    }
}