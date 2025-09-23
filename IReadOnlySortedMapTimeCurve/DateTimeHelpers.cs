using System;
using Collections;

namespace TimeReadOnlySortedMap
{
    public static class DateTimeHelpers
    {
        private const long ticksPerSecond = 10_000_000L;
        public static DateTime ToDateTime(this byte[] bytes)
        {
            if (bytes is null)
                throw new ArgumentNullException(nameof(bytes));

            if (bytes.Length != 8)
                throw new ArgumentException("The byte array has incorrect size.");

            var ticks = BitConverter.ToInt64(bytes, 0);            
            return new DateTime(ticks);
        }

        public static byte[] ToByteArray(this DateTime dateTime)
        {
            var ticks = dateTime.Ticks;
            return BitConverter.GetBytes(ticks);
        }

        public static double ToSeconds(this long ticks)
        {
            if (ticks < 0)
                throw new ArgumentOutOfRangeException(nameof(ticks));
            
            return ticks / (double) ticksPerSecond;
        }

        public static double ToSeconds(this double ticks)
        {
            if (ticks < 0)
                throw new ArgumentOutOfRangeException(nameof(ticks));
            
            return ticks / ticksPerSecond;
        }
       
        public static long GetStartOfDayInTicks(MapWithValuesTypeConverter<byte[], long> depthTicks)
        {
            long minTicks = GetMinTicksFromLocalTime(depthTicks);
            return GetStartOfDayInTicks(minTicks);
        }

         /// <summary>
        /// Находит и возвращает минимальное количество тиков в кривой.
        /// </summary>
        /// <param name="depthTicks"></param>
        /// <returns>Минимальное количество тиков в кривой depthTicks. Если depthTicks не содержит элементов, 
        /// то возвращается long.MinValue.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static long GetMinTicksFromLocalTime(MapWithValuesTypeConverter<byte[], long> depthTicks)
        {
            if (depthTicks is null)
                throw new ArgumentNullException(nameof(depthTicks));

            if (depthTicks.Count < 1)
                return 0;

            long firstTicks = depthTicks[0].Value;
            long lastTicks = depthTicks[depthTicks.Count - 1].Value;
            return Math.Min(firstTicks, lastTicks);
        }

        public static long GetStartOfDayInTicks(long ticks)
        {
            return new DateTime(ticks).Date.Ticks;
        }                       
    }
}
