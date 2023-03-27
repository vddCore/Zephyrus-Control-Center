using Glitonea.Mvvm.Messaging;
using Slate.Model.Messaging;
using Slate.Model.Settings.Components;

namespace Slate.Controller
{
    public partial class ApplicationController
    {
        private GraphicsAndDisplaySettings GraphicsAndDisplaySettings => ControlCenterSettings.GraphicsAndDisplay;

        private void SubscribeToGraphicsAndDisplaySettings()
        {
            Message.Subscribe<MuxSwitchModeChangedMessage>(this, OnGraphicsModeChanged);
            Message.Subscribe<EcoModeChangedMessage>(this, OnGraphicsPowerSavingModeChanged);
            Message.Subscribe<DisplayOverdriveChangedMessage>(this, OnDisplayOverdriveChanged);
            Message.Subscribe<DisplayRefreshRateChangedMessage>(this, OnDisplayRefreshRateChanged);
        }

        private void OnGraphicsModeChanged(MuxSwitchModeChangedMessage msg)
        {
            _asusHalService.SetGraphicsMode(msg.MuxSwitchMode);
        }
        
        private void OnGraphicsPowerSavingModeChanged(EcoModeChangedMessage msg)
        {
            _asusHalService.SetEcoMode(msg.Enabled);
        }
        
        private void OnDisplayOverdriveChanged(DisplayOverdriveChangedMessage msg)
        {
            _asusHalService.SetDisplayOverdrive(msg.Enabled);
        }
        
        private void OnDisplayRefreshRateChanged(DisplayRefreshRateChangedMessage msg)
        {
            _displayManagementService.SetInternalDisplayRefreshRate(msg.RefreshRate);
        }
    }
}