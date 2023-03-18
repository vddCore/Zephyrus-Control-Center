using Slate.Infrastructure.Settings;
using Slate.Model.Settings.Components;

namespace Slate.Model.Settings
{
    public class ControlCenterSettings : SettingsBase
    {
        public ApplicationSettings Application { get; set; } = new();
        public FansSettings Fans { get; set; } = new();
        public GraphicsAndDisplaySettings GraphicsAndDisplay { get; set; } = new();
        public PowerManagementSettings PowerManagement { get; set; } = new();
        public KeyboardSettings Keyboard { get; set; } = new();
    }
}