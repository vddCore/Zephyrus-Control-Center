using Avalonia;
using Avalonia.Controls;
using PropertyChanged;

namespace Slate.View.Control.Primitives
{
    [DoNotNotify]
    public partial class Card : UserControl
    {
        public static readonly StyledProperty<object?> HeaderContentProperty
            = AvaloniaProperty.Register<Card, object?>(nameof(HeaderContent));

        public static readonly StyledProperty<bool> ShowHeaderProperty
            = AvaloniaProperty.Register<Card, bool>(nameof(ShowHeader));

        public object? HeaderContent
        {
            get => GetValue(HeaderContentProperty);
            set => SetValue(HeaderContentProperty, value);
        }
        
        public bool ShowHeader
        {
            get => GetValue(ShowHeaderProperty);
            set => SetValue(ShowHeaderProperty, value);
        }
        
        public Card()
        {
            InitializeComponent();
        }
    }
}