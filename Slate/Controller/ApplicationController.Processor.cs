using Glitonea.Mvvm.Messaging;
using Slate.Infrastructure.Asus;
using Slate.Model.Messaging;
using Slate.Model.Settings.Components;

namespace Slate.Controller
{
    public partial class ApplicationController
    {
        private ProcessorSettings ProcessorSettings => _settingsService.ControlCenter!.Processor;

        private void SubscribeToProcessorSettings()
        {
            Message.Subscribe<ManualCpuFanControlChangedMessage>(this, OnManualCpuFanControlChanged);         
            Message.Subscribe<CpuFanCurveUpdatedMessage>(this, OnCpuFanCurveUpdated);
        }

        private void OnManualCpuFanControlChanged(ManualCpuFanControlChangedMessage msg)
        {
            if (!msg.Enabled)
            {
                _asusHalService.WriteCpuFanCurve(
                    ProcessorSettings.FanCurve!
                );
            }
            else
            {
                /**
                 * Need to set a built-in preset manually to whatever because
                 * fan curve takes precedence over manual duty cycle.
                 **/
                _asusHalService.SetPerformancePreset(PerformancePreset.Performance);
                _asusHalService.SetCpuFanDutyCycle(msg.DutyCycle);
            }
        }
        
        private void OnCpuFanCurveUpdated(CpuFanCurveUpdatedMessage msg)
        {
            _asusHalService.WriteCpuFanCurve(msg.Curve);
        }
    }
}