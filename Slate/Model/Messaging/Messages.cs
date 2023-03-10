using Avalonia.Controls;
using Glitonea.Mvvm.Messaging;
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
    
    public sealed record PerformancePresetChangedMessage(PerformancePreset Preset) 
        : Message;
    
    public sealed record SettingsModifiedMessage(
        SettingsComponent Owner,
        string? PropertyName
    ) : Message;
}