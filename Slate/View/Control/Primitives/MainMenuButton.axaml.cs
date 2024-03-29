using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using PropertyChanged;

namespace Slate.View.Control.Primitives
{
    [DoNotNotify]
    public partial class MainMenuButton : UserControl
    {
        public static readonly StyledProperty<ICommand?> CommandProperty =
            AvaloniaProperty.Register<MainMenuButton, ICommand?>(nameof(Command));
            
        public static readonly StyledProperty<object?> CommandParameterProperty =
            AvaloniaProperty.Register<MainMenuButton, object?>(nameof(CommandParameter));
        
        public static readonly StyledProperty<object?> ImageContentProperty =
            AvaloniaProperty.Register<MainMenuButton, object?>(nameof(ImageContent));
        
        public static readonly StyledProperty<string?> HeaderProperty =
            AvaloniaProperty.Register<MainMenuButton, string?>(nameof(Header));
        
        public static readonly StyledProperty<string?> DescriptionProperty =
            AvaloniaProperty.Register<MainMenuButton, string?>(nameof(Description));
        
        public static readonly StyledProperty<object?> GadgetContentProperty =
            AvaloniaProperty.Register<MainMenuButton, object?>(nameof(GadgetContent));

        public ICommand? Command
        {
            get => GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }
        
        public object? CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }
        
        public object? ImageContent
        {
            get => GetValue(ImageContentProperty);
            set => SetValue(ImageContentProperty, value);
        }
        
        public string? Header
        {
            get => GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }
        
        public string? Description
        {
            get => GetValue(DescriptionProperty);
            set => SetValue(DescriptionProperty, value);
        }

        public object? GadgetContent
        {
            get => GetValue(GadgetContentProperty);
            set => SetValue(GadgetContentProperty, value);
        }
        
        public MainMenuButton()
        {
            InitializeComponent();
        }
    }
}