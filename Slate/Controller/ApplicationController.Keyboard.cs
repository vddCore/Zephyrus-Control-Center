using System.Diagnostics;
using System.IO;
using Avalonia.Threading;
using Glitonea.Mvvm.Messaging;
using Slate.Infrastructure.Asus;
using Slate.Model;
using Slate.Model.Messaging;
using Slate.Model.Settings.Components;

namespace Slate.Controller
{
    public partial class ApplicationController
    {
        private KeyboardSettings KeyboardSettings => ControlCenterSettings.Keyboard;

        private void SubscribeToKeyboardSettings()
        {
            Message.Subscribe<AuraSettingsChangedMessage>(this, OnAuraSettingsChanged);
        }

        private void HandleSpecialKeyPress(AtkWmiEventID ev)
        {
            switch (ev)
            {
                case AtkWmiEventID.KeyPressM3:
                    HandleKeyPress(KeyboardSettings.BindM3);
                    break;

                case AtkWmiEventID.KeyPressM4:
                    HandleKeyPress(KeyboardSettings.BindM4);
                    break;

                case AtkWmiEventID.KeyPressFnF4:
                    HandleKeyPress(KeyboardSettings.BindF4);
                    break;

                case AtkWmiEventID.KeyPressFnF5:
                    HandleKeyPress(KeyboardSettings.BindF5);
                    break;
            }
        }

        private void HandleKeyPress(KeyBind keyBind)
        {
            switch (keyBind.Mode)
            {
                case KeyBindMode.Default:
                {
                    Dispatcher.UIThread.Post(() =>
                    {
                        Message.Broadcast<TrayIconClickedMessage>();
                    });
                    
                    break;
                }

                case KeyBindMode.Media:
                    _inputInjectionService.SimulateMediaKeyPress(keyBind.MediaKey);
                    break;

                case KeyBindMode.Command:
                {
                    new Process
                    {
                        StartInfo = new ProcessStartInfo(keyBind.Command ?? string.Empty)
                        {
                            WorkingDirectory = Path.GetDirectoryName(keyBind.Command)
                        }
                    }.Start();
                    
                    break;
                }
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