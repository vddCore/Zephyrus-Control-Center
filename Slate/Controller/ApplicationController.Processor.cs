using Glitonea.Mvvm.Messaging;
using Slate.Infrastructure.Asus;
using Slate.Infrastructure.PowerManagement;
using Slate.Model.Messaging;
using Slate.Model.Settings.Components;

namespace Slate.Controller
{
    public partial class ApplicationController
    {
        private ProcessorSettings ProcessorSettings => _settingsService.ControlCenter!.Processor;

        private void SubscribeToProcessorSettings()
        {
            Message.Subscribe<CpuFanCurveUpdatedMessage>(this, OnCpuFanCurveUpdated);
            Message.Subscribe<CpuBoostModeChangedMessage>(this, OnCpuBoostModeChanged);
        }

        private void OnCpuFanCurveUpdated(CpuFanCurveUpdatedMessage msg)
        {
            _asusHalService.WriteCpuFanCurve(msg.Curve);
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
    }
}