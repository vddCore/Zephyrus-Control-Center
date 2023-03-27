using Avalonia.Media;
using Glitonea.Mvvm.Messaging;
using Slate.Infrastructure.Asus;
using Slate.Infrastructure.Settings;
using Slate.View;
using Starlight.Asus;
using Starlight.Asus.Aura;

namespace Slate.Model.Messaging
{
    public sealed record GlobalTickMessage(ulong TotalTicksElapsed)
        : Message;

    public sealed record TrayIconClickedMessage
        : Message;

    public sealed record MainWindowLoadedMessage
        : Message;

    public sealed record MainWindowTransitionFinishedMessage(bool WasSlidingIn)
        : Message;

    public sealed record PageSwitchedMessage(PageMarker? PageMarker)
        : Message;

    public sealed record SettingsModifiedMessage(
        SettingsComponent Origin,
        string? PropertyName
    ) : Message;

    public sealed record StartupLaunchChangedMessage(bool Enabled)
        : Message;

    public sealed record UpdateCheckChangedMessage(bool Enabled)
        : Message;

    public sealed record CpuFanCurveUpdatedMessage(FanCurve Curve)
        : Message;
    
    public sealed record GpuFanCurveUpdatedMessage(FanCurve Curve)
        : Message;

    public sealed record PerformancePresetUpdatedMessage(PerformancePreset Preset)
        : Message;

    public sealed record ManualFanOverrideUpdatedMessage(
        bool IsOverrideEnabled,
        byte CpuDutyCycle,
        byte GpuDutyCycle
    ) : Message;

    public sealed record MuxSwitchModeChangedMessage(MuxSwitchMode MuxSwitchMode)
        : Message;

    public sealed record EcoModeChangedMessage(bool Enabled)
        : Message;

    public sealed record EcoModeTransitionStartedMessage
        : Message;

    public sealed record EcoModeTransitionFinishedMessage
        : Message;

    public sealed record DisplayOverdriveChangedMessage(bool Enabled)
        : Message;

    public sealed record DisplayRefreshRateChangedMessage(uint RefreshRate)
        : Message;
    
    public sealed record CpuBoostModeChangedMessage(
        bool EnableOnAC,
        bool EnableOnDC
    ) : Message;
    
    public sealed record BatteryChargeLimitChangedMessage(int Value) 
        : Message;

    public sealed record PowerTargetsChangedMessage(
        byte TotalSystemPPT,
        byte CpuPPT
    ) : Message;

    public sealed record AuraSettingsChangedMessage(
        AuraAnimation Animation,
        System.Drawing.Color PrimaryColor,
        System.Drawing.Color SecondaryColor,
        AuraAnimationSpeed AnimationSpeed
    ) : Message;

    public sealed record AuraFollowSystemAccentColorStateChangedMessage(
        bool Enabled
    ) : Message;

    public sealed record AniMeMatrixBrightnessChangedMessage(
        BrightnessLevel BrightnessLevel
    ) : Message;

    public sealed record SystemAccentColorChangedMessage(
        Color PrimaryAccentColor,
        Color SecondaryAccentColor,
        Color TertiaryAccentColor
    ) : Message;
}