using System.ComponentModel;
using Glitonea.Mvvm;
using Glitonea.Mvvm.Messaging;
using Slate.Infrastructure.Services;
using Slate.Model.Messaging;
using Slate.Model.Settings.Components;

namespace Slate.ViewModel.Page
{
    public class ProcessorPageViewModel : ViewModelBase
    {
        private readonly IAsusHalService _asusHalService;
        private readonly ISettingsService _settingsService;

        public ProcessorSettings ProcessorSettings => _settingsService.ControlCenter!.Processor;

        public ProcessorPageViewModel(
            IAsusHalService asusHalService,
            ISettingsService settingsService)
        {
            _asusHalService = asusHalService;
            _settingsService = settingsService;
            
            Message.Subscribe<PerformancePresetChangedMessage>(this, OnPerformancePresetChanged);

            ProcessorSettings.PropertyChanged += ProcesorSettings_PropertyChanged!;
        }

        private void ProcesorSettings_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(ProcessorSettings.ManualFanDutyCycle):
                {
                    SetFanDutyCycle();
                    break;
                }

                case nameof(ProcessorSettings.ManualFanControlEnabled):
                {
                    if (ProcessorSettings.ManualFanControlEnabled)
                    {
                        SetFanDutyCycle();
                    }
                    else
                    {
                        new PerformancePresetChangedMessage(_settingsService.ControlCenter!.SelectedPreset)
                            .Broadcast();
                    }
                    break;
                }
            }
        }
        
        private void OnPerformancePresetChanged(PerformancePresetChangedMessage obj)
        {
            ProcessorSettings.ManualFanControlEnabled = false;
        }

        private void SetFanDutyCycle()
        {
            _asusHalService.SetCpuFanDutyCycle(
                _settingsService
                    .ControlCenter!
                    .Processor
                    .ManualFanDutyCycle
            );
        }
    }
}