using System;
using Collections;

namespace TimeReadOnlySortedMap
{
    public static class TestCurvesHelper
    {
        private const long ticksPerDay = 864_000_000_000L;
        private const long secondsPerDay = 86_400L;

        public static PieList<double, byte[]> GetLocalTimeWithIncreasingDateTime()
        {
            var result = new PieList<double, byte[]>();
            result.Insert(1000, DateTimeHelpers.ToByteArray(new DateTime((long)(ticksPerDay * 1.8))));
            result.Insert(1100, DateTimeHelpers.ToByteArray(new DateTime((long)(ticksPerDay * 1.9))));
            result.Insert(1200, DateTimeHelpers.ToByteArray(new DateTime((long)(ticksPerDay * 2))));
            result.Insert(1300, DateTimeHelpers.ToByteArray(new DateTime((long)(ticksPerDay * 2.1))));
            result.Insert(1400, DateTimeHelpers.ToByteArray(new DateTime((long)(ticksPerDay * 2.2))));

            return result;
        }

        public static PieList<double, byte[]> GetLocalTimeWithDecreasingDateTime()
        {
            var result = new PieList<double, byte[]>();
            result.Insert(1000, DateTimeHelpers.ToByteArray(new DateTime((long)(ticksPerDay * 2.2))));
            result.Insert(1100, DateTimeHelpers.ToByteArray(new DateTime((long)(ticksPerDay * 2.1))));
            result.Insert(1200, DateTimeHelpers.ToByteArray(new DateTime((long)(ticksPerDay * 2))));
            result.Insert(1300, DateTimeHelpers.ToByteArray(new DateTime((long)(ticksPerDay * 1.9))));
            result.Insert(1400, DateTimeHelpers.ToByteArray(new DateTime((long)(ticksPerDay * 1.8))));

            return result;
        }

        public static PieList<double, double> GetDepthValue()
        {
            var result = new PieList<double, double>();

            result.Insert(900, -2.5);
            result.Insert(1100, -1.5);
            result.Insert(1210, -0.5);
            result.Insert(1220, 0);
            result.Insert(1340, 0.5);
            result.Insert(1500, 1.5);

            return result;
        }

        public static PieList<double, double> GetSecondsValueWithIncreasingDateTimeFromLocalTimeMin()
        {
            var result = new PieList<double, double>();
            result.Insert(secondsPerDay * 0, -2.5);
            result.Insert(secondsPerDay * 0.1, -1.5);
            result.Insert(secondsPerDay * 0.21, -0.5);
            result.Insert(secondsPerDay * 0.22, 0);
            result.Insert(secondsPerDay * 0.34, 0.5);
            result.Insert(secondsPerDay * 0.4, 1.5);

            return result;
        }

        public static PieList<double, double> GetSecondsValueWithDecreasingDateTimeFromLocalTimeMin()
        {
            var result = new PieList<double, double>();
            result.Insert(secondsPerDay * 0.4, -2.5);
            result.Insert(secondsPerDay * 0.3, -1.5);
            result.Insert(secondsPerDay * 0.19, -0.5);
            result.Insert(secondsPerDay * 0.18, 0);
            result.Insert(secondsPerDay * 0.06, 0.5);
            result.Insert(secondsPerDay * 0, 1.5);

            return result;
        }

        public static PieList<double, double> GetSecondsValueWithIncreasingDateTimeFromStartOfDay()
        {
            var result = new PieList<double, double>();
            result.Insert(secondsPerDay * 0.8, -2.5);
            result.Insert(secondsPerDay * 0.9, -1.5);
            result.Insert(secondsPerDay * 1.01, -0.5);
            result.Insert(secondsPerDay * 1.02, 0);
            result.Insert(secondsPerDay * 1.14, 0.5);
            result.Insert(secondsPerDay * 1.2, 1.5);

            return result;
        }

        public static PieList<double, double> GetSecondsValueWithDecreasingDateTimeFromStartOfDay()
        {
            var result = new PieList<double, double>();
            result.Insert(secondsPerDay * 1.2, -2.5);
            result.Insert(secondsPerDay * 1.1, -1.5);
            result.Insert(secondsPerDay * 0.99, -0.5);
            result.Insert(secondsPerDay * 0.98, 0);
            result.Insert(secondsPerDay * 0.86, 0.5);
            result.Insert(secondsPerDay * 0.8, 1.5);

            return result;
        }
    }
}
