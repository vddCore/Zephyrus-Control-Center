using Glitonea.Mvvm.Messaging;
using Slate.Model.Messaging;
using Slate.Model.Settings.Components;

namespace Slate.Controller
{
    public partial class ApplicationController
    {
        private KeyboardSettings KeyboardSettings => _settingsService.ControlCenter!.Keyboard;

        private void SubscribeToKeyboardSettings()
        {
            Message.Subscribe<AuraSettingsChangedMessage>(this, OnAuraSettingsChanged);
        }

        private void OnAuraSettingsChanged(AuraSettingsChangedMessage msg)
        {
            _asusAuraService.WriteAuraSettings(
                msg.Animation,
                msg.PrimaryColor,
                msg.SecondaryColor,
                msg.AnimationSpeed
            );
        }
    }
}