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

            /**
             * We read these before subscribing to settings-broadcast
             * messages to avoid causing system instability. And maybe
             * improve reporting reliability. Who knows.
             **/
            ReadVolatileSettingsFromFirmware();

            SubscribeToApplicationSettings();
            SubscribeToFansSettings();
            SubscribeToGraphicsAndDisplaySettings();

            Message.Subscribe<MainWindowLoadedMessage>(this, OnMainWindowLoaded);
        }

        private void ReadVolatileSettingsFromFirmware()
        {
            GraphicsAndDisplaySettings.MuxSwitchMode = _asusHalService.GetGraphicsMode();
            GraphicsAndDisplaySettings.IsEcoModeEnabled = _asusHalService.GetEcoMode();
            GraphicsAndDisplaySettings.IsDisplayOverdriveEnabled = _asusHalService.GetDisplayOverdrive();
        }

        private void ApplyInitialValues()
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

            new CpuFanCurveUpdatedMessage(FansSettings.CpuFanCurve)
                .Broadcast();

            new GpuFanCurveUpdatedMessage(FansSettings.GpuFanCurve)
                .Broadcast();

            new CpuBoostModeChangedMessage(
                PowerManagementSettings.IsProcessorBoostActiveOnAC,
                PowerManagementSettings.IsProcessorBoostActiveOnAC
            );
        }

        private void OnMainWindowLoaded(MainWindowLoadedMessage _)
        {
            ApplyInitialValues();
        }
    }
}