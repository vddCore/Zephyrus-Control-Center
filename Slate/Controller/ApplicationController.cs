using Glitonea.Mvvm.Messaging;
using Slate.Infrastructure.Asus;
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
        private readonly IPowerManagementService _powerManagementService;
        
        private ControlCenterSettings ControlCenterSettings => _settingsService.ControlCenter!;
        
        public ApplicationController(
            IAsusHalService asusHalService,
            ISettingsService settingsService,
            IPowerManagementService powerManagementService)
        {
            _asusHalService = asusHalService;
            _settingsService = settingsService;
            _powerManagementService = powerManagementService;

            if (!_asusHalService.IsAcpiSessionOpen)
                _asusHalService.OpenAcpiSession();
            
            SubscribeToApplicationSettings();
            SubscribeToProcessorSettings();
            
            Message.Subscribe<MainWindowLoadedMessage>(this, OnMainWindowLoaded);
        }

        private void ApplyInitialValues()
        {
            if (ProcessorSettings.FanCurve == null)
            {
                ProcessorSettings.FanCurve = _asusHalService.ReadBuiltInCpuFanCurve(
                    PerformancePreset.Balanced
                );
            }
            
            new CpuFanCurveUpdatedMessage(
                ProcessorSettings.FanCurve
            ).Broadcast();

            new CpuBoostModeChangedMessage(
                ProcessorSettings.IsBoostActiveOnAC,
                ProcessorSettings.IsBoostActiveOnDC
            );
        }
        
        private void OnMainWindowLoaded(MainWindowLoadedMessage _)
        {
            ApplyInitialValues();
        }
    }
}