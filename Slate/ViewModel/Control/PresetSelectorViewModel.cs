using Glitonea.Mvvm;
using Glitonea.Mvvm.Messaging;
using Slate.Infrastructure.Asus;
using Slate.Infrastructure.Services;
using Slate.Model.Messaging;

namespace Slate.ViewModel.Control
{
    public class PresetSelectorViewModel : ViewModelBase
    {
        private readonly IAsusHalService _asusHalService;
        private readonly ISettingsService _settingsService;
        
        public PresetSelectorViewModel(
            IAsusHalService asusHalService,
            ISettingsService settingsService)
        {
            _asusHalService = asusHalService;
            _settingsService = settingsService;
            
            Message.Subscribe<MainWindowLoadedMessage>(this, OnMainWindowLoaded);
            Message.Subscribe<PerformancePresetChangedMessage>(this, OnPerformancePresetChanged);
        }

        private void OnMainWindowLoaded(MainWindowLoadedMessage obj)
        {
            if (_settingsService.ControlCenter!.Processor.ManualFanControlEnabled)
                return;
            
            ActivatePreset(_settingsService.ControlCenter!.SelectedPreset);
        }

        public void ActivatePreset(object? parameter)
        {
            var preset = parameter as PerformancePreset?;

            if (preset == null)
                return;

            new PerformancePresetChangedMessage(preset.Value)
                .Broadcast();
        }
        
        private void OnPerformancePresetChanged(PerformancePresetChangedMessage msg)
        {
            _settingsService.ControlCenter!.SelectedPreset = msg.Preset;
            _asusHalService.SetPerformancePreset(msg.Preset);
        }
    }
}