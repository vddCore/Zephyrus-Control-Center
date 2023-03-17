using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using PropertyChanged;

namespace Slate.View.Page
{
    [DoNotNotify]
    public partial class PowerManagementPage : UserControl
    {
        public PowerManagementPage()
        {
            InitializeComponent();
        }

        private void BatteryChargeLimitSlider_PropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
        {
            if (sender is not RangeBase)
                return;

            if (e.Property == RangeBase.ValueProperty)
            {
                if (e.NewValue is double d)
                {
                    Classes.Set("LowBatteryLimit", d < 20);
                }
            }
        }

        private void TotalPlatformPptSlider_PropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
        {
            if (sender is not RangeBase)
                return;
            
            if (e.Property == RangeBase.ValueProperty)
            {
                if (e.NewValue is double d)
                {
                    Classes.Set("HighSystemPPT", d > 135);
                }
            }
        }

        private void ProcessorPptSlider_PropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
        {
            if (sender is not RangeBase)
                return;
            
            if (e.Property == RangeBase.ValueProperty)
            {
                if (e.NewValue is double d)
                {
                    Classes.Set("HighProcessorPPT", d > 75);
                }
            }
        }
    }
}