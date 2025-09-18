using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Collections;

namespace IReadOnlySortedMapTimeCurve
{
    public static class TestCurvesHelper
    {
        private const long ticksPerSecond = 10_000_000L;
        private const long ticksPerDay = 864_000_000_000L;


        //public static PieList<double, byte[]> GetLocalTimeCurveWithIncreasingDateTime()
        //{
        //    var result = new PieList<double, byte[]>();

        //    result.Insert(1000, DateTimeHelpers.ToByteArray(new DateTime(10 * ticksPerSecond)));                       
        //    result.Insert(1100, DateTimeHelpers.ToByteArray(new DateTime(20 * ticksPerSecond)));
        //    result.Insert(1200, DateTimeHelpers.ToByteArray(new DateTime(30 * ticksPerSecond)));
        //    result.Insert(1300, DateTimeHelpers.ToByteArray(new DateTime(40 * ticksPerSecond)));
        //    result.Insert(1400, DateTimeHelpers.ToByteArray(new DateTime(50 * ticksPerSecond)));

        //    return result;
        //}

        public static PieList<double, byte[]> GetLocalTimeCurveWithDecreasingDateTime()
        {
            var result = new PieList<double, byte[]>();

            result.Insert(1000, DateTimeHelpers.ToByteArray(new DateTime(50 * ticksPerSecond)));
            result.Insert(1100, DateTimeHelpers.ToByteArray(new DateTime(40 * ticksPerSecond)));
            result.Insert(1200, DateTimeHelpers.ToByteArray(new DateTime(30 * ticksPerSecond)));
            result.Insert(1300, DateTimeHelpers.ToByteArray(new DateTime(20 * ticksPerSecond)));
            result.Insert(1400, DateTimeHelpers.ToByteArray(new DateTime(10 * ticksPerSecond)));

            return result;
        }

        public static PieList<double, byte[]> GetLocalTimeCurveWithIncreasingDateTime()
        {
            var result = new PieList<double, byte[]>();
            result.Insert(1000, DateTimeHelpers.ToByteArray(new DateTime((long)(ticksPerDay * 1.8))));
            result.Insert(1100, DateTimeHelpers.ToByteArray(new DateTime((long)(ticksPerDay * 1.9))));
            result.Insert(1200, DateTimeHelpers.ToByteArray(new DateTime((long)(ticksPerDay * 2))));
            result.Insert(1300, DateTimeHelpers.ToByteArray(new DateTime((long)(ticksPerDay * 2.1))));
            result.Insert(1400, DateTimeHelpers.ToByteArray(new DateTime((long)(ticksPerDay * 2.2))));

            return result;
        }

        public static PieList<double, byte[]> GetLocalTimeCurveWithIncreasingDateTimeFromStartOfDay()
        {
            var result = new PieList<double, byte[]>();
            result.Insert(1000, DateTimeHelpers.ToByteArray(new DateTime((long)(ticksPerDay * 0.8))));
            result.Insert(1100, DateTimeHelpers.ToByteArray(new DateTime((long)(ticksPerDay * 0.9))));
            result.Insert(1200, DateTimeHelpers.ToByteArray(new DateTime((long)(ticksPerDay * 1))));
            result.Insert(1300, DateTimeHelpers.ToByteArray(new DateTime((long)(ticksPerDay * 1.1))));
            result.Insert(1400, DateTimeHelpers.ToByteArray(new DateTime((long)(ticksPerDay * 1.2))));

            return result;
        }

        public static PieList<double, double> GetDepthValueCurve()
        {
            var result = new PieList<double, double>();

            result.Insert(1050, -2.5);
            result.Insert(1100, -1.5);
            result.Insert(1210, -0.5);
            result.Insert(1340, 0.5);
            result.Insert(1390, 1.5);

            return result;
        }

        public static PieList<long, double> GetTicksValueCurveWithIncreasingDateTime()
        {
            var result = new PieList<long, double>();
            result.Insert((long)(ticksPerDay * 1.85), -2.5);
            result.Insert((long)(ticksPerDay * 1.9), -1.5);
            result.Insert((long)(ticksPerDay * 2.01), -0.5);
            result.Insert((long)(ticksPerDay * 2.14), 0.5);
            result.Insert((long)(ticksPerDay * 2.19), 1.5);

            return result;
        }

        public static PieList<long, double> GetTicksValueCurveWithIncreasingDateTimeFromLocalTimeMin()
        {
            var result = new PieList<long, double>();
            result.Insert(0, -2.5);
            result.Insert((long)(ticksPerDay * 0.05), -1.5);
            result.Insert((long)(ticksPerDay * 0.16), -0.5);
            result.Insert((long)(ticksPerDay * 0.29), 0.5);
            result.Insert((long)(ticksPerDay * 0.34), 1.5);

            return result;
        }

        public static PieList<long, double> GetTicksValueCurveWithIncreasingDateTimeFromStartOfDay()
        {
            var result = new PieList<long, double>();
            result.Insert((long)(ticksPerDay * 0.85), -2.5);
            result.Insert((long)(ticksPerDay * 0.9), -1.5);
            result.Insert((long)(ticksPerDay * 1.01), -0.5);
            result.Insert((long)(ticksPerDay * 1.14), 0.5);
            result.Insert((long)(ticksPerDay * 1.19), 1.5);

            return result;
        }

        //public static PieList<double, double> GetSecondsValueCurveWithIncreasingDateTimeFromLocalTimeMin()
        //{
        //    var result = new PieList<double, double>();

        //    result.Insert(0, -2.5);
        //    result.Insert(5, -1.5);
        //    result.Insert(16, -0.5);
        //    result.Insert(29, 0.5);
        //    result.Insert(34, 1.5);

        //    return result;
        //}

        //public static PieList<double, double> GetSecondsValueCurveWithDecreasingDateTimeFromLocalTimeMin()
        //{
        //    var result = new PieList<double, double>();

        //    result.Insert(34, -2.5);
        //    result.Insert(29, -1.5);
        //    result.Insert(16, -0.5);
        //    result.Insert(5, 0.5);
        //    result.Insert(0, 1.5);

        //    return result;
        //}

       

        //public static PieList<double, double> GetSecondsValueCurveWithIncreasingDateTime()
        //{
        //    var result = new PieList<double, double>();

        //    result.Insert(15, -2.5);
        //    result.Insert(20, -1.5);
        //    result.Insert(31, -0.5);
        //    result.Insert(44, 0.5);
        //    result.Insert(49, 1.5);

        //    return result;
        //}

        //public static PieList<double, double> GetSecondsValueCurveWithDecreasingDateTime()
        //{
        //    var result = new PieList<double, double>();

        //    result.Insert(45, -2.5);
        //    result.Insert(40, -1.5);
        //    result.Insert(29, -0.5);
        //    result.Insert(16, 0.5);
        //    result.Insert(11, 1.5);

        //    return result;
        //}
    }
}
