using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Platform;
using Avalonia.Skia;
using Glitonea.Mvvm;
using Glitonea.Mvvm.Messaging;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.Drawing;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using Slate.Infrastructure;
using Slate.Infrastructure.Asus;
using Slate.Infrastructure.Services;
using Slate.Model.Messaging;
using Slate.Model.Settings.Components;

namespace Slate.ViewModel.Page
{
    public class ProcessorPageViewModel : ViewModelBase
    {
        private readonly ISettingsService _settingsService;
        private readonly IAsusHalService _asusHalService;

        public ISeries[] Series { get; set; } = new ISeries[]
        {
            new LineSeries<ObservablePoint>
            {
                Fill = null,
                LineSmoothness = 0.275,
                GeometryStroke = new SolidColorPaint(SKColors.DarkGray, 2),
                Stroke = new SolidColorPaint(SKColors.DarkGray, 2),
                GeometrySize = 8,
                DataPadding = new LvcPoint(5,5)
            }
        };

        public int FanSpeed => _asusHalService.ReadCpuFanSpeed();
        
        public ProcessorSettings ProcessorSettings => _settingsService.ControlCenter!.Processor;
        
        public ProcessorPageViewModel(
            ISettingsService settingsService,
            IAsusHalService asusHalService)
        {
            _settingsService = settingsService;
            _asusHalService = asusHalService;

            var primaryColor = AvaloniaLocator
                .Current
                .GetRequiredService<IPlatformSettings>()
                .GetColorValues()
                .AccentColor1
                .ToSKColor();

            if (ProcessorSettings.FanCurve == null)
            {
                ProcessorSettings.FanCurve = _asusHalService.ReadBuiltInCpuFanCurve(
                    PerformancePreset.Balanced
                );
            }
            
            var series = (LineSeries<ObservablePoint>)Series[0];
            series.Values = ProcessorSettings.FanCurve.ToChartValues();

            series.GeometryStroke = new SolidColorPaint(primaryColor, 2);
            series.Stroke = new SolidColorPaint(primaryColor, 2);
            
            Message.Subscribe<SystemAccentColorChangedMessage>(this, OnSystemAccentColorChanged);
            Message.Subscribe<GlobalTickMessage>(this, OnGlobalTick);
        }

        public void HandleCurveModification()
        {
            var series = (LineSeries<ObservablePoint>)Series[0];
            var values = (ObservableCollection<ObservablePoint>)series.Values!;

            ProcessorSettings.FanCurve = values.ToFanCurve();
        }
        
        public void ActivatePreset(object? parameter)
        {
            var preset = (PerformancePreset)parameter!;
            var curve = _asusHalService.ReadBuiltInCpuFanCurve(preset);

            ProcessorSettings.FanCurve = curve;
            
            var series = (LineSeries<ObservablePoint>)Series[0];
            var oldValues = (ObservableCollection<ObservablePoint>)series.Values!;
            var newValues = ProcessorSettings.FanCurve.ToChartValues();

            for (var i = 0; i < 8; i++)
                 oldValues[i] = newValues[i];
        }

        private void OnSystemAccentColorChanged(SystemAccentColorChangedMessage msg)
        {
            var series = (LineSeries<ObservablePoint>)Series[0];
            
            series.GeometryStroke = new SolidColorPaint(msg.PrimaryAccentColor.ToSKColor(), 2);
            series.Stroke = new SolidColorPaint(msg.PrimaryAccentColor.ToSKColor(), 2);
        }
        
        private void OnGlobalTick(GlobalTickMessage obj)
        {
            OnPropertyChanged(nameof(FanSpeed));
        }
    }
}