using System;
using IReadOnlySortedMapTimeCurve;
using NUnit.Framework;
using Collections;
using ReadOnlySortedMapTimeCurve;
using NUnit.Framework.Legacy;

namespace ReadOnlySortedMapTimeCurveTests
{
    [TestFixture]
    public class DateTimeHelpersTests
    {
        //private readonly PieList<double, byte[]> localTimeCurve = new PieList<double, byte[]>();
        //private readonly PieList<double, double> depthValueCurve = new PieList<double, double>();
        //private readonly PieList<double, double> correctSecondsCurve = new PieList<double, double>();
        private static readonly DateTime dateTimeForTests = new DateTime(2025, 9, 2, 1, 1, 30, 300);
        private static readonly double dateTimeForTestsInSecondsFromStartOfDay = 3600 + 60 + 30 + 0.3;
        private const long ticksPerSecond = (long)1e7;
        private const double secondsPerTick = 1e-7;

        //public void SetupLocalTimeWithIncreasingDateTime()
        //{
        //    localTimeCurve.Clear();
        //    byte[] dateTimeByteArray1 = DateTimeHelpers.ToByteArray(new DateTime(
        //        10 * ticksPerSecond));
        //    byte[] dateTimeByteArray2 = DateTimeHelpers.ToByteArray(new DateTime(
        //        20 * ticksPerSecond));
        //    byte[] dateTimeByteArray3 = DateTimeHelpers.ToByteArray(new DateTime(
        //        30 * ticksPerSecond));
        //    byte[] dateTimeByteArray4 = DateTimeHelpers.ToByteArray(new DateTime(
        //        40 * ticksPerSecond));
        //    byte[] dateTimeByteArray5 = DateTimeHelpers.ToByteArray(new DateTime(
        //        50 * ticksPerSecond));
        //    localTimeCurve.Insert(1000, dateTimeByteArray1);
        //    localTimeCurve.Insert(1100, dateTimeByteArray2);
        //    localTimeCurve.Insert(1200, dateTimeByteArray3);
        //    localTimeCurve.Insert(1300, dateTimeByteArray4);
        //    localTimeCurve.Insert(1400, dateTimeByteArray5);
        //}
        //public void SetupDepthValueWithIncreasingDateTime()
        //{
        //    depthValueCurve.Clear();
        //    depthValueCurve.Insert(1050, -2.5);
        //    depthValueCurve.Insert(1120, -1.5);
        //    depthValueCurve.Insert(1210, -0.5);
        //    depthValueCurve.Insert(1340, 0.5);
        //    depthValueCurve.Insert(1390, 1.5);
        //}
        //public void SetupSecondsValueWithIncreasingDateTime()
        //{
        //    correctSecondsCurve.Clear();
        //    correctSecondsCurve.Insert(15, -2.5);
        //    correctSecondsCurve.Insert(22, -1.5);
        //    correctSecondsCurve.Insert(31, -0.5);
        //    correctSecondsCurve.Insert(44, 0.5);
        //    correctSecondsCurve.Insert(49, 1.5);
        //}


        //public void SetupLocalTimeWithDecreasingDateTime()
        //{
        //    localTimeCurve.Clear();
        //    byte[] dateTimeByteArray1 = DateTimeHelpers.ToByteArray(new DateTime(
        //        50 * ticksPerSecond));
        //    byte[] dateTimeByteArray2 = DateTimeHelpers.ToByteArray(new DateTime(
        //        40 * ticksPerSecond));
        //    byte[] dateTimeByteArray3 = DateTimeHelpers.ToByteArray(new DateTime(
        //        30 * ticksPerSecond));
        //    byte[] dateTimeByteArray4 = DateTimeHelpers.ToByteArray(new DateTime(
        //        20 * ticksPerSecond));
        //    byte[] dateTimeByteArray5 = DateTimeHelpers.ToByteArray(new DateTime(
        //        10 * ticksPerSecond));
        //    localTimeCurve.Insert(1000, dateTimeByteArray1);
        //    localTimeCurve.Insert(1100, dateTimeByteArray2);
        //    localTimeCurve.Insert(1200, dateTimeByteArray3);
        //    localTimeCurve.Insert(1300, dateTimeByteArray4);
        //    localTimeCurve.Insert(1400, dateTimeByteArray5);
        //}
        //public void SetupDepthValueWithDecreasingDateTime()
        //{
        //    depthValueCurve.Clear();
        //    depthValueCurve.Insert(1050, -2.5);
        //    depthValueCurve.Insert(1120, -1.5);
        //    depthValueCurve.Insert(1210, -0.5);
        //    depthValueCurve.Insert(1340, 0.5);
        //    depthValueCurve.Insert(1390, 1.5);
        //}
        //public void SetupSecondsValueWithDecreasingDateTime()
        //{
        //    correctSecondsCurve.Clear();
        //    correctSecondsCurve.Insert(45, -2.5);
        //    correctSecondsCurve.Insert(38, -1.5);
        //    correctSecondsCurve.Insert(29, -0.5);
        //    correctSecondsCurve.Insert(16, 0.5);
        //    correctSecondsCurve.Insert(11, 1.5);
        //}

        //// TODO: Понять, нужен ли этот метод?
        //public void SetupLocalTimeWithIncreasingDateTimeFromStartOfCurve()
        //{
        //    localTimeCurve.Clear();
        //    byte[] dateTimeByteArray1 = DateTimeHelpers.ToByteArray(new DateTime(
        //        0));
        //    byte[] dateTimeByteArray2 = DateTimeHelpers.ToByteArray(new DateTime(
        //        10 * ticksPerSecond));
        //    byte[] dateTimeByteArray3 = DateTimeHelpers.ToByteArray(new DateTime(
        //        20 * ticksPerSecond));
        //    byte[] dateTimeByteArray4 = DateTimeHelpers.ToByteArray(new DateTime(
        //        30 * ticksPerSecond));
        //    byte[] dateTimeByteArray5 = DateTimeHelpers.ToByteArray(new DateTime(
        //        40 * ticksPerSecond));
        //    localTimeCurve.Insert(1000, dateTimeByteArray1);
        //    localTimeCurve.Insert(1100, dateTimeByteArray2);
        //    localTimeCurve.Insert(1200, dateTimeByteArray3);
        //    localTimeCurve.Insert(1300, dateTimeByteArray4);
        //    localTimeCurve.Insert(1400, dateTimeByteArray5);
        //}


        [Test]
        public void ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => DateTimeHelpers.CreateFromByteArray(null));
        }

        [Test]
        public void ShouldBeRightArraySize()
        {
            // arrange
            byte[] byteArray = new byte[4];

            // assert 
            Assert.Throws<ArgumentException> (() => DateTimeHelpers.CreateFromByteArray(byteArray), "Byte array is wrong");
        }

        [Test]
        public void ShouldConvertByteArrayToDateTime()
        {
            // arrange
            var dt = DateTime.Now;
            var bytes = dt.ToByteArray();

            // act
            var dateTime = DateTimeHelpers.CreateFromByteArray(bytes);

            // assert
            Assert.That(dt, Is.EqualTo(dateTime));
        }

        [Test]
        public void ShouldGetDataFromCurve()
        {
            // arrange
            var depthValueCurve = TestCurvesHelper.GetDepthValueCurve();

            // act
            double? key = depthValueCurve[0].Key;
            double? val = depthValueCurve[0].Value;

            // assert
            Assert.That(key.HasValue);
            Assert.That(val.HasValue);
        }

        [Test]
        public void ShouldGetTimeInSecondsFromStartOfDay()
        {
            // act
            double seconds = DateTimeHelpers.GetTimeInSecondsFromStartOfDay(dateTimeForTests);

            // assert
            ClassicAssert.AreEqual(dateTimeForTestsInSecondsFromStartOfDay, seconds, 1e-10);
        }

        //[Test]
        //public void ShouldGetMinDateTimeFromLocalTimeWithIncreasingDateTime() 
        //{
        //    // arrange
        //    SetupLocalTimeWithIncreasingDateTime();
        //    DateTime minDateTime = new DateTime(10 * coefficientForConvertingTicksToSeconds);


        //    // act
        //    DateTime dateTime = DateTimeHelpers.GetMinDateTimeFromLocalTime(localTimeCurve);

        //    // assert
        //    Assert.That(dateTime.Equals(minDateTime));
        //}
        //[Test]
        //public void ShouldGetMinDateTimeFromLocalTimeWithDecreasingDateTime()
        //{
        //    // arrange
        //    SetupLocalTimeWithDecreasingDateTime();
        //    DateTime minDateTime = new DateTime(10 * coefficientForConvertingTicksToSeconds);


        //    // act
        //    DateTime dateTime = DateTimeHelpers.GetMinDateTimeFromLocalTime(localTimeCurve);

        //    // assert
        //    Assert.That(dateTime.Equals(minDateTime));
        //}

        [Test]
        public void ShouldGetSecondsFromStartOfCurve()
        {
            // arrange
            var depthValueCurve = TestCurvesHelper.GetDepthValueCurve();
            var localTimeCurve = TestCurvesHelper.GetLocalTimeCurveWithIncreasingDateTime();
            var localTimeSortedMap = localTimeCurve.ToSortedMap();
            DateTime minDateTime = new DateTime(10 * ticksPerSecond);

            // act
            double seconds = DateTimeHelpers.GetSecondsFromStartOfCurve(localTimeSortedMap);

            // assert
            ClassicAssert.AreEqual(10, seconds, 1e-10);
        }

        [Test]
        public void ShouldGetSecondsFromDateTime()
        {
            // arrange
            DateTime minDateTime = new DateTime(10 * ticksPerSecond);

            // act
            double seconds = DateTimeHelpers.GetSecondsFromDateTime(minDateTime);

            // assert
            ClassicAssert.AreEqual(10, seconds, 1e-10);
        }

        [Test]
        public void ShouldConvertTicksToSeconds()
        {
            // arrange
            var localTimeCurve = TestCurvesHelper.GetLocalTimeCurveWithIncreasingDateTime();
            var depthValueCurve = TestCurvesHelper.GetDepthValueCurve();
            var correctSecondsCurve = TestCurvesHelper.GetSecondsValueCurveWithIncreasingDateTime();
            var depthValueCurveSortedMap = depthValueCurve.ToSortedMap();
            var depthTicksCurveSortedMap = localTimeCurve.ToSortedMap();
            // act
            //// DateTime rightDateTime = DateTimeHelpers.CreateFromByteArray(depthTicksCurve.First.Value); // упорядочены ли значения времени?
            var converter = new DepthToTimeIndexConverter(depthTicksCurveSortedMap);
            var resultedCurve = converter.Convert(depthValueCurveSortedMap);

            //for (var i = 0; i < depthValueCurve.Count; i++)
            //{
            //    Console.WriteLine(curveSecondsValue[i].ToString());
            //}

            // assert
            Assert.That(resultedCurve != null);
            Assert.That(resultedCurve.Count == depthValueCurveSortedMap.Count);
            Assert.That(resultedCurve.Count == correctSecondsCurve.Count);
            for (int i = 0; i < resultedCurve.Count; i++)
            {
                Assert.That(resultedCurve[i].Key == correctSecondsCurve[i].Key);
                Assert.That(Equals(resultedCurve[i].Value, correctSecondsCurve[i].Value));
            }
        }


    }    
}
