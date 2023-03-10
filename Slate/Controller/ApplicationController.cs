using Slate.Infrastructure.Services;

namespace Slate.Controller
{
    public partial class ApplicationController
    {
        private const string ApplicationName = "Zephyrus Control Center";

        private readonly IAsusHalService _asusHalService;
        private readonly ISettingsService _settingsService;
        
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
        }
    }
}