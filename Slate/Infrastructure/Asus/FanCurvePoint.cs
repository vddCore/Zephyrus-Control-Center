using System;

namespace Slate.Infrastructure.Asus
{
    public struct FanCurvePoint
    {
        /**
         * ASUS' firmware does not calculate anything.
         * Instead, we have this neat lookup table.
         *
         * I guess.
         *
         * Nothing makes sense about this fucking software.
         * 
         * *sobs*
         **/
        private static readonly int[] _rpmLookupForward = new[]
        {
            0000, 1800, 1850, 1900, 1950, 2000,
            2050, 2100, 2150, 2175, 2200, 2250,
            2300, 2350, 2375, 2400, 2450, 2500,
            2550, 2575, 2600, 2650, 2700, 2750,
            2800, 2850, 2900, 2950, 2975, 3000,
            3050, 3100, 3150, 3200, 3250, 3300,
            3350, 3400, 3450, 3500, 3550, 3575,
            3600, 3650, 3700, 3750, 3800, 3850,
            3875, 3900, 3950, 4000, 4050, 4100,
            4150, 4175, 4200, 4250, 4275, 4300,
            4350, 4400, 4450, 4500, 4550, 4575,
            4600, 4650, 4700, 4750, 4800, 4850,
            4875, 4900, 4950, 5000, 5050, 5075,
            5100, 5150, 5200, 5250, 5300, 5350,
            5400, 5450, 5475, 5500, 5550, 5600,
            5650, 5675, 5700, 5750, 5800, 5850,
            5900, 5950, 5975, 6000, 6050, 6100,
            6150, 6200, 6250, 6300, 6350, 6375,
            6400, 6450, 6500
        };

        /// <summary>
        /// Represents X axis on the fan curve.
        /// </summary>
        public byte Temperature { get; }

        /// <summary>
        /// Represents Y axis on the fan curve (RPM LUT index).
        /// </summary>
        public byte LookupTableIndex { get; }

        /// <summary>
        /// Actual RPM based on the lookup table index.
        /// </summary>
        public int RPM => _rpmLookupForward[LookupTableIndex];

        public FanCurvePoint(byte temperature, byte lookupTableIndex)
        {
            Temperature = temperature;
            LookupTableIndex = (byte)Math.Min(_rpmLookupForward.Length, lookupTableIndex);
        }

        public static (byte, int) Approximate(int rpm)
        {
            if (rpm < 1800)
                return (0, 0);
            
            var result = _rpmLookupForward.Nearest(rpm);
            return ((byte)result.IndexOf, result.Value);
        }
    }
}