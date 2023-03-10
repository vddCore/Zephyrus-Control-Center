using Glitonea.Mvvm;
using Glitonea.Mvvm.Messaging;
using Slate.Infrastructure.Asus;
using Slate.Infrastructure.Services;
using Slate.Model;
using Slate.Model.Messaging;

namespace Slate.ViewModel.SubView
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
        }

        private void OnMainWindowLoaded(MainWindowLoadedMessage obj)
        {
            ActivatePreset(_settingsService.ControlCenter!.SelectedPreset);
        }

        public void ActivatePreset(object? parameter)
        {
            var preset = parameter as PerformancePreset?;

            if (preset == null)
                return;

            _settingsService.ControlCenter!.SelectedPreset = preset.Value;
            _asusHalService.SetPerformancePreset(preset.Value);

            new PerformancePresetChangedMessage(preset.Value)
                .Broadcast();
        }
    }
}