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

        public byte TotalSystemPPT
        {
            get => PowerManagementSettings.TotalSystemPPT;
            set
            {
                if (ProcessorPPT > value)
                    ProcessorPPT = value;

                PowerManagementSettings.TotalSystemPPT = value;
                OnPropertyChanged(nameof(TotalSystemPPT));
            }
        }

        public byte ProcessorPPT
        {
            get => PowerManagementSettings.ProcessorPPT;
            set
            {
                if (value > PowerManagementSettings.TotalSystemPPT)
                    value = PowerManagementSettings.TotalSystemPPT;

                PowerManagementSettings.ProcessorPPT = value;
                OnPropertyChanged(nameof(ProcessorPPT));
            }
        }

        public PowerManagementPageViewModel(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }
    }
}