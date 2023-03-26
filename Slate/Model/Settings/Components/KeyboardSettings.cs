using System.Drawing;
using Slate.Infrastructure.Settings;
using Slate.Model.Messaging;
using Starlight.Asus.Aura;

namespace Slate.Model.Settings.Components
{
    public class KeyboardSettings : SettingsComponent
    {
        public bool FollowSystemAccentColor { get; set; } = false;

        public AuraAnimation Animation { get; set; } = AuraAnimation.Static;
        public ColorWrapper PrimaryColor { get; set; } = new(Color.White);
        public ColorWrapper SecondaryColor { get; set; } = new(Color.White);
        public AuraAnimationSpeed AnimationSpeed { get; set; } = AuraAnimationSpeed.Medium;

        public KeyBind BindM3 { get; set; } = new(KeyBindMode.Default);
        public KeyBind BindM4 { get; set; } = new(KeyBindMode.Default);
        public KeyBind BindF4 { get; set; } = new(KeyBindMode.Default);
        public KeyBind BindF5 { get; set; } = new(KeyBindMode.Default);

        protected override void OnSettingsModified(string? propertyName)
        {
            switch (propertyName)
            {
                case nameof(Animation):
                case nameof(PrimaryColor):
                case nameof(SecondaryColor):
                case nameof(AnimationSpeed):
                {
                    new AuraSettingsChangedMessage(
                        Animation,
                        PrimaryColor.HardwareColor,
                        SecondaryColor.HardwareColor,
                        AnimationSpeed
                    ).Broadcast();

                    break;
                }

                case nameof(FollowSystemAccentColor):
                {
                    new AuraFollowSystemAccentColorStateChangedMessage(
                        FollowSystemAccentColor
                    ).Broadcast();
                    
                    break;
                }
            }
        }
    }
}