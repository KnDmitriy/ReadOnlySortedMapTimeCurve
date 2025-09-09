using Collections;
using ReadOnlySortedMapTimeCurve;
using System;

namespace IReadOnlySortedMapTimeCurve
{
    public static class DateTimeHelpers
    {
        public static DateTime CreateFromByteArray(byte[] bytes)
        {
            if (bytes is null)
                throw new ArgumentNullException(nameof(bytes));

            if (bytes.Length != 8)
                throw new ArgumentException("Byte array is wrong");

            var ticks = BitConverter.ToInt64(bytes, 0);
            
            return new DateTime(ticks);
        }

        public static byte[] CreateFromDateTime(DateTime dateTime)
        {
            var ticks = dateTime.Ticks;

            return BitConverter.GetBytes(ticks);
        }

        public static DateTime GetDateTimeFromBeginningOfCurve(PieList<double, byte[]> curveLocalTime)
        {
            if (curveLocalTime is null)
                throw new ArgumentNullException(nameof(curveLocalTime));
            if (curveLocalTime.Count < 1)
                throw new ArgumentException("PieList mustn't be empty.");

            // В curveLocalTime объекты даты и времени упорядочены.
            // Значит минимальное значение даты и времени либо первое значение curveLocalTime, либо последнее.
            DateTime firstDateTime = CreateFromByteArray(curveLocalTime.First.Value);
            DateTime lastDateTime = CreateFromByteArray(curveLocalTime.Last.Value);
            DateTime minDateTime;
            // Возможно нужна проверка на то, находится ли время в одном часовом поясе (за это отвечает свойство Kind).
            // Хотя кривая называется "Местное время", значит, по идее, время уже указано в нужном часовом поясе. 
            if (firstDateTime.CompareTo(lastDateTime) > 0)
            {
                minDateTime = lastDateTime;
            }
            else
            {
                minDateTime = firstDateTime;
            }
            return minDateTime;
        }

        public static double GetTimeInSecondsFromBeginningOfDay(DateTime dateTime)
        {
            throw new NotImplementedException();
        }
        public static double GetLinearInterpolation(IReadOnlySortedMap<double, byte[]> localTime, double depth, int indexOfNextItem)
        {
            if (depth < 0)
                throw new ArgumentOutOfRangeException("The depth can't be less than zero.");

            DateTime dateTime0 = CreateFromByteArray(localTime[indexOfNextItem - 1].Value);
            double seconds0 = dateTime0.Second;  // заменить!!!
            DateTime dateTime1 = CreateFromByteArray(localTime[indexOfNextItem].Value);
            double seconds1 = dateTime1.Second;  // заменить!!!
            if (seconds0 < 0 || seconds1 < 0)
                throw new ArgumentOutOfRangeException("Time can't be negative.");
            double depth0 = localTime[indexOfNextItem - 1].Key;
            double depth1 = localTime[indexOfNextItem].Key;

            double interpolatedSeconds = seconds0 + (depth - depth0) * (seconds1 - seconds0) / (depth1 - depth0);
            return interpolatedSeconds;
        }


        //public static DateTime GetDateTimeFromBeginningOfCurve(IReadOnlySortedMap<double, byte[]> curveDepthTicks)
        //{
        //    return DateTimeHelpers.CreateFromByteArray(curveDepthTicks.);
        //}

    }
}
