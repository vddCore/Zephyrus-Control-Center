using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using PropertyChanged;

namespace Slate.View.Control.Primitives
{
    [DoNotNotify]
    public partial class GifRadioButton : UserControl
    {
        public static readonly StyledProperty<Uri> GifSourceUriProperty
            = AvaloniaProperty.Register<GifRadioButton, Uri>(nameof(GifSourceUri));

        public static readonly StyledProperty<double> GifWidthProperty
            = AvaloniaProperty.Register<GifRadioButton, double>(nameof(GifWidth));

        public static readonly StyledProperty<double> GifHeightProperty
            = AvaloniaProperty.Register<GifRadioButton, double>(nameof(GifHeight));

        public Uri GifSourceUri
        {
            get => GetValue(GifSourceUriProperty);
            set => SetValue(GifSourceUriProperty, value);
        }

        public double GifWidth
        {
            get => GetValue(GifWidthProperty);
            set => SetValue(GifWidthProperty, value);
        }
        
        public double GifHeight
        {
            get => GetValue(GifHeightProperty);
            set => SetValue(GifHeightProperty, value);
        }
        
        public GifRadioButton()
        {
            InitializeComponent();
        }

        protected override void OnPointerEntered(PointerEventArgs e)
        {
            GifImageContainer.Start();
            
            base.OnPointerEntered(e);
        }

        protected override void OnPointerExited(PointerEventArgs e)
        {
            GifImageContainer.Stop();
            
            base.OnPointerExited(e);
        }
    }
}