using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using PropertyChanged;

namespace Slate.View.Page
{
    [DoNotNotify]
    public partial class PowerManagementPage : UserControl
    {
        private const int LowBatteryLimitValue = 20;
        private const int HighSystemPptValue = 135;
        private const int HighProcessorPptValue = 85;

        public PowerManagementPage()
        {
            InitializeComponent();

            Classes.Set("LowBatteryLimit", BatteryChargeLimitSlider.Value <= LowBatteryLimitValue);
            Classes.Set("HighSystemPPT", TotalSystemPptSlider.Value >= HighSystemPptValue);
            Classes.Set("HighProcessorPPT", ProcessorPptSlider.Value >= HighProcessorPptValue);
        }

        private void BatteryChargeLimitSlider_PropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
        {
            if (sender is not RangeBase)
                return;

            if (e.Property == RangeBase.ValueProperty)
            {
                if (e.NewValue is double d)
                {
                    Classes.Set("LowBatteryLimit", d <= LowBatteryLimitValue);
                }
            }
        }

        private void TotalSystemPptSlider_PropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
        {
            if (sender is not RangeBase slider)
                return;

            if (e.Property == RangeBase.ValueProperty)
            {
                Classes.Set("HighSystemPPT", slider.Value >= HighSystemPptValue);
            }
        }

        private void ProcessorPptSlider_PropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
        {
            if (sender is not RangeBase slider)
                return;

            if (e.Property == RangeBase.ValueProperty)
            {
                Classes.Set("HighProcessorPPT", slider.Value >= HighProcessorPptValue);
            }
        }
    }
}