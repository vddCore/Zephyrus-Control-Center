using Glitonea.Mvvm.Messaging;
using Slate.Infrastructure.Asus;
using Slate.Model;
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

        private void HandleSpecialKeyPress(AtkWmiEventID ev)
        {
            switch (ev)
            {
                case AtkWmiEventID.KeyPressM3:
                    HandleKeyPress(KeyboardSettings.BindM3, ev);
                    break;
                
                case AtkWmiEventID.KeyPressM4:
                    HandleKeyPress(KeyboardSettings.BindM4, ev);
                    break;
                
                case AtkWmiEventID.KeyPressFnF4:
                    HandleKeyPress(KeyboardSettings.BindF4, ev);
                    break;
                
                case AtkWmiEventID.KeyPressFnF5:
                    HandleKeyPress(KeyboardSettings.BindF5, ev);
                    break;
            }
        }

        private void HandleKeyPress(KeyBind keyBind, AtkWmiEventID ev)
        {
            switch (keyBind.Mode)
            {
                case KeyBindMode.Default:
                    break;
                
                case KeyBindMode.Media:
                    _inputInjectionService.SimulateMediaKeyPress(keyBind.MediaKey);
                    break;
                
                case KeyBindMode.Command:
                    break;
            }
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