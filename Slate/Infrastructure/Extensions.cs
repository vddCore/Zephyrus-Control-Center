using System;
using System.Collections.ObjectModel;
using System.Linq;
using LiveChartsCore.Defaults;
using Slate.Infrastructure.Asus;

namespace Slate.Infrastructure
{
    public static class Extensions
    {
        public static ObservableCollection<ObservablePoint> ToChartValues(this FanCurve curve, bool spaceOut = true)
        {
            if (spaceOut)
            {
                var copy = curve;

                for (var i = 0; i < copy.Points.Length - 1; i++)
                {
                    var a = copy.Points[i + 1].Temperature;
                    var b = copy.Points[i].Temperature;
                    var diff = a - b;

                    if (diff < 5)
                    {
                        for (var j = i; j >= 0; j--)
                        {
                            var newTemp = copy.Points[j].Temperature - (5 - diff);

                            if (newTemp < FanCurve.MinimumTemperature)
                                newTemp = FanCurve.MinimumTemperature + (j * 5);

                            copy.Points[j].Temperature = (byte)newTemp;
                        }
                    }
                }

                curve = copy;
            }

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

        public static string ToFourCharacterCode(this int value)
        {
            var bytes = BitConverter.GetBytes(value);
            return $"{(char)bytes[0]}{(char)bytes[1]}{(char)bytes[2]}{(char)bytes[3]}";
        }

        public static int ReverseBytes(this int value)
        {
            var bytes = BitConverter.GetBytes(value);
            return BitConverter.ToInt32(bytes.Reverse().ToArray());
        }
    }
}