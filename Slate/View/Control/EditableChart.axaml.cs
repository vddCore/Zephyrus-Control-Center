using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.Drawing;
using LiveChartsCore.Kernel;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Avalonia;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.Painting.Effects;
using PropertyChanged;
using SkiaSharp;
using Slate.Infrastructure.Asus;

namespace Slate.View.Control
{
    [DoNotNotify]
    public partial class EditableChart : UserControl
    {
        public static readonly StyledProperty<ISeries[]?> SeriesProperty
            = AvaloniaProperty.Register<EditableChart, ISeries[]?>(nameof(Series));

        public static readonly StyledProperty<Axis[]> XAxesProperty
            = AvaloniaProperty.Register<EditableChart, Axis[]>(nameof(XAxes), new Axis[]
            {
                new()
                {
                    TextSize = 12,
                    MinLimit = FanCurve.MinimumTemperature,
                    MaxLimit = FanCurve.MaximumTemperature,
                    MinStep = 10,
                    ForceStepToMin = true,
                    SeparatorsPaint = new SolidColorPaint(SKColors.DimGray) { StrokeThickness = 1 },
                    Padding = new(0,3,3,5)
                }
            });

        public static readonly StyledProperty<Axis[]> YAxesProperty
            = AvaloniaProperty.Register<EditableChart, Axis[]>(nameof(YAxes), new Axis[]
            {
                new()
                {
                    TextSize = 12,
                    MinLimit = 0,
                    MaxLimit = FanCurve.MaximumFanRPM,
                    MinStep = FanCurve.MaximumFanRPM / 5,
                    ForceStepToMin = true,
                    Labeler = (d) => { return d >= FanCurve.MinimumFanRPM || d == 0 ? d.ToString("F0") : ""; },
                    SeparatorsPaint = new SolidColorPaint(SKColors.DimGray) { StrokeThickness = 1 },
                }
            });

        public static readonly StyledProperty<RectangularSection[]> SectionsProperty
            = AvaloniaProperty.Register<EditableChart, RectangularSection[]>(nameof(Sections), new RectangularSection[]
            {
                new()
                {
                    Xi = FanCurve.MinimumTemperature - 10,
                    Xj = FanCurve.MaximumTemperature + 10,
                    Yi = -1000,
                    Yj = FanCurve.MinimumFanRPM,
                    Stroke = new SolidColorPaint(SKColors.White.WithAlpha(200))
                    {
                        StrokeThickness = 1,
                        PathEffect = new DashEffect(new float[] { 4, 4 }),
                        
                    },
                    Fill = new SolidColorPaint(new SKColor(0, 0, 0, 100))
                }
            });

        public static readonly StyledProperty<ICommand?> CurveModifiedCommandProperty
            = AvaloniaProperty.Register<EditableChart, ICommand?>(nameof(CurveModifiedCommand));

        private ChartPoint? _editedChartPoint;
        private ObservablePoint? _editedPoint;

        public ISeries[]? Series
        {
            get => GetValue(SeriesProperty);
            set => SetValue(SeriesProperty, value);
        }

        public Axis[] XAxes
        {
            get => GetValue(XAxesProperty);
            set => SetValue(XAxesProperty, value);
        }

        public Axis[] YAxes
        {
            get => GetValue(YAxesProperty);
            set => SetValue(YAxesProperty, value);
        }

        public RectangularSection[] Sections
        {
            get => GetValue(SectionsProperty);
            set => SetValue(SectionsProperty, value);
        }

        public ICommand? CurveModifiedCommand
        {
            get => GetValue(CurveModifiedCommandProperty);
            set => SetValue(CurveModifiedCommandProperty, value);
        }

        public EditableChart()
        {
            InitializeComponent();
        }

        private void Chart_OnPointerMoved(object? sender, PointerEventArgs e)
        {
            if (sender is not CartesianChart _)
                return;

            if (_editedPoint == null || _editedChartPoint == null)
                return;

            if (Series == null)
                return;

            if (Series[0].Values is not ObservableCollection<ObservablePoint> values)
                return;

            var pointIndex = values.IndexOf(_editedPoint);

            var p = e.GetPosition(CartesianChart);
            var dataCoordinates = CartesianChart.ScalePixelsToData(new LvcPointD(p.X, p.Y));

            var horizontalMoveLegal = true;

            if (pointIndex - 1 >= 0)
            {
                var prevPoint = values[pointIndex - 1];
                horizontalMoveLegal = prevPoint.X + 5 < dataCoordinates.X;
            }

            if (pointIndex + 1 < values.Count)
            {
                var nextPoint = values[pointIndex + 1];
                horizontalMoveLegal = horizontalMoveLegal && (nextPoint.X - 5 > dataCoordinates.X);
            }

            if (horizontalMoveLegal 
                && dataCoordinates.X >= FanCurve.MinimumTemperature
                && dataCoordinates.X <= FanCurve.MaximumTemperature)
            {
                _editedPoint.X = dataCoordinates.X;
            }

            if (dataCoordinates.Y <= FanCurve.MaximumFanRPM && dataCoordinates.Y > 0)
            {
                _editedPoint.Y = dataCoordinates.Y;
            }
        }

        private void Chart_OnPointerReleased(object? sender, PointerReleasedEventArgs _)
        {
            if (sender is not CartesianChart _)
                return;

            if (_editedPoint == null)
                return;

            if (Series == null)
                return;

            if (Series[0].Values is not ObservableCollection<ObservablePoint> points)
                return;

            var index = points.IndexOf(_editedPoint!);
            
            if (_editedPoint.Y < FanCurve.MinimumFanRPM)
                _editedPoint.Y = FanCurve.MinimumFanRPM - 1;

            for (var i = index; i < points.Count; i++)
            {
                if (points[i].Y < _editedPoint.Y)
                    points[i].Y = _editedPoint.Y;
            }

            for (var i = index; i >= 0; i--)
            {
                if (points[i].Y > _editedPoint.Y)
                    points[i].Y = _editedPoint.Y;
            }

            if (CurveModifiedCommand?.CanExecute(null) == true)
            {
                CurveModifiedCommand?.Execute(null);
            }

            _editedPoint = null;
            _editedChartPoint = null;
        }

        private void Chart_OnChartPointPointerDown(IChartView chartView, ChartPoint? point)
        {
            if (chartView is not CartesianChart _)
                return;

            if (point != null)
            {
                _editedChartPoint = point;
                _editedPoint = point.Context.Entity as ObservablePoint;
            }
        }
    }
}