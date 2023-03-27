using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using PropertyChanged;
using Starlight.Asus;

namespace Slate.View.Control.Primitives
{
    [DoNotNotify]
    public partial class BrightnessSelector : UserControl
    {
        public static readonly StyledProperty<BrightnessLevel> BrightnessLevelProperty
            = AvaloniaProperty.Register<Card, BrightnessLevel>(nameof(BrightnessLevel));

        public BrightnessLevel BrightnessLevel
        {
            get => GetValue(BrightnessLevelProperty);
            set => SetValue(BrightnessLevelProperty, value);
        }

        public BrightnessSelector()
        {
            InitializeComponent();
        }

        protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
        {
            UpdateButtonStates(BrightnessLevel);

            base.OnAttachedToVisualTree(e);
        }

        protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
        {
            if (change.Property == BrightnessLevelProperty)
            {
                UpdateButtonStates(BrightnessLevel);
            }

            base.OnPropertyChanged(change);
        }

        private void UpdateButtonStates(BrightnessLevel level)
        {
            OffButton.IsChecked = level == BrightnessLevel.Off;
            DimButton.IsChecked = level == BrightnessLevel.Dim;
            MediumButton.IsChecked = level == BrightnessLevel.Medium;
            BrightButton.IsChecked = level == BrightnessLevel.Bright;
        }

        private void OffButton_Click(object? sender, RoutedEventArgs e)
            => BrightnessLevel = BrightnessLevel.Off;

        private void DimButton_Click(object? sender, RoutedEventArgs e)
            => BrightnessLevel = BrightnessLevel.Dim;

        private void MediumButton_Click(object? sender, RoutedEventArgs e)
            => BrightnessLevel = BrightnessLevel.Medium;

        private void BrightButton_Click(object? sender, RoutedEventArgs e)
            => BrightnessLevel = BrightnessLevel.Bright;
    }
}