using Glitonea.Mvvm;
using Slate.Infrastructure.Services;
using Slate.Model.Settings.Components;

namespace Slate.ViewModel.Page
{
    public class KeyboardPageViewModel : SingleInstanceViewModelBase
    {
        private ISettingsService _settingsService;

        public KeyboardSettings KeyboardSettings => _settingsService.ControlCenter!.Keyboard;

        public KeyboardPageViewModel(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }
    }
}