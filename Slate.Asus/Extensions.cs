using System;
using System.Linq;

namespace Slate.Asus
{
    internal static class Extensions
    {
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

        public static int ToFourCharacterCodeInteger(this string fourcc)
        {
            if (fourcc.Length != 4)
            {
                throw new ArgumentException(
                    "FOUR character code has FOUR characters. What do you not understand?",
                    nameof(fourcc)
                );
            }

            var bytes = new[]
            {
                (byte)fourcc[0],
                (byte)fourcc[1],
                (byte)fourcc[2],
                (byte)fourcc[3]
            };

            return BitConverter.ToInt32(bytes);
        }

        public static int ReverseBytes(this int value)
        {
            var bytes = BitConverter.GetBytes(value);
            return BitConverter.ToInt32(bytes.Reverse().ToArray());
        }
    }
}