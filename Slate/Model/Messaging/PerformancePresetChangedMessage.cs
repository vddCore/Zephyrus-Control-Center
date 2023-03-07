using Glitonea.Mvvm.Messaging;

namespace Slate.Model.Messaging
{
    public class PerformancePresetChangedMessage : Message
    {
        public PerformancePreset Preset { get; }

        public PerformancePresetChangedMessage(PerformancePreset preset)
        {
            Preset = preset;
        }
    }
}