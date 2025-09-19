using System;
using Collections;
using ReadOnlySortedMapTimeCurve;

namespace TimeReadOnlySortedMap
{
    public static class DateTimeHelpers
    {
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
            if (ticks < 0) throw new ArgumentOutOfRangeException(nameof(ticks));
            return ticks / 10_000_000d;
        }

        public static double ToSeconds(this double ticks)
        {
            if (ticks < 0) throw new ArgumentOutOfRangeException(nameof(ticks));
            return ticks / 10_000_000d;
        }

        public static long GetStartOfDayInTicks(long ticks)
        {
            return new DateTime(ticks).Date.Ticks;
        }

        /// <summary>
        /// Находит и возвращает минимальное количество тиков в кривой.
        /// </summary>
        /// <param name="depthTicksCurve"></param>
        /// <returns>Минимальное количество тиков в кривой depthTicksCurve. Если depthTicksCurve не содержит элементов, 
        /// то возвращается long.MinValue.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static long GetMinTicksFromLocalTime(TicksFromByteArray depthTicksCurve)
        {
            if (depthTicksCurve is null)
                throw new ArgumentNullException(nameof(depthTicksCurve));
            if (depthTicksCurve.Count < 1)
                return 0;
            long firstTicks = depthTicksCurve[0].Value;
            long lastTicks = depthTicksCurve[depthTicksCurve.Count - 1].Value;
            return Math.Min(firstTicks, lastTicks);
        }

        public static long GetStartOfDayInTicks(TicksFromByteArray depthTicksCurve)
        {
            long minTicks = GetMinTicksFromLocalTime(depthTicksCurve);
            return GetStartOfDayInTicks(minTicks);
        }
    }
}
