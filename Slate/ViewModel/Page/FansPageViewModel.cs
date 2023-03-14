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
using PropertyChanged;
using SkiaSharp;
using Slate.Infrastructure;
using Slate.Infrastructure.Asus;
using Slate.Infrastructure.Services;
using Slate.Model.Messaging;
using Slate.Model.Settings.Components;

namespace Slate.ViewModel.Page
{
    public class FansPageViewModel : ViewModelBase
    {
        private readonly ISettingsService _settingsService;
        private readonly IAsusHalService _asusHalService;
        
        public IHardwareMonitorService HardwareMonitor { get; set; }
        public FansSettings FansSettings => _settingsService.ControlCenter!.Fans;

        public ISeries[] CpuSeries { get; set; } = new ISeries[]
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
        
        public ISeries[] GpuSeries { get; set; } = new ISeries[]
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
        
        public FansPageViewModel(
            ISettingsService settingsService,
            IAsusHalService asusHalService,
            IHardwareMonitorService hardwareMonitor)
        {
            _settingsService = settingsService;
            _asusHalService = asusHalService;
            HardwareMonitor = hardwareMonitor;

            var primaryColor = AvaloniaLocator
                .Current
                .GetRequiredService<IPlatformSettings>()
                .GetColorValues()
                .AccentColor1
                .ToSKColor();

            InitializeCpuSeries(primaryColor);
            InitializeGpuSeries(primaryColor);
            
            Message.Subscribe<SystemAccentColorChangedMessage>(this, OnSystemAccentColorChanged);
        }

        public void HandleCpuCurveModification()
        {
            var cpuSeries = (LineSeries<ObservablePoint>)CpuSeries[0];
            var values = (ObservableCollection<ObservablePoint>)cpuSeries.Values!;
            FansSettings.CpuFanCurve = values.ToFanCurve();
        }
        
        public void HandleGpuCurveModification()
        {
            var gpuSeries = (LineSeries<ObservablePoint>)GpuSeries[0];
            var values = (ObservableCollection<ObservablePoint>)gpuSeries.Values!;

            FansSettings.GpuFanCurve = values.ToFanCurve();
        }
        
        public void ActivatePreset(object? parameter)
        {
            var preset = (PerformancePreset)parameter!;

            SetCpuSeriesToPreset(preset);
            SetGpuSeriesToPreset(preset);
            
            FansSettings.PerformancePreset = preset;
        }

        private void InitializeCpuSeries(SKColor curveColor)
        {
            var cpuSeries = (LineSeries<ObservablePoint>)CpuSeries[0];
            cpuSeries.Values = FansSettings.CpuFanCurve!.ToChartValues();
            cpuSeries.GeometryStroke = new SolidColorPaint(curveColor, 2);
            cpuSeries.Stroke = new SolidColorPaint(curveColor, 2);
        }
        
        private void SetCpuSeriesToPreset(PerformancePreset preset)
        {
            var cpuCurve = _asusHalService.ReadBuiltInCpuFanCurve(preset);
            var cpuSeries = (LineSeries<ObservablePoint>)CpuSeries[0];
            var oldCpuValues = (ObservableCollection<ObservablePoint>)cpuSeries.Values!;
            var newCpuValues = cpuCurve.ToChartValues();
            
            FansSettings.CpuFanCurve = newCpuValues.ToFanCurve();
            
            for (var i = 0; i < 8; i++)
                oldCpuValues[i] = newCpuValues[i];
        }
        
        private void InitializeGpuSeries(SKColor curveColor)
        {
            var gpuSeries = (LineSeries<ObservablePoint>)GpuSeries[0];
            gpuSeries.Values = FansSettings.GpuFanCurve!.ToChartValues();
            gpuSeries.GeometryStroke = new SolidColorPaint(curveColor, 2);
            gpuSeries.Stroke = new SolidColorPaint(curveColor, 2);
        }
        
        private void SetGpuSeriesToPreset(PerformancePreset preset)
        {
            var gpuCurve = _asusHalService.ReadBuiltInGpuFanCurve(preset);
            var gpuSeries = (LineSeries<ObservablePoint>)GpuSeries[0];
            var oldGpuValues = (ObservableCollection<ObservablePoint>)gpuSeries.Values!;
            var newGpuValues = gpuCurve.ToChartValues();
            
            FansSettings.GpuFanCurve = newGpuValues.ToFanCurve();
            
            for (var i = 0; i < 8; i++)
                oldGpuValues[i] = newGpuValues[i];
        }
        
        [SuppressPropertyChangedWarnings]
        private void OnSystemAccentColorChanged(SystemAccentColorChangedMessage msg)
        {
            var newColor = msg.PrimaryAccentColor.ToSKColor();
            
            SetLineSeriesColor((LineSeries<ObservablePoint>)CpuSeries[0], newColor);
            SetLineSeriesColor((LineSeries<ObservablePoint>)GpuSeries[0], newColor);
        }

        private void SetLineSeriesColor(LineSeries<ObservablePoint> series, SKColor color)
        {
            series.GeometryStroke = new SolidColorPaint(color, 2);
            series.Stroke = new SolidColorPaint(color, 2);
        }
    }
}