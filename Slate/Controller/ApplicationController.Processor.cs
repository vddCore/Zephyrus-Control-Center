using Glitonea.Mvvm.Messaging;
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
        }

        private void OnManualCpuFanControlChanged(ManualCpuFanControlChangedMessage msg)
        {
            if (!msg.Enabled)
            {
                _asusHalService.SetPerformancePreset(
                    _settingsService.ControlCenter!.SelectedPreset
                );
            }
            else
            {
                _asusHalService.SetCpuFanDutyCycle(msg.DutyCycle);
            }
        }
    }
}