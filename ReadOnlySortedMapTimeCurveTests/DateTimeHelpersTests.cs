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
        private static readonly DateTime dateTimeForTests = new DateTime(2025, 9, 2, 1, 1, 30, 300);
        private static readonly double dateTimeForTestsInSecondsFromStartOfDay = 3600 + 60 + 30 + 0.3;
        private const long ticksPerSecond = (long)1e7;
        private const double secondsPerTick = 1e-7;

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
            var converter = new DepthToTimeIndexConverter(depthTicksCurveSortedMap);
            var resultedCurve = converter.Convert(depthValueCurveSortedMap);

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
