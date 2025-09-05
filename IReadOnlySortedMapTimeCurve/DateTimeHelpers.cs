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
            throw new NotImplementedException();
        }

        public static double GetTimeInSecondsFromBeginningOfDay(DateTime dateTime)
        {
            return 0;

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

        public static double GetLinearInterpolation(double depth, double depth0, double depth1, double seconds0, double seconds1)
        {
            return GetLinearInterpolation(depth, new LocalTimeItem(depth0, seconds0), new LocalTimeItem(depth1, seconds1));
        }

        public static double GetLinearInterpolation(double depth, LocalTimeItem begin, LocalTimeItem end)
        {
            if (depth < 0)
                throw new ArgumentOutOfRangeException("The depth can't be less than zero.");
            if (begin.Time < 0 || end.Time < 0)
                throw new ArgumentOutOfRangeException("Time can't be negative.");
            return begin.Time + (depth - begin.Depth) * (end.Time - begin.Time) / (end.Depth - begin.Depth);
        }


        //public static DateTime GetDateTimeFromBeginningOfCurve(IReadOnlySortedMap<double, byte[]> curveDepthTicks)
        //{
        //    return DateTimeHelpers.CreateFromByteArray(curveDepthTicks.);
        //}

    }
}
