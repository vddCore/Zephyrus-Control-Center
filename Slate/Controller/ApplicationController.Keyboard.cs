using System.Diagnostics;
using System.Management;
using Glitonea.Mvvm.Messaging;
using Slate.Infrastructure.Asus;
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

        private void SubscribeToAsusEventProvider()
        {
            _asusHalService.SubscribeToWmiEvent(OnAsusWmiEventReceived);
        }

        private void OnAsusWmiEventReceived(ManagementBaseObject managementObject)
        {
            if (int.TryParse(managementObject.Properties["EventID"].Value.ToString(), out var eventId))
            {
                switch ((SpecialKeyWmiEvent)eventId)
                {
                    case SpecialKeyWmiEvent.M3:
                        break;
                    
                    case SpecialKeyWmiEvent.M4:
                        break;
                    
                    case SpecialKeyWmiEvent.FnF4:
                        break;
                    
                    case SpecialKeyWmiEvent.FnF5:
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