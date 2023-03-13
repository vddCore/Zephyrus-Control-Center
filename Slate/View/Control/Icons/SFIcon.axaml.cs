using Avalonia;
using Avalonia.Controls;
using PropertyChanged;

namespace Slate.View.Control.Icons
{
    [DoNotNotify]
    public partial class SFIcon : UserControl
    {
        public static readonly StyledProperty<char> GlyphProperty
            = AvaloniaProperty.Register<Card, char>(nameof(Glyph));

        public char Glyph
        {
            get => GetValue(GlyphProperty);
            set => SetValue(GlyphProperty, value);
        }
        
        public SFIcon()
        {
            InitializeComponent();
        }
    }
}