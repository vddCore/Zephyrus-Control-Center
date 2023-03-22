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
            Debug.WriteLine("WMI event {");
            Debug.WriteLine("  Properties: {");
            foreach (var prop in managementObject.Properties)
            {
                Debug.WriteLine($"    {prop.Name}: {prop.Value}");
            }
            Debug.WriteLine("  }");
            Debug.WriteLine("}");
            
            if (int.TryParse(managementObject.Properties["EventID"].Value.ToString(), out var eventId))
            {
                switch ((AtkWmiEventID)eventId)
                {
                    default: break;
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