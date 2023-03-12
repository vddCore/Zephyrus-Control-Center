using Slate.Infrastructure.Asus;
using Slate.Infrastructure.Settings;
using Slate.Model.Messaging;

namespace Slate.Model.Settings.Components
{
    public class GraphicsAndDisplaySettings : SettingsComponent
    {
        public FanCurve? FanCurve { get; set; }

        protected override void OnSettingsModified(string? propertyName)
        {
            switch (propertyName)
            {
                case nameof(FanCurve):
                {
                    new GpuFanCurveUpdatedMessage(
                        FanCurve!
                    ).Broadcast();
                    
                    break;
                }
            }
        }
    }
}