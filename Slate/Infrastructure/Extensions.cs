using System;
using System.Collections.ObjectModel;
using System.Linq;
using LiveChartsCore.Defaults;
using Slate.Infrastructure.Asus;

namespace Slate.Infrastructure
{
    public static class Extensions
    {
        public static ObservableCollection<ObservablePoint> ToChartValues(this FanCurve curve)
        {
            return new ObservableCollection<ObservablePoint>(
                curve.Points.Select(
                    x => new ObservablePoint(
                        x.Temperature,
                        x.RPM
                    )
                )
            );
        }

        public static FanCurve ToFanCurve(this ObservableCollection<ObservablePoint> values)
        {
            var points = new FanCurvePoint[8];

            for (var i = 0; i < 8; i++)
            {
                var x = (byte)(values[i].X!);
                var (y, _) = FanCurvePoint.Approximate((int)values[i].Y!);

                points[i] = new FanCurvePoint(x, y);
            }

            return new FanCurve(points);
        }

        public static (int IndexOf, int Value) Nearest(this int[] array, int value)
        {
            var bestIndex = -1;
            var bestValue = int.MaxValue;
            var minDiff = int.MaxValue;

            for (var i = 0; i < array.Length; i++)
            {
                var diff = Math.Abs((long)array[i] - value);

                if (minDiff > diff)
                {
                    minDiff = (int)diff;
                    bestValue = array[i];
                    bestIndex = i;
                }
            }

            return (bestIndex, bestValue);
        }
    }
}