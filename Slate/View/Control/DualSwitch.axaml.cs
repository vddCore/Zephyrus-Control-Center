using System;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using PropertyChanged;

namespace Slate.View.Control
{
    [DoNotNotify]
    public partial class DualSwitch : UserControl
    {
        public static readonly StyledProperty<object?> LeftOptionContentProperty
            = AvaloniaProperty.Register<DualSwitch, object?>(nameof(LeftOptionContent));

        public static readonly StyledProperty<ICommand?> LeftOptionCommandProperty
            = AvaloniaProperty.Register<DualSwitch, ICommand?>(nameof(LeftOptionCommand));

        public static readonly StyledProperty<bool> IsLeftOptionCheckedProperty
            = AvaloniaProperty.Register<DualSwitch, bool>(nameof(IsLeftOptionChecked));

        public static readonly StyledProperty<object?> RightOptionContentProperty
            = AvaloniaProperty.Register<DualSwitch, object?>(nameof(RightOptionContent));

        public static readonly StyledProperty<ICommand?> RightOptionCommandProperty
            = AvaloniaProperty.Register<DualSwitch, ICommand?>(nameof(RightOptionCommand));

        public static readonly StyledProperty<bool> IsRightOptionCheckedProperty
            = AvaloniaProperty.Register<DualSwitch, bool>(nameof(IsRightOptionChecked));


        public object? LeftOptionContent
        {
            get => GetValue(LeftOptionContentProperty);
            set => SetValue(LeftOptionContentProperty, value);
        }

        public ICommand? LeftOptionCommand
        {
            get => GetValue(LeftOptionCommandProperty);
            set => SetValue(LeftOptionCommandProperty, value);
        }

        public bool IsLeftOptionChecked
        {
            get => GetValue(IsLeftOptionCheckedProperty);
            set => SetValue(IsLeftOptionCheckedProperty, value);
        }

        public object? RightOptionContent
        {
            get => GetValue(RightOptionContentProperty);
            set => SetValue(RightOptionContentProperty, value);
        }

        public ICommand? RightOptionCommand
        {
            get => GetValue(RightOptionCommandProperty);
            set => SetValue(RightOptionCommandProperty, value);
        }

        public bool IsRightOptionChecked
        {
            get => GetValue(IsRightOptionCheckedProperty);
            set => SetValue(IsRightOptionCheckedProperty, value);
        }

        public DualSwitch()
        {
            InitializeComponent();
        }

        protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
        {
            if (change.Property == IsLeftOptionCheckedProperty)
            {
                Classes.Set("left-checked", IsLeftOptionChecked);
            }
            else if (change.Property == IsRightOptionCheckedProperty)
            {
                Classes.Set("right-checked", IsRightOptionChecked);
            }

            base.OnPropertyChanged(change);
        }
    }
}