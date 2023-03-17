using Glitonea.Mvvm.Messaging;
using Slate.Infrastructure.PowerManagement;
using Slate.Model.Messaging;
using Slate.Model.Settings.Components;

namespace Slate.Controller
{
    public partial class ApplicationController
    {
        private PowerManagementSettings PowerManagementSettings => _settingsService.ControlCenter!.PowerManagement;
        
        private void SubscribeToPowerManagementSettings()
        {
            Message.Subscribe<CpuBoostModeChangedMessage>(this, OnCpuBoostModeChanged);
            Message.Subscribe<BatteryChargeLimitChangedMessage>(this, OnBatteryChargeLimitChanged);
        }

        private void OnCpuBoostModeChanged(CpuBoostModeChangedMessage msg)
        {
            _powerManagementService.WriteProcessorBoostState(
                PowerMode.AC, 
                msg.EnableOnAC ? ProcessorBoostLevel.Aggressive : ProcessorBoostLevel.Disabled
            );
            
            _powerManagementService.WriteProcessorBoostState(
                PowerMode.DC,
                msg.EnableOnDC ? ProcessorBoostLevel.Aggressive : ProcessorBoostLevel.Disabled
            );

            _powerManagementService.CommitCurrentPowerSchemeChanges();
        }
        
        private void OnBatteryChargeLimitChanged(BatteryChargeLimitChangedMessage msg)
        {
            _asusHalService.SetBatteryChargeTarget(msg.Value);
        }
    }
}