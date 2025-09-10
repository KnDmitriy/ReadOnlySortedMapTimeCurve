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
        private readonly PieList<double, byte[]> curveLocalTime = new PieList<double, byte[]>();
        private readonly PieList<double, double> curveDepthValue = new PieList<double, double>();
        private readonly PieList<double, double> correctSecondsCurve = new PieList<double, double>();
        private static readonly DateTime dateTimeForTests = new DateTime(2025, 9, 2, 1, 1, 30, 300);
        private static readonly double dateTimeForTestsInSecondsFromStartOfDay = 3600 + 60 + 30 + 0.3;
        private const long CoefficientSecondsPerTicks = (long)1e7;
        private const double CoefficientTicksPerSeconds = 1e-7;
        //public void SetupLocalTimeIncreasing()
        //{
        //    curveLocalTime.Clear();
        //    byte[] dateTimeByteArray1 = DateTimeHelpers.CreateFromDateTime(dateTimeForTests);
        //    byte[] dateTimeByteArray2 = DateTimeHelpers.CreateFromDateTime(dateTimeForTests.AddMinutes(1));
        //    byte[] dateTimeByteArray3 = DateTimeHelpers.CreateFromDateTime(dateTimeForTests.AddMinutes(2));
        //    byte[] dateTimeByteArray4 = DateTimeHelpers.CreateFromDateTime(dateTimeForTests.AddMinutes(3));
        //    byte[] dateTimeByteArray5 = DateTimeHelpers.CreateFromDateTime(dateTimeForTests.AddMinutes(4));
        //    curveLocalTime.Insert(1000.1, dateTimeByteArray1);
        //    curveLocalTime.Insert(1001.1, dateTimeByteArray2);
        //    curveLocalTime.Insert(1002.1, dateTimeByteArray3);
        //    curveLocalTime.Insert(1003.1, dateTimeByteArray4);
        //    curveLocalTime.Insert(1004.1, dateTimeByteArray5);
        //}
        public void SetupLocalTimeWithIncreasingDateTime()
        {
            curveLocalTime.Clear();
            byte[] dateTimeByteArray1 = DateTimeHelpers.ToByteArray(new DateTime(
                10 * CoefficientSecondsPerTicks));
            byte[] dateTimeByteArray2 = DateTimeHelpers.ToByteArray(new DateTime(
                20 * CoefficientSecondsPerTicks));
            byte[] dateTimeByteArray3 = DateTimeHelpers.ToByteArray(new DateTime(
                30 * CoefficientSecondsPerTicks));
            byte[] dateTimeByteArray4 = DateTimeHelpers.ToByteArray(new DateTime(
                40 * CoefficientSecondsPerTicks));
            byte[] dateTimeByteArray5 = DateTimeHelpers.ToByteArray(new DateTime(
                50 * CoefficientSecondsPerTicks));
            curveLocalTime.Insert(1000, dateTimeByteArray1);
            curveLocalTime.Insert(1100, dateTimeByteArray2);
            curveLocalTime.Insert(1200, dateTimeByteArray3);
            curveLocalTime.Insert(1300, dateTimeByteArray4);
            curveLocalTime.Insert(1400, dateTimeByteArray5);
        }
        public void SetupDepthValueWithIncreasingDateTime()
        {
            curveDepthValue.Clear();
            curveDepthValue.Insert(1050, -2.5);
            curveDepthValue.Insert(1120, -1.5);
            curveDepthValue.Insert(1210, -0.5);
            curveDepthValue.Insert(1340, 0.5);
            curveDepthValue.Insert(1390, 1.5);
        }
        public void SetupSecondsValueWithIncreasingDateTime()
        {
            correctSecondsCurve.Clear();
            correctSecondsCurve.Insert(15, -2.5);
            correctSecondsCurve.Insert(22, -1.5);
            correctSecondsCurve.Insert(31, -0.5);
            correctSecondsCurve.Insert(44, 0.5);
            correctSecondsCurve.Insert(49, 1.5);
        }


        public void SetupLocalTimeWithDecreasingDateTime()
        {
            curveLocalTime.Clear();
            byte[] dateTimeByteArray1 = DateTimeHelpers.ToByteArray(new DateTime(
                50 * CoefficientSecondsPerTicks));
            byte[] dateTimeByteArray2 = DateTimeHelpers.ToByteArray(new DateTime(
                40 * CoefficientSecondsPerTicks));
            byte[] dateTimeByteArray3 = DateTimeHelpers.ToByteArray(new DateTime(
                30 * CoefficientSecondsPerTicks));
            byte[] dateTimeByteArray4 = DateTimeHelpers.ToByteArray(new DateTime(
                20 * CoefficientSecondsPerTicks));
            byte[] dateTimeByteArray5 = DateTimeHelpers.ToByteArray(new DateTime(
                10 * CoefficientSecondsPerTicks));
            curveLocalTime.Insert(1000, dateTimeByteArray1);
            curveLocalTime.Insert(1100, dateTimeByteArray2);
            curveLocalTime.Insert(1200, dateTimeByteArray3);
            curveLocalTime.Insert(1300, dateTimeByteArray4);
            curveLocalTime.Insert(1400, dateTimeByteArray5);
        }
        public void SetupDepthValueWithDecreasingDateTime()
        {
            curveDepthValue.Clear();
            curveDepthValue.Insert(1050, -2.5);
            curveDepthValue.Insert(1120, -1.5);
            curveDepthValue.Insert(1210, -0.5);
            curveDepthValue.Insert(1340, 0.5);
            curveDepthValue.Insert(1390, 1.5);
        }
        public void SetupSecondsValueWithDecreasingDateTime()
        {
            correctSecondsCurve.Clear();
            correctSecondsCurve.Insert(45, -2.5);
            correctSecondsCurve.Insert(38, -1.5);
            correctSecondsCurve.Insert(29, -0.5);
            correctSecondsCurve.Insert(16, 0.5);
            correctSecondsCurve.Insert(11, 1.5);
        }

        // TODO: Понять, нужен ли этот метод?
        public void SetupLocalTimeWithIncreasingDateTimeFromStartOfCurve()
        {
            curveLocalTime.Clear();
            byte[] dateTimeByteArray1 = DateTimeHelpers.ToByteArray(new DateTime(
                0));
            byte[] dateTimeByteArray2 = DateTimeHelpers.ToByteArray(new DateTime(
                10 * CoefficientSecondsPerTicks));
            byte[] dateTimeByteArray3 = DateTimeHelpers.ToByteArray(new DateTime(
                20 * CoefficientSecondsPerTicks));
            byte[] dateTimeByteArray4 = DateTimeHelpers.ToByteArray(new DateTime(
                30 * CoefficientSecondsPerTicks));
            byte[] dateTimeByteArray5 = DateTimeHelpers.ToByteArray(new DateTime(
                40 * CoefficientSecondsPerTicks));
            curveLocalTime.Insert(1000, dateTimeByteArray1);
            curveLocalTime.Insert(1100, dateTimeByteArray2);
            curveLocalTime.Insert(1200, dateTimeByteArray3);
            curveLocalTime.Insert(1300, dateTimeByteArray4);
            curveLocalTime.Insert(1400, dateTimeByteArray5);
        }



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
            SetupDepthValueWithIncreasingDateTime();

            // act
            double? key = curveDepthValue[0].Key;
            double? val = curveDepthValue[0].Value;

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
        //    DateTime dateTime = DateTimeHelpers.GetMinDateTimeFromLocalTime(curveLocalTime);

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
        //    DateTime dateTime = DateTimeHelpers.GetMinDateTimeFromLocalTime(curveLocalTime);

        //    // assert
        //    Assert.That(dateTime.Equals(minDateTime));
        //}

        [Test]
        public void ShouldGetSecondsFromStartOfCurve()
        {
            // arrange
            SetupLocalTimeWithDecreasingDateTime();
            DateTime minDateTime = new DateTime(10 * CoefficientSecondsPerTicks);
            var localTimeSortedMap = curveLocalTime.ToSortedMap();

            // act
            double seconds = DateTimeHelpers.GetSecondsFromStartOfCurve(localTimeSortedMap);

            // assert
            ClassicAssert.AreEqual(10, seconds, 1e-10);
        }

        [Test]
        public void ShouldGetSecondsFromDateTime()
        {
            // arrange
            DateTime minDateTime = new DateTime(10 * CoefficientSecondsPerTicks);

            // act
            double seconds = DateTimeHelpers.GetSecondsFromDateTime(minDateTime);

            // assert
            ClassicAssert.AreEqual(10, seconds, 1e-10);
        }

        [Test]
        public void ShouldConvertTicksToSeconds()
        {
            // arrange
            SetupLocalTimeWithIncreasingDateTime();
            SetupDepthValueWithIncreasingDateTime();
            SetupSecondsValueWithIncreasingDateTime();
            var curveDepthValueSortedMap = curveDepthValue.ToSortedMap();
            var curveDepthTicksSortedMap = curveLocalTime.ToSortedMap();
            // act
            //// DateTime rightDateTime = DateTimeHelpers.CreateFromByteArray(curveDepthTicks.First.Value); // упорядочены ли значения времени?
            var converter = new DepthToTimeIndexConverter(curveDepthTicksSortedMap);
            var resultedCurve = converter.Convert(curveDepthValueSortedMap);

            //for (var i = 0; i < curveDepthValue.Count; i++)
            //{
            //    Console.WriteLine(curveSecondsValue[i].ToString());
            //}

            // assert
            Assert.That(resultedCurve != null);
            Assert.That(resultedCurve.Count == curveDepthValueSortedMap.Count);
            Assert.That(resultedCurve.Count == correctSecondsCurve.Count);
            for (int i = 0; i < resultedCurve.Count; i++)
            {
                Assert.That(resultedCurve[i].Key == correctSecondsCurve[i].Key);
                Assert.That(Equals(resultedCurve[i].Value, correctSecondsCurve[i].Value));
            } 
        }

        
    }    
}
