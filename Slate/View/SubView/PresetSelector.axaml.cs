using Avalonia.Controls;
using Glitonea.Mvvm.Messaging;
using PropertyChanged;
using Slate.Model;
using Slate.Model.Messaging;

namespace Slate.View.SubView
{
    [DoNotNotify]
    public partial class PresetSelector : UserControl
    {
        public PresetSelector()
        {
            InitializeComponent();
            
            Message.Subscribe<PerformancePresetChangedMessage>(this, OnPerformancePresetChanged);
        }

        private void OnPerformancePresetChanged(PerformancePresetChangedMessage msg)
        {
            switch (msg.Preset)
            {
                case PerformancePreset.Silent:
                    SilentPresetButton.Classes.Set("PresetSelected", true);
                    BalancedPresetButton.Classes.Set("PresetSelected", false);
                    PerformancePresetButton.Classes.Set("PresetSelected", false);
                    break;
                
                case PerformancePreset.Balanced:
                    SilentPresetButton.Classes.Set("PresetSelected", false);
                    BalancedPresetButton.Classes.Set("PresetSelected", true);
                    PerformancePresetButton.Classes.Set("PresetSelected", false);
                    break;
                
                case PerformancePreset.Performance:
                    SilentPresetButton.Classes.Set("PresetSelected", false);
                    BalancedPresetButton.Classes.Set("PresetSelected", false);
                    PerformancePresetButton.Classes.Set("PresetSelected", true);
                    break;
            }
        }
    }
}