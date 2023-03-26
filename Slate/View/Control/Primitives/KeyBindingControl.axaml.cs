using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Layout;
using PropertyChanged;
using Slate.Model;

namespace Slate.View.Control.Primitives
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

        public static readonly StyledProperty<object?> DefaultFunctionButtonContentProperty
            = AvaloniaProperty.Register<KeyBindingControl, object?>(nameof(DefaultFunctionButtonContent), "None");

        public static readonly StyledProperty<object?> MediaFunctionButtonContentProperty
            = AvaloniaProperty.Register<KeyBindingControl, object?>(nameof(MediaFunctionButtonContent), "Media");

        public static readonly StyledProperty<object?> CommandFunctionButtonContentProperty
            = AvaloniaProperty.Register<KeyBindingControl, object?>(nameof(CommandFunctionButtonContent), "Command");

        public static readonly StyledProperty<KeyBind> KeyBindProperty
            = AvaloniaProperty.Register<KeyBindingControl, KeyBind>(
                nameof(KeyBind),
                new KeyBind(KeyBindMode.Default)
            );
        
        public static readonly StyledProperty<string> CommandTextProperty
            = AvaloniaProperty.Register<KeyBindingControl, string>(nameof(CommandText));

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

        public object? DefaultFunctionButtonContent
        {
            get => GetValue(DefaultFunctionButtonContentProperty);
            set => SetValue(DefaultFunctionButtonContentProperty, value);
        }

        public object? MediaFunctionButtonContent
        {
            get => GetValue(MediaFunctionButtonContentProperty);
            set => SetValue(MediaFunctionButtonContentProperty, value);
        }

        public object? CommandFunctionButtonContent
        {
            get => GetValue(CommandFunctionButtonContentProperty);
            set => SetValue(CommandFunctionButtonContentProperty, value);
        }

        public KeyBind KeyBind
        {
            get => GetValue(KeyBindProperty);
            set => SetValue(KeyBindProperty, value);
        }
        
        public string CommandText
        {
            get => GetValue(CommandTextProperty);
            set => SetValue(CommandTextProperty, value);
        }

        public KeyBindingControl()
        {
            InitializeComponent();
        }

        protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
        {
            UpdateInternalState(KeyBind);
            CommandText = KeyBind.Command!;
            
            base.OnAttachedToVisualTree(e);
        }

        protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
        {
            if (change.Property == KeyBindProperty)
            {
                if (change.NewValue is KeyBind bind)
                {
                    UpdateInternalState(bind);
                }
            }
            else if (change.Property == CommandTextProperty)
            {
                if (change.NewValue is string str)
                {
                    KeyBind = KeyBind with { Command = str };
                }
            }

            base.OnPropertyChanged(change);
        }

        private void DefaultFunctionButton_Click(object? sender, RoutedEventArgs e) 
            => KeyBind = KeyBind with { Mode = KeyBindMode.Default };

        private void MediaFunctionButton_Click(object? sender, RoutedEventArgs e) 
            => KeyBind = KeyBind with { Mode = KeyBindMode.Media };

        private void CommandFunctionButton_Click(object? sender, RoutedEventArgs e) 
            => KeyBind = KeyBind with { Mode = KeyBindMode.Command };

        private void MediaRadioButtonPrevious_Click(object? sender, RoutedEventArgs e) 
            => KeyBind = KeyBind with { MediaKey = MediaKey.Previous };

        private void MediaRadioButtonStop_Click(object? sender, RoutedEventArgs e)
            => KeyBind = KeyBind with { MediaKey = MediaKey.Stop };
        
        private void MediaRadioButtonPlayPause_Click(object? sender, RoutedEventArgs e)
            => KeyBind = KeyBind with { MediaKey = MediaKey.PlayPause };
        
        private void MediaRadioButtonNext_OnClick(object? sender, RoutedEventArgs e)
            => KeyBind = KeyBind with { MediaKey = MediaKey.Next };

        private void UpdateInternalState(KeyBind keyBind)
        {
            UpdateButtonStatus(keyBind.MediaKey, keyBind.Mode);
            UpdateClasses(keyBind.Mode);
        }

        private void UpdateButtonStatus(MediaKey? mediaKey, KeyBindMode mode)
        {
            DefaultFunctionButton.IsChecked = mode == KeyBindMode.Default;
            MediaFunctionButton.IsChecked = mode == KeyBindMode.Media;
            CommandFunctionButton.IsChecked = mode == KeyBindMode.Command;

            MediaRadioButtonPrevious.IsChecked = mediaKey == MediaKey.Previous;
            MediaRadioButtonPlayPause.IsChecked = mediaKey == MediaKey.PlayPause;
            MediaRadioButtonStop.IsChecked = mediaKey == MediaKey.Stop;
            MediaRadioButtonNext.IsChecked = mediaKey == MediaKey.Next;
        }

        private void UpdateClasses(KeyBindMode mode)
        {
            Classes.Set("default-mode", mode == KeyBindMode.Default);
            Classes.Set("media-mode", mode == KeyBindMode.Media);
            Classes.Set("command-mode", mode == KeyBindMode.Command);
        }
    }
}