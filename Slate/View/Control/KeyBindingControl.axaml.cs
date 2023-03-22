using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Interactivity;
using Avalonia.Layout;
using PropertyChanged;

namespace Slate.View.Control
{
    [DoNotNotify]
    public partial class KeyBindingControl : UserControl
    {
        public static readonly StyledProperty<object?> KeyIconContentProperty
            = AvaloniaProperty.Register<KeyBindingControl, object?>(nameof(KeyIconContent));

        public static readonly StyledProperty<string> KeyLabelProperty
            = AvaloniaProperty.Register<KeyBindingControl, string>(nameof(KeyLabel));

        public static readonly StyledProperty<VerticalAlignment> VerticalKeyLabelAlignmentProperty
            = AvaloniaProperty.Register<KeyBindingControl, VerticalAlignment>(
                nameof(VerticalKeyLabelAlignment),
                VerticalAlignment.Bottom
            );

        public static readonly StyledProperty<object?> PrimaryFunctionButtonContentProperty
            = AvaloniaProperty.Register<KeyBindingControl, object?>(nameof(PrimaryFunctionButtonContent), "None");

        public static readonly StyledProperty<object?> SecondaryFunctionButtonContentProperty
            = AvaloniaProperty.Register<KeyBindingControl, object?>(nameof(SecondaryFunctionButtonContent), "Media");
        
        public static readonly StyledProperty<object?> TertiaryFunctionButtonContentProperty
            = AvaloniaProperty.Register<KeyBindingControl, object?>(nameof(TertiaryFunctionButtonContent), "Command");
        
        public static readonly StyledProperty<KeyBindingMode> KeyBindingModeProperty
            = AvaloniaProperty.Register<KeyBindingControl, KeyBindingMode>(nameof(KeyBindingMode));

        public static readonly StyledProperty<object?> PanelContentProperty
            = AvaloniaProperty.Register<KeyBindingControl, object?>(nameof(PanelContent));

        public static readonly StyledProperty<DataTemplates> PanelContentTemplatesProperty
            = AvaloniaProperty.Register<KeyBindingControl, DataTemplates>(nameof(PanelContentTemplates));

        public object? KeyIconContent
        {
            get => GetValue(KeyIconContentProperty);
            set => SetValue(KeyIconContentProperty, value);
        }

        public string KeyLabel
        {
            get => GetValue(KeyLabelProperty);
            set => SetValue(KeyLabelProperty, value);
        }

        public VerticalAlignment VerticalKeyLabelAlignment
        {
            get => GetValue(VerticalKeyLabelAlignmentProperty);
            set => SetValue(VerticalKeyLabelAlignmentProperty, value);
        }
        
        public object? PrimaryFunctionButtonContent
        {
            get => GetValue(PrimaryFunctionButtonContentProperty);
            set => SetValue(PrimaryFunctionButtonContentProperty, value);
        }
        
        public object? SecondaryFunctionButtonContent
        {
            get => GetValue(SecondaryFunctionButtonContentProperty);
            set => SetValue(SecondaryFunctionButtonContentProperty, value);
        }
        
        public object? TertiaryFunctionButtonContent
        {
            get => GetValue(TertiaryFunctionButtonContentProperty);
            set => SetValue(TertiaryFunctionButtonContentProperty, value);
        }
        
        public KeyBindingMode KeyBindingMode
        {
            get => GetValue(KeyBindingModeProperty);
            set => SetValue(KeyBindingModeProperty, value);
        }

        public object? PanelContent
        {
            get => GetValue(PanelContentProperty);
            set => SetValue(PanelContentProperty, value);
        }

        public DataTemplates PanelContentTemplates
        {
            get => GetValue(PanelContentTemplatesProperty);
            set => SetValue(PanelContentTemplatesProperty, value);
        }

        public KeyBindingControl()
        {
            InitializeComponent();
        }

        protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
        {
            UpdateInternalState(KeyBindingMode);
            
            base.OnAttachedToVisualTree(e);
        }

        protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
        {
            if (change.Property == KeyBindingModeProperty)
            {
                if (change.NewValue is KeyBindingMode mode)
                {
                    UpdateInternalState(mode);
                }
            }
            
            base.OnPropertyChanged(change);
        }

        private void PrimaryFunctionButton_Click(object? sender, RoutedEventArgs e)
        {
            KeyBindingMode = KeyBindingMode.Primary;
        }

        private void SecondaryFunctionButton_Click(object? sender, RoutedEventArgs e)
        {
            KeyBindingMode = KeyBindingMode.Secondary;
        }

        private void TertiaryFunctionButton_Click(object? sender, RoutedEventArgs e)
        {
            KeyBindingMode = KeyBindingMode.Tertiary;
        }

        private void UpdateInternalState(KeyBindingMode mode)
        {
            UpdateButtonStatus(mode);
            UpdateClasses(mode);
        }
        
        private void UpdateButtonStatus(KeyBindingMode mode)
        {
            PrimaryFunctionButton.IsChecked = mode == KeyBindingMode.Primary;
            SecondaryFunctionButton.IsChecked = mode == KeyBindingMode.Secondary;
            TertiaryFunctionButton.IsChecked = mode == KeyBindingMode.Tertiary;
        }

        private void UpdateClasses(KeyBindingMode mode)
        {
            Classes.Set("primary-mode", mode == KeyBindingMode.Primary);
            Classes.Set("secondary-mode", mode == KeyBindingMode.Secondary);
            Classes.Set("tertiary-mode", mode == KeyBindingMode.Tertiary);
        }
    }
}