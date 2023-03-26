using System;
using Avalonia;
using Avalonia.Media;
using Avalonia.Platform;
using Glitonea.Mvvm;
using Glitonea.Mvvm.Messaging;
using PropertyChanged;
using Slate.Infrastructure.Services;
using Slate.Model.Messaging;
using Slate.Model.Settings.Components;
using Starlight.Asus.Aura;

namespace Slate.ViewModel.Control
{
    public class AuraColorControlViewModel : SingleInstanceViewModelBase
    {
        private ISettingsService _settingsService;

        public KeyboardSettings KeyboardSettings => _settingsService.ControlCenter!.Keyboard;

        public HsvColor PrimaryColor
        {
            get => new(new Color(
                255,
                KeyboardSettings.PrimaryColor.R,
                KeyboardSettings.PrimaryColor.G,
                KeyboardSettings.PrimaryColor.B
            ));

            set
            {
                var hsv = new HsvColor(
                    1, value.H, Math.Max(0.006, value.S), 1
                );

                var color = hsv.ToRgb();

                KeyboardSettings.PrimaryColor = new(System.Drawing.Color.FromArgb(
                    255, color.R, color.G, color.B
                ));

                OnPropertyChanged(nameof(PrimaryColor));
            }
        }

        public HsvColor SecondaryColor
        {
            get => new(new Color(
                255,
                KeyboardSettings.SecondaryColor.R,
                KeyboardSettings.SecondaryColor.G,
                KeyboardSettings.SecondaryColor.B
            ));

            set
            {
                var hsv = new HsvColor(
                    1, value.H, Math.Max(0.006, value.S), 1
                );

                var color = hsv.ToRgb();

                KeyboardSettings.SecondaryColor = new(System.Drawing.Color.FromArgb(
                    255, color.R, color.G, color.B
                ));

                OnPropertyChanged(nameof(SecondaryColor));
            }
        }

        public bool FollowSystemAccentColor
        {
            get => KeyboardSettings.FollowSystemAccentColor;
            set
            {
                KeyboardSettings.FollowSystemAccentColor = value;

                if (value)
                {
                    var color = AvaloniaLocator
                        .Current
                        .GetRequiredService<IPlatformSettings>()
                        .GetColorValues()
                        .AccentColor1;

                    SetAuraToStaticColor(color);
                }
            }
        }

        public bool AnimationSupportsPrimaryColor => KeyboardSettings.Animation == AuraAnimation.Breathe
                                                     || KeyboardSettings.Animation == AuraAnimation.Pulse
                                                     || KeyboardSettings.Animation == AuraAnimation.Static;

        public bool AnimationSupportsSecondaryColor => KeyboardSettings.Animation == AuraAnimation.Breathe;

        public bool IsStaticChecked => KeyboardSettings.Animation == AuraAnimation.Static;
        public bool IsBreatheChecked => KeyboardSettings.Animation == AuraAnimation.Breathe;
        public bool IsPulseChecked => KeyboardSettings.Animation == AuraAnimation.Pulse;
        public bool IsRainbowChecked => KeyboardSettings.Animation == AuraAnimation.Rainbow;

        public AuraColorControlViewModel(ISettingsService settingsService)
        {
            _settingsService = settingsService;
            
            Message.Subscribe<SystemAccentColorChangedMessage>(this, OnSystemAccentColorChanged);
        }
        
        public void SwitchAnimation(object? parameter)
        {
            if (parameter is not AuraAnimation auraAnimation)
                return;

            KeyboardSettings.Animation = auraAnimation;

            OnPropertyChanged(nameof(IsStaticChecked));
            OnPropertyChanged(nameof(IsBreatheChecked));
            OnPropertyChanged(nameof(IsPulseChecked));
            OnPropertyChanged(nameof(IsRainbowChecked));
            OnPropertyChanged(nameof(AnimationSupportsPrimaryColor));
            OnPropertyChanged(nameof(AnimationSupportsSecondaryColor));
        }

        [SuppressPropertyChangedWarnings]
        private void OnSystemAccentColorChanged(SystemAccentColorChangedMessage msg)
        {
            if (KeyboardSettings.FollowSystemAccentColor)
            {
                SetAuraToStaticColor(msg.PrimaryAccentColor);
            }
        }

        private void SetAuraToStaticColor(Color color)
        {
            SwitchAnimation(AuraAnimation.Static);
            PrimaryColor = new(color);
            SecondaryColor = new(color);
        }
    }
}