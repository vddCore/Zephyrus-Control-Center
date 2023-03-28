using System.Management;
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
        private readonly IDisplayManagementService _displayManagementService;
        private readonly IAsusAuraService _asusAuraService;
        private readonly IInputInjectionService _inputInjectionService;
        private readonly IAsusAnimeMatrixService _asusAnimeMatrixService;

        private ControlCenterSettings ControlCenterSettings => _settingsService.ControlCenter!;

        public ApplicationController(
            IAsusHalService asusHalService,
            ISettingsService settingsService,
            IPowerManagementService powerManagementService,
            IDisplayManagementService displayManagementService,
            IAsusAuraService asusAuraService,
            IInputInjectionService inputInjectionService,
            IAsusAnimeMatrixService asusAnimeMatrixService)
        {
            _asusHalService = asusHalService;
            _settingsService = settingsService;
            _powerManagementService = powerManagementService;
            _displayManagementService = displayManagementService;
            _asusAuraService = asusAuraService;
            _inputInjectionService = inputInjectionService;
            _asusAnimeMatrixService = asusAnimeMatrixService;

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
            SubscribeToKeyboardSettings();
            SubscribeToAsusEventProvider();
            SubscribeToAniMeMatrixSettings();

            Message.Subscribe<MainWindowLoadedMessage>(this, OnMainWindowLoaded);
        }

        private void SubscribeToAsusEventProvider()
        {
            _asusHalService.SubscribeToWmiEvent(OnAsusWmiEventReceived);
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

            new ManualFanOverrideUpdatedMessage(
                FansSettings.IsManualOverrideEnabled,
                FansSettings.ManualCpuFanDutyCycle,
                FansSettings.ManualGpuFanDutyCycle
            ).Broadcast();

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

            new AuraSettingsChangedMessage(
                KeyboardSettings.Animation,
                KeyboardSettings.PrimaryColor.HardwareColor,
                KeyboardSettings.SecondaryColor.HardwareColor,
                KeyboardSettings.AnimationSpeed
            ).Broadcast();

            new AniMeMatrixBrightnessChangedMessage(
                AniMeMatrixSettings.Brightness
            );
        }

        private void OnAsusWmiEventReceived(ManagementBaseObject managementObject)
        {
            // Debug.WriteLine("WMI event {");
            // Debug.WriteLine("  Properties: {");
            // foreach (var prop in managementObject.Properties)
            // {
            //     Debug.WriteLine($"    {prop.Name}: {prop.Value}");
            // }
            // Debug.WriteLine("  }");
            // Debug.WriteLine("}");
            //
            if (int.TryParse(managementObject.Properties["EventID"].Value.ToString(), out var eventId))
            {
                var ev = (AtkWmiEventID)eventId;
                
                switch (ev)
                {
                    case AtkWmiEventID.KeyPressM3:
                    case AtkWmiEventID.KeyPressM4:
                    case AtkWmiEventID.KeyPressFnF4:
                    case AtkWmiEventID.KeyPressFnF5:
                        HandleSpecialKeyPress(ev);
                        break;
                }
            }
        }

        private void OnMainWindowLoaded(MainWindowLoadedMessage _)
        {
            ApplyInitialValues();
        }
    }
}