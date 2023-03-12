using System;
using System.Text.Json.Serialization;

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
        [JsonIgnore]
        private static readonly int[] _rpmLookupForward = new[]
        {
            1799, 1800, 1800, 1900, 1900, 2000,
            2000, 2100, 2100, 2100, 2200, 2200,
            2300, 2300, 2300, 2400, 2400, 2400,
            2500, 2500, 2600, 2600, 2700, 2700,
            2800, 2800, 2900, 2900, 2900, 3000,
            3000, 3100, 3100, 3200, 3200, 3300,
            3300, 3400, 3400, 3500, 3500, 3500,
            3600, 3600, 3700, 3700, 3800, 3800,
            3800, 3900, 3900, 4000, 4000, 4100,
            4100, 4100, 4200, 4200, 4200, 4300,
            4300, 4400, 4400, 4500, 4500, 4500,
            4600, 4600, 4700, 4700, 4800, 4800,
            4800, 4900, 4900, 5000, 5000, 5000,
            5100, 5100, 5200, 5200, 5300, 5300,
            5400, 5400, 5400, 5500, 5500, 5600,
            5600, 5600, 5700, 5700, 5800, 5800,
            5900, 5900, 5900, 6000, 6000, 6100,
            6100, 6200, 6200, 6300, 6300, 6300,
            6400, 6400, 6500 
        };

        /// <summary>
        /// Represents X axis on the fan curve.
        /// </summary>
        public byte Temperature { get; set; }

        /// <summary>
        /// Represents Y axis on the fan curve (RPM LUT index).
        /// </summary>
        public byte LookupTableIndex { get; set; }

        /// <summary>
        /// Actual RPM based on the lookup table index.
        /// </summary>
        [JsonIgnore]
        public int RPM => _rpmLookupForward[LookupTableIndex];

        [JsonConstructor]
        public FanCurvePoint()
        {
        }
        
        public FanCurvePoint(byte temperature, byte lookupTableIndex)
        {
            Temperature = temperature;
            LookupTableIndex = (byte)Math.Min(_rpmLookupForward.Length, lookupTableIndex);
        }

        public static (byte, int) Approximate(int rpm)
        {
            if (rpm < 1800)
                return (0, 849);
            
            var result = _rpmLookupForward.Nearest(rpm);
            return ((byte)result.IndexOf, result.Value);
        }
    }
}