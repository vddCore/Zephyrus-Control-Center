﻿using System;
using System.Text;

namespace Slate.Infrastructure.Asus
{
    public class FanCurve
    {
        public const int MaximumFanRPM = 6500;
        public const int MinimumFanRPM = 1800;
        public const int MinimumTemperature = 20;
        public const int MaximumTemperature = 110;
        
        public byte[] RawData { get; } = new byte[16];
        public FanCurvePoint[] Points { get; } = new FanCurvePoint[8];

        public FanCurve(byte[] rawData)
        {
            if (rawData.Length != 16)
            {
                throw new ArgumentException(
                    $"Expected a 16-byte array. The provided array is {rawData.Length} long.",
                    nameof(rawData)
                );
            }

            RawData = rawData;
            
            for (var i = 0; i < 8; i++)
            {
                Points[i] = new FanCurvePoint(
                    RawData[i], 
                    RawData[i + 8]
                );
            }
        }

        public FanCurve(FanCurvePoint[] points)
        {
            if (points.Length != 8)
            {
                throw new ArgumentException(
                    $"Expected an 8-point fan curve. The provided array has {points.Length} of them.",
                    nameof(points)
                );
            }
            
            Points = points;
            
            for (var i = 0; i < 8; i++)
            {
                RawData[i] = Points[i].Temperature;
                RawData[i + 8] = Points[i].LookupTableIndex;
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach (var b in RawData)
            {
                sb.Append($"{b:X2} ");
            }
            
            sb.AppendLine();
            
            foreach (var b in RawData)
            {
                sb.Append($"{b:D3} ");
            }

            return sb.ToString();
        }
    }
}