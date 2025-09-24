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
       
        public static long GetStartOfDayFromTicks(ByteArrayWrapper ticksByDepthMap)
        {
            return GetStartOfDay(GetMinTicks(ticksByDepthMap));
        }

         /// <summary>
        /// Находит и возвращает минимальное количество тиков в кривой.
        /// </summary>
        /// <param name="ticksByDepthMap"></param>
        /// <returns>Минимальное количество тиков в кривой depthTicks</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static long GetMinTicks(ByteArrayWrapper ticksByDepthMap)
        {
            if (ticksByDepthMap is null)
                throw new ArgumentNullException(nameof(ticksByDepthMap));

            if (ticksByDepthMap.Count < 1)
                return 0;

            long firstTicks = ticksByDepthMap[0].Value;
            long lastTicks = ticksByDepthMap[ticksByDepthMap.Count - 1].Value;
            return Math.Min(firstTicks, lastTicks);
        }

        public static long GetStartOfDay(long timeInTicks)
        {
            return new DateTime(timeInTicks).Date.Ticks;
        }                       
    }
}
