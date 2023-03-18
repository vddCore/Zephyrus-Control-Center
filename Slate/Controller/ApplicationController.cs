using Glitonea.Mvvm.Messaging;
using Slate.Infrastructure;
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
        private readonly IDisplayManagementService _displayManagementService;

        private ControlCenterSettings ControlCenterSettings => _settingsService.ControlCenter!;

        public ApplicationController(
            IAsusHalService asusHalService,
            ISettingsService settingsService,
            IPowerManagementService powerManagementService,
            IDisplayManagementService displayManagementService)
        {
            _asusHalService = asusHalService;
            _settingsService = settingsService;
            _powerManagementService = powerManagementService;
            _displayManagementService = displayManagementService;
            
            if (!_asusHalService.IsAcpiSessionOpen)
                _asusHalService.OpenAcpiSession();

            /**
             * We read these before subscribing to settings-broadcast
             * messages to avoid causing system instability. And maybe
             * improve reporting reliability. Who knows.
             **/
            ReadCurrentHardwareSettings();

            SubscribeToApplicationSettings();
            SubscribeToFansSettings();
            SubscribeToGraphicsAndDisplaySettings();
            SubscribeToPowerManagementSettings();
            
            Message.Subscribe<MainWindowLoadedMessage>(this, OnMainWindowLoaded);
        }

        private void ReadCurrentHardwareSettings()
        {
            if (FansSettings.CpuFanCurve == null)
            {
                FansSettings.CpuFanCurve = _asusHalService.ReadBuiltInCpuFanCurve(
                    PerformancePreset.Balanced
                );
            }

            if (FansSettings.GpuFanCurve == null)
            {
                FansSettings.GpuFanCurve = _asusHalService.ReadBuiltInGpuFanCurve(
                    PerformancePreset.Balanced
                );
            }
            
            GraphicsAndDisplaySettings.MuxSwitchMode = _asusHalService.GetGraphicsMode();
            GraphicsAndDisplaySettings.IsEcoModeEnabled = _asusHalService.GetEcoMode();
            GraphicsAndDisplaySettings.IsDisplayOverdriveEnabled = _asusHalService.GetDisplayOverdrive();
            GraphicsAndDisplaySettings.DisplayRefreshRate = _displayManagementService.GetInternalDisplayRefreshRate();
        }

        private void ApplyInitialValues()
        {
            new CpuFanCurveUpdatedMessage(FansSettings.CpuFanCurve!)
                .Broadcast();

            new GpuFanCurveUpdatedMessage(FansSettings.GpuFanCurve!)
                .Broadcast();

            new CpuBoostModeChangedMessage(
                PowerManagementSettings.IsProcessorBoostActiveOnAC,
                PowerManagementSettings.IsProcessorBoostActiveOnAC
            ).Broadcast();

            new BatteryChargeLimitChangedMessage(
                PowerManagementSettings.BatteryChargeLimit
            ).Broadcast();

            new PowerTargetsChangedMessage(
                PowerManagementSettings.TotalSystemPPT,
                PowerManagementSettings.ProcessorPPT
            ).Broadcast();
        }

        private void OnMainWindowLoaded(MainWindowLoadedMessage _)
        {
            ApplyInitialValues();
        }
    }
}