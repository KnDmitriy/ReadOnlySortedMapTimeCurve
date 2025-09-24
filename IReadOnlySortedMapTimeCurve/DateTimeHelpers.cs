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
            return BitConverter.GetBytes(dateTime.Ticks);
        }

        public static double ToSeconds(this long ticks)
        {
            if (ticks < 0)
                throw new ArgumentOutOfRangeException(nameof(ticks));
            
            return ticks / (double)ticksPerSecond;
        }

        public static double ToSeconds(this double ticks)
        {
            if (ticks < 0)
                throw new ArgumentOutOfRangeException(nameof(ticks));
            
            return ticks / ticksPerSecond;
        }
       
        public static long GetStartOfDayFromTicks(ByteArrayWrapper depthTicks)
        {
            return GetStartOfDay(GetMinTicks(depthTicks));
        }

         /// <summary>
        /// Находит и возвращает минимальное количество тиков в кривой.
        /// </summary>
        /// <param name="depthTicks"></param>
        /// <returns>Минимальное количество тиков в кривой depthTicks</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static long GetMinTicks(ByteArrayWrapper depthTicks)
        {
            if (depthTicks is null)
                throw new ArgumentNullException(nameof(depthTicks));

            if (depthTicks.Count < 1)
                return 0;

            long firstTicks = depthTicks[0].Value;
            long lastTicks = depthTicks[depthTicks.Count - 1].Value;
            return Math.Min(firstTicks, lastTicks);
        }

        public static long GetStartOfDay(long timeInTicks)
        {
            return new DateTime(timeInTicks).Date.Ticks;
        }                       
    }
}
