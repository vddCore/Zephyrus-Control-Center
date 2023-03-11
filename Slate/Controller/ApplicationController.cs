using Glitonea.Mvvm.Messaging;
using Slate.Infrastructure.Services;
using Slate.Model.Messaging;
using Slate.Model.Settings;

namespace Slate.Controller
{
    public partial class ApplicationController
    {
        private const string ApplicationName = "Zephyrus Control Center";

        private readonly IAsusHalService _asusHalService;
        private readonly ISettingsService _settingsService;

        private ControlCenterSettings ControlCenterSettings => _settingsService.ControlCenter!;
        
        public ApplicationController(
            IAsusHalService asusHalService,
            ISettingsService settingsService)
        {
            _asusHalService = asusHalService;
            _settingsService = settingsService;

            if (!_asusHalService.IsAcpiSessionOpen)
                _asusHalService.OpenAcpiSession();
            
            SubscribeToApplicationSettings();
            SubscribeToProcessorSettings();
            
            Message.Subscribe<MainWindowLoadedMessage>(this, OnMainWindowLoaded);
        }

        private void ApplyInitialValues()
        {
            new ManualCpuFanControlChangedMessage(
                ProcessorSettings.ManualFanControlEnabled,
                ProcessorSettings.ManualFanDutyCycle
            );
        }
        
        private void OnMainWindowLoaded(MainWindowLoadedMessage obj)
        {
            ApplyInitialValues();
        }
    }
}