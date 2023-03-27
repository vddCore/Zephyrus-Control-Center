using Glitonea.Mvvm;
using Slate.Infrastructure.Services;
using Slate.Model.Settings.Components;
using Starlight.Asus;

namespace Slate.ViewModel.Page
{
    public class AniMeMatrixPageViewModel : SingleInstanceViewModelBase
    {
        private readonly ISettingsService _settingsService;

        public AniMeMatrixSettings AniMeMatrixSettings => _settingsService.ControlCenter!.AniMeMatrix;

        public bool IsViewEnabled => Brightness != BrightnessLevel.Off;
        
        public BrightnessLevel Brightness
        {
            get => AniMeMatrixSettings.Brightness;
            
            set
            {
                AniMeMatrixSettings.Brightness = value;
                
                OnPropertyChanged(nameof(Brightness));
                OnPropertyChanged(nameof(IsViewEnabled));
            }
        }
        
        public AniMeMatrixPageViewModel(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }
    }
}