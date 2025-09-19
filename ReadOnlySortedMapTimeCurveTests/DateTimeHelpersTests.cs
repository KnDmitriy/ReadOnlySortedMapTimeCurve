using System;
using System.Collections.Generic;
using Collections;
using TimeReadOnlySortedMap;
using Moq;
using NUnit.Framework;

namespace TimeReadOnlySortedMapTests
{
    [TestFixture]
    public class DateTimeHelpersTests
    {
        [Test]
        public void ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => DateTimeHelpers.ToDateTime(null));
        }

        [Test]
        public void ShouldBeRightArraySize()
        {
            byte[] byteArray = new byte[4];
            Assert.Throws<ArgumentException> (() => DateTimeHelpers.ToDateTime(byteArray), "Byte array is wrong");
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionToDateTime()
        {
            byte[] byteArray = new byte[4];
            Assert.Throws<ArgumentNullException>(() => DateTimeHelpers.ToDateTime(null));
        }

        [Test]
        public void ShouldConvertByteArrayToDateTime()
        {
            var dt = DateTime.Now;
            var bytes = dt.ToByteArray();

            var dateTime = DateTimeHelpers.ToDateTime(bytes);

            Assert.That(dateTime, Is.EqualTo(dt));
        }

        [Test]
        public void ShouldGetDataFromCurve()
        {
            var depthValueCurve = TestCurvesHelper.GetDepthValue();

            double? key = depthValueCurve[0].Key;
            double? val = depthValueCurve[0].Value;

            Assert.That(key.HasValue);
            Assert.That(val.HasValue);
        }

        [Test]
        public void ShouldThrowArgumentOutOfRangeExceptionFromToSeconds()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => ((long)-1).ToSeconds());
            Assert.Throws<ArgumentOutOfRangeException>(() => (-1.1).ToSeconds());
        }


        [Test]
        public void ShouldGetMinTicksFromLocalTimeWithIncreasingDateTime()
        {
            var localTimeMock = new Mock<IReadOnlySortedMap<double, byte[]>>();
            localTimeMock.Setup(l => l.Values).Returns(new List<byte[]>());
            var ticksFromByteArrayMock = new TicksFromByteArray(localTimeMock.Object);

            var localTime = TestCurvesHelper.GetLocalTimeWithIncreasingDateTime();
            var ticksFromByteArray = new TicksFromByteArray(localTime.ToSortedMap());
            long firstTicksFromLocalTime = localTime[0].Value.ToDateTime().Ticks;

            Assert.Throws<ArgumentNullException>(() => DateTimeHelpers.GetMinTicksFromLocalTime(null));
            Assert.That(DateTimeHelpers.GetMinTicksFromLocalTime(ticksFromByteArrayMock),
               Is.EqualTo(0));      
            Assert.That(DateTimeHelpers.GetMinTicksFromLocalTime(ticksFromByteArray),
                Is.EqualTo(firstTicksFromLocalTime));
        }

        [Test]
        public void ShouldGetMinTicksFromLocalTimeFromStartOfDayWithIncreasingDateTime()
        {
            var localTimeMock = new Mock<IReadOnlySortedMap<double, byte[]>>();
            localTimeMock.Setup(l => l.Values).Returns(new List<byte[]>());
            var ticksFromByteArrayMock = new TicksFromByteArray(localTimeMock.Object);

            var localTime = TestCurvesHelper.GetLocalTimeWithIncreasingDateTime();
            var ticksFromByteArray = new TicksFromByteArray(localTime.ToSortedMap());
            long firstTicksFromLocalTime = DateTimeHelpers.GetStartOfDayInTicks(ticksFromByteArray[0].Value);


            Assert.Throws<ArgumentNullException>(() => DateTimeHelpers.GetStartOfDayInTicks(null));
            Assert.That(DateTimeHelpers.GetStartOfDayInTicks(ticksFromByteArrayMock),
               Is.EqualTo(0));
            Assert.That(DateTimeHelpers.GetStartOfDayInTicks(ticksFromByteArray),
                Is.EqualTo(DateTimeHelpers.GetStartOfDayInTicks(firstTicksFromLocalTime)));
        }

        [Test]
        public void ShouldGetMinTicksFromLocalTimeWithDecreasingDateTime()
        {
            var localTimeMock = new Mock<IReadOnlySortedMap<double, byte[]>>();
            localTimeMock.Setup(l => l.Values).Returns(new List<byte[]>());
            var ticksFromByteArrayMock = new TicksFromByteArray(localTimeMock.Object);

            var localTime = TestCurvesHelper.GetLocalTimeWithDecreasingDateTime();
            var ticksFromByteArray = new TicksFromByteArray(localTime.ToSortedMap());
            long firstTicksFromLocalTime = localTime[localTime.Count - 1].Value.ToDateTime().Ticks;

            Assert.Throws<ArgumentNullException>(() => DateTimeHelpers.GetMinTicksFromLocalTime(null));
            Assert.That(DateTimeHelpers.GetMinTicksFromLocalTime(ticksFromByteArrayMock),
               Is.EqualTo(0));
            Assert.That(DateTimeHelpers.GetMinTicksFromLocalTime(ticksFromByteArray),
                Is.EqualTo(firstTicksFromLocalTime));
        }

        [Test]
        public void ShouldGetMinTicksFromLocalTimeFromStartOfDayWithDecreasingDateTime()
        {
            var localTimeMock = new Mock<IReadOnlySortedMap<double, byte[]>>();
            localTimeMock.Setup(l => l.Values).Returns(new List<byte[]>());
            var ticksFromByteArrayMock = new TicksFromByteArray(localTimeMock.Object);

            var localTime = TestCurvesHelper.GetLocalTimeWithDecreasingDateTime();
            var ticksFromByteArray = new TicksFromByteArray(localTime.ToSortedMap());
            long firstTicksFromLocalTime = DateTimeHelpers.GetStartOfDayInTicks(
                ticksFromByteArray[ticksFromByteArray.Count - 1].Value);


            Assert.Throws<ArgumentNullException>(() => DateTimeHelpers.GetStartOfDayInTicks(null));
            Assert.That(DateTimeHelpers.GetStartOfDayInTicks(ticksFromByteArrayMock),
               Is.EqualTo(0));
            Assert.That(DateTimeHelpers.GetStartOfDayInTicks(ticksFromByteArray),
                Is.EqualTo(DateTimeHelpers.GetStartOfDayInTicks(firstTicksFromLocalTime)));
        }
    }    
}
