using Glitonea.Mvvm;
using Slate.Infrastructure.Services;
using Slate.Model.Settings.Components;

namespace Slate.ViewModel.Page
{
    public class PowerManagementPageViewModel : ViewModelBase
    {
        private readonly ISettingsService _settingsService;
        
        public PowerManagementSettings PowerManagementSettings =>
            _settingsService.ControlCenter!.PowerManagement;

        public PowerManagementPageViewModel(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }
    }
}