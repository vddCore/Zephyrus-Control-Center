using Avalonia.Controls;
using Glitonea.Mvvm.Messaging;
using Slate.Infrastructure.Asus;
using Slate.Infrastructure.Settings;

namespace Slate.Model.Messaging
{
    public sealed record MainWindowLoadedMessage 
        : Message;

    public sealed record TrayIconClickedMessage
        : Message;
    
    public sealed record GlobalTickMessage(ulong TotalTicksElapsed)
        : Message;
    
    public sealed record MainWindowTransitionFinishedMessage(bool WasSlidingIn) 
        : Message;
    
    public sealed record PageSwitchedMessage(UserControl? Page) 
        : Message;
    
    public sealed record SettingsModifiedMessage(
        SettingsComponent Origin,
        string? PropertyName
    ) : Message;
    
    public sealed record PerformancePresetChangedMessage(PerformancePreset Preset) 
        : Message;

    public sealed record StartupLaunchChangedMessage(bool Enabled)
        : Message;

    public sealed record UpdateCheckChangedMessage(bool Enabled)
        : Message;

    public sealed record ManualCpuFanControlChangedMessage(bool Enabled, byte DutyCycle)
        : Message;

    public sealed record CpuFanCurveUpdatedMessage(byte[] FanCurveData);
}