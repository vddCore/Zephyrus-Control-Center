using Slate.Infrastructure.Settings;

namespace Slate.Model.Settings.Components
{
    public class PowerManagementSettings : SettingsComponent
    {
        public bool IsBoostActiveOnAC { get; set; }
        public bool IsBoostActiveOnDC { get; set; }
    }
}