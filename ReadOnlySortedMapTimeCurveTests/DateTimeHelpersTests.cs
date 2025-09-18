using System;
using System.Collections.Generic;
using Collections;
using IReadOnlySortedMapTimeCurve;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.Legacy;
using ReadOnlySortedMapTimeCurve;

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
            Assert.Throws<ArgumentNullException>(() => DateTimeHelpers.ToDateTime(null));
        }

        [Test]
        public void ShouldBeRightArraySize()
        {
            // arrange
            byte[] byteArray = new byte[4];

            // assert 
            Assert.Throws<ArgumentException> (() => DateTimeHelpers.ToDateTime(byteArray), "Byte array is wrong");
        }

        [Test]
        public void ShouldConvertByteArrayToDateTime()
        {
            // arrange
            var dt = DateTime.Now;
            var bytes = dt.ToByteArray();

            // act
            var dateTime = DateTimeHelpers.ToDateTime(bytes);

            // assert
            Assert.That(dateTime, Is.EqualTo(dt));
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
            //double seconds = DateTimeHelpers.GetSecondsFromStartOfCurve(localTimeSortedMap);

            // assert
            //ClassicAssert.AreEqual(10, seconds, 1e-10);
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
            var correctSecondsCurve = TestCurvesHelper.GetTicksValueCurveWithIncreasingDateTime();

            var depthValueCurveSortedMap = depthValueCurve.ToSortedMap();
            var depthTicksCurveSortedMap = localTimeCurve.ToSortedMap();

            //// act
            //var converter = new DepthToTimeIndexConverter(depthTicksCurveSortedMap);
            //var resultedCurve = converter.Convert(depthValueCurveSortedMap);

            //// assert
            //Assert.That(resultedCurve != null);
            //Assert.That(resultedCurve.Count == depthValueCurveSortedMap.Count);
            //Assert.That(resultedCurve.Count == correctSecondsCurve.Count);
            //for (int i = 0; i < resultedCurve.Count; i++)
            //{
            //    Assert.That(resultedCurve[i].Key == correctSecondsCurve[i].Key);
            //    Assert.That(Equals(resultedCurve[i].Value, correctSecondsCurve[i].Value));
            //}
        }

        [Test]
        public void ShouldGetMinTicksFromLocalTime()
        {
            var localTimeMock = new Mock<IReadOnlySortedMap<double, byte[]>>();
            localTimeMock.Setup(l => l.Values).Returns(new List<byte[]>());
            var ticksFromByteArrayMock = new TicksFromByteArray(localTimeMock.Object);

            var localTime = TestCurvesHelper.GetLocalTimeCurveWithIncreasingDateTime();
            var ticksFromByteArray = new TicksFromByteArray(localTime.ToSortedMap());
            long firstTicksFromCurve = localTime[0].Value.ToDateTime().Ticks;


            Assert.Throws<ArgumentException>(() => DateTimeHelpers.GetMinTicksFromLocalTime(ticksFromByteArrayMock));
            Assert.Throws<ArgumentNullException>(() => DateTimeHelpers.GetMinTicksFromLocalTime(null));
            Assert.That(DateTimeHelpers.GetMinTicksFromLocalTime(ticksFromByteArray),
                Is.EqualTo(firstTicksFromCurve));
        }

        [Test]
        public void ShouldGetMinTicksFromLocalTimeFromStartOfDay()
        {
            var localTimeMock = new Mock<IReadOnlySortedMap<double, byte[]>>();
            localTimeMock.Setup(l => l.Values).Returns(new List<byte[]>());
            var ticksFromByteArrayMock = new TicksFromByteArray(localTimeMock.Object);

            var localTime = TestCurvesHelper.GetLocalTimeCurveWithIncreasingDateTime();
            var ticksFromByteArray = new TicksFromByteArray(localTime.ToSortedMap());

            var expectedLocalTime = TestCurvesHelper.GetLocalTimeCurveWithIncreasingDateTimeFromStartOfDay();
            long firstTicksFromCurve = expectedLocalTime[0].Value.ToDateTime().Ticks;


            Assert.Throws<ArgumentNullException>(() => DateTimeHelpers.GetMinTicksFromStartOfDay(null));
            Assert.Throws<ArgumentException>(() => DateTimeHelpers.GetMinTicksFromStartOfDay(ticksFromByteArrayMock));
            Assert.That(DateTimeHelpers.GetMinTicksFromStartOfDay(ticksFromByteArray),
                Is.EqualTo(firstTicksFromCurve));
        }
    }    
}
