using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Slate.Model
{
    public record ColorWrapper  : INotifyPropertyChanged
    {
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }

        [JsonIgnore]
        public System.Drawing.Color HardwareColor
        {
            get => System.Drawing.Color.FromArgb(R, G, B);
            set
            {
                R = value.R;
                G = value.G;
                B = value.B;
            }
        }

        [JsonIgnore]
        public Avalonia.Media.Color MediaRGB
        {
            get => Avalonia.Media.Color.FromRgb(R, G, B);
            set
            {
                R = value.R;
                G = value.G;
                B = value.B;
            }
        }

        [JsonIgnore]
        public Avalonia.Media.HsvColor MediaHSV
        {
            get => new(MediaRGB);
            set => MediaRGB = value.ToRgb();
        }
        
        public event PropertyChangedEventHandler? PropertyChanged;

        public ColorWrapper()
        {
        }

        public ColorWrapper(System.Drawing.Color color)
        {
            HardwareColor = color;
        }

        public ColorWrapper(Avalonia.Media.Color color)
        {
            MediaRGB = color;
        }

        public ColorWrapper(byte r, byte g, byte b)
            : this()
        {
            R = r;
            G = g;
            B = b;
        }

    }
}