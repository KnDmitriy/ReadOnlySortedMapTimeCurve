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

        public static PieList<double, byte[]> GetLocalTimeCurveWithIncreasingDateTime()
        {
            var result = new PieList<double, byte[]>();

            result.Insert(1000, DateTimeHelpers.ToByteArray(new DateTime(10 * ticksPerSecond)));                       
            result.Insert(1100, DateTimeHelpers.ToByteArray(new DateTime(20 * ticksPerSecond)));
            result.Insert(1200, DateTimeHelpers.ToByteArray(new DateTime(30 * ticksPerSecond)));
            result.Insert(1300, DateTimeHelpers.ToByteArray(new DateTime(40 * ticksPerSecond)));
            result.Insert(1400, DateTimeHelpers.ToByteArray(new DateTime(50 * ticksPerSecond)));

            return result;
        }

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

        public static PieList<double, double> GetDepthValueCurve()
        {
            var result = new PieList<double, double>();

            result.Insert(1050, -2.5);
            result.Insert(1120, -1.5);
            result.Insert(1210, -0.5);
            result.Insert(1340, 0.5);
            result.Insert(1390, 1.5);

            return result;
        }

        //public static PieList<double, double> GetDepthValueCurveWithIncreasingDateTime()
        //{
        //    var result = new PieList<double, double>();

        //    result.Insert(1050, -2.5);
        //    result.Insert(1120, -1.5);
        //    result.Insert(1210, -0.5);
        //    result.Insert(1340, 0.5);
        //    result.Insert(1390, 1.5);

        //    return result;
        //}

        //public static PieList<double, double> GetDepthValueCurveWithDecreasingDateTime()
        //{
        //    //var result = new PieList<double, double>();

        //    //result.Insert(1050, -2.5);
        //    //result.Insert(1120, -1.5);
        //    //result.Insert(1210, -0.5);
        //    //result.Insert(1340, 0.5);
        //    //result.Insert(1390, 1.5);

        //    //return result;
        //    return GetDepthValueCurveWithIncreasingDateTime();
        //}

        public static PieList<double, double> GetSecondsValueCurveWithIncreasingDateTime()
        {
            var result = new PieList<double, double>();

            result.Insert(15, -2.5);
            result.Insert(22, -1.5);
            result.Insert(31, -0.5);
            result.Insert(44, 0.5);
            result.Insert(49, 1.5);

            return result;
        }

        public static PieList<double, double> GetSecondsValueCurveWithDecreasingDateTime()
        {
            var result = new PieList<double, double>();

            result.Insert(45, -2.5);
            result.Insert(38, -1.5);
            result.Insert(29, -0.5);
            result.Insert(16, 0.5);
            result.Insert(11, 1.5);

            return result;
        }
    }
}
