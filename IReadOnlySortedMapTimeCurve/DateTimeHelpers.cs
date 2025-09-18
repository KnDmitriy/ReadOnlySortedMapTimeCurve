using System;
using Collections;
using ReadOnlySortedMapTimeCurve;

namespace IReadOnlySortedMapTimeCurve
{
    public static class DateTimeHelpers
    {
        private const long ticksPerSecond = 10_000_000;
        private const double secondsPerTick = 1e-7;

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

        //public static DateTime GetMinTicksFromDepthTicks(IReadOnlySortedMap<double, byte[]> curveLocalTime)
        //{
        //    if (curveLocalTime is null)
        //        throw new ArgumentNullException(nameof(curveLocalTime));
        //    if (curveLocalTime.Count < 1)
        //        throw new ArgumentException("PieList mustn't be empty.");

        //    // В curveLocalTime объекты даты и времени упорядочены.
        //    // Значит минимальное значение даты и времени либо первое значение curveLocalTime, либо последнее.
        //    DateTime firstDateTime = ToDateTime(curveLocalTime[0].Value);
        //    DateTime lastDateTime = ToDateTime(curveLocalTime[curveLocalTime.Count - 1].Value);
        //    DateTime minDateTime;
        //    // Возможно нужна проверка на то, находится ли время в одном часовом поясе (за это отвечает свойство Kind).
        //    // Хотя кривая называется "Местное время", значит, по идее, время уже указано в нужном часовом поясе. 
        //    if (firstDateTime.CompareTo(lastDateTime) > 0)
        //    {
        //        minDateTime = lastDateTime;
        //    }
        //    else
        //    {
        //        minDateTime = firstDateTime;
        //    }
        //    return minDateTime;
        //}

        //public static double GetTicksFromStartOfCurve(IReadOnlySortedMap<double, byte[]> curveLocalTime)
        //{
        //    DateTime minDateTime = GetMinTicksFromDepthTicks(curveLocalTime);
        //    return GetSecondsFromDateTime(minDateTime); 
        //}

        public static double GetTimeInSecondsFromStartOfDay(DateTime dateTime)
        {
            TimeSpan time = dateTime.TimeOfDay;
            return time.TotalSeconds;
        }

        public static long GetTimeInTicksFromStartOfDay(long ticks)
        {
            TimeSpan time = new DateTime(ticks).TimeOfDay;
            return time.Ticks;
        }

        public static double GetSecondsFromDateTime(DateTime minDateTime)
        {
            return minDateTime.Ticks * secondsPerTick;
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
                return long.MinValue;
            long firstTicks = depthTicksCurve[0].Value;
            long lastTicks = depthTicksCurve[depthTicksCurve.Count - 1].Value;
            return Math.Min(firstTicks, lastTicks);
        }

        public static long GetMinTicksFromStartOfDay(TicksFromByteArray depthTicksCurve)
        {
            long minTicks = GetMinTicksFromLocalTime(depthTicksCurve);
            return GetTimeInTicksFromStartOfDay(minTicks);
        }





        //public static DateTime GetDateTimeFromStartOfCurve(IReadOnlySortedMap<double, byte[]> curveDepthTicks)
        //{
        //    return DateTimeHelpers.CreateFromByteArray(curveDepthTicks.);
        //}

    }
}
