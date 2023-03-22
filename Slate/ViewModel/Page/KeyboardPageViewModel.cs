using System;
using System.Collections.Generic;
using System.Diagnostics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Skia;
using Glitonea.Mvvm;
using Glitonea.Mvvm.Messaging;
using Slate.Infrastructure.Services;
using Slate.Model.Messaging;
using Slate.Model.Settings.Components;
using Slate.View.Control;
using Slate.View.Page;
using Starlight.Asus.Aura;

namespace Slate.ViewModel.Page
{
    public class KeyboardPageViewModel : SingleInstanceViewModelBase
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

        public KeyMacroViewMarker KeyMacroViewMarkerM3 { get; set; } = KeyMacroViewMarkers.Empty;
        public bool IsM3MuteChecked => KeyMacroViewMarkerM3 == KeyMacroViewMarkers.Empty;
        public bool IsM3MediaChecked => KeyMacroViewMarkerM3 == KeyMacroViewMarkers.Media;
        public bool IsM3CommandChecked => KeyMacroViewMarkerM3 == KeyMacroViewMarkers.Command;
        public double BorderHeightM3 => KeyMacroViewMarkerM3 == KeyMacroViewMarkers.Empty ? 0 : 32;

        public CornerRadius CornerRadiusM3_1 => KeyMacroViewMarkerM3 == KeyMacroViewMarkers.Empty
            ? new(4, 0, 0, 4)
            : new(4, 0, 0, 0);

        public CornerRadius CornerRadiusM3_2 => KeyMacroViewMarkerM3 == KeyMacroViewMarkers.Empty
            ? new(0, 6, 6, 0)
            : new(0, 6, 0, 0);

        public KeyboardPageViewModel(ISettingsService settingsService)
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

        public void SwitchM3MacroKeyView(object? parameter)
        {
            if (parameter is not KeyMacroViewMarker marker)
                return;

            KeyMacroViewMarkerM3 = marker;

            OnPropertyChanged(nameof(BorderHeightM3));
            OnPropertyChanged(nameof(CornerRadiusM3_1));
            OnPropertyChanged(nameof(CornerRadiusM3_2));
            OnPropertyChanged(nameof(IsM3MuteChecked));
            OnPropertyChanged(nameof(IsM3MediaChecked));
            OnPropertyChanged(nameof(IsM3CommandChecked));
        }

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