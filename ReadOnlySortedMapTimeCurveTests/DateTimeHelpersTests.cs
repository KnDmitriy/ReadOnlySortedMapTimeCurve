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
        private const long ticksPerSecond = 10_000_000L;

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
            Assert.Throws<ArgumentNullException>(() => DateTimeHelpers.ToDateTime(null));
        }

        [Test]
        public void ShouldConvertByteArrayToDateTime()
        {
            var dateTimeNow = DateTime.Now;
            var bytes = dateTimeNow.ToByteArray();

            var dateTimeNowFromBytes = DateTimeHelpers.ToDateTime(bytes);

            Assert.That(dateTimeNowFromBytes, Is.EqualTo(dateTimeNow));
        }

        [Test]
        public void ShouldGetDataFromCurve()
        {
            var valueByDepthMap = TestCurvesHelper.GetValuesByDepth();

            double? key = valueByDepthMap[0].Key;
            double? val = valueByDepthMap[0].Value;

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
        public void ShouldConvertToSecondsRight()
        {
            long ticks = ticksPerSecond * 2;
            double secondsFromTicks = 2;
            Assert.That(ticks.ToSeconds(), Is.EqualTo(secondsFromTicks));
            Assert.That(((double)ticks).ToSeconds(), Is.EqualTo(secondsFromTicks));
        }


        [Test]
        public void ShouldRightGetMinTicksFromLocalTimeMapWithIncreasingDateTime()
        {
            var localTimeMapMock = new Mock<IReadOnlySortedMap<double, byte[]>>();
            localTimeMapMock.Setup(l => l.Values).Returns(new List<byte[]>());
            var byteArrayWrapperMock = new ByteArrayWrapper(localTimeMapMock.Object);

            var localTimeMap = TestCurvesHelper.GetLocalTimeMapWithIncreasingDateTime();
            var byteArrayWrapper = new ByteArrayWrapper(localTimeMap.ToSortedMap());
            long firstTicksFromLocalTimeMap = localTimeMap[0].Value.ToDateTime().Ticks;

            Assert.Throws<ArgumentNullException>(() => DateTimeHelpers.GetMinTicks(null));
            Assert.That(DateTimeHelpers.GetMinTicks(byteArrayWrapperMock),
               Is.EqualTo(0));      
            Assert.That(DateTimeHelpers.GetMinTicks(byteArrayWrapper),
                Is.EqualTo(firstTicksFromLocalTimeMap));
        }

        [Test]
        public void ShouldRightGetMinTicksFromLocalTimeMapWithDecreasingDateTime()
        {
            var localTimeMapMock = new Mock<IReadOnlySortedMap<double, byte[]>>();
            localTimeMapMock.Setup(l => l.Values).Returns(new List<byte[]>());
            var byteArrayWrapperMock = new ByteArrayWrapper(localTimeMapMock.Object);

            var localTimeMap = TestCurvesHelper.GetLocalTimeMapWithDecreasingDateTime();
            var byteArrayWrapper = new ByteArrayWrapper(localTimeMap.ToSortedMap());
            long firstTicksFromLocalTimeMap = localTimeMap[localTimeMap.Count - 1].Value.ToDateTime().Ticks;

            Assert.Throws<ArgumentNullException>(() => DateTimeHelpers.GetMinTicks(null));
            Assert.That(DateTimeHelpers.GetMinTicks(byteArrayWrapperMock),
               Is.EqualTo(0));
            Assert.That(DateTimeHelpers.GetMinTicks(byteArrayWrapper),
                Is.EqualTo(firstTicksFromLocalTimeMap));
        }

        [Test]
        public void ShouldRightGetStartOfDayFromLocalTimeMapWithIncreasingDateTime()
        {
            var localTimeMapMock = new Mock<IReadOnlySortedMap<double, byte[]>>();
            localTimeMapMock.Setup(l => l.Values).Returns(new List<byte[]>());
            var byteArrayWrapperMock = new ByteArrayWrapper(localTimeMapMock.Object);

            var localTimeMap = TestCurvesHelper.GetLocalTimeMapWithIncreasingDateTime();
            var byteArrayWrapper = new ByteArrayWrapper(localTimeMap.ToSortedMap());
            long firstTicksFromLocalTimeMap = DateTimeHelpers.GetStartOfDay(byteArrayWrapper[0].Value);


            Assert.Throws<ArgumentNullException>(() => DateTimeHelpers.GetStartOfDayFromTicks(null));
            Assert.That(DateTimeHelpers.GetStartOfDayFromTicks(byteArrayWrapperMock),
                Is.EqualTo(0));
            Assert.That(DateTimeHelpers.GetStartOfDayFromTicks(byteArrayWrapper),
                Is.EqualTo(DateTimeHelpers.GetStartOfDay(firstTicksFromLocalTimeMap)));
        }

        [Test]
        public void ShouldRightGetStartOfDayFromLocalTimeMapWithDecreasingDateTime()
        {
            var localTimeMapMock = new Mock<IReadOnlySortedMap<double, byte[]>>();
            localTimeMapMock.Setup(l => l.Values).Returns(new List<byte[]>());
            var byteArrayWrapperMock = new ByteArrayWrapper(localTimeMapMock.Object);

            var localTimeMap = TestCurvesHelper.GetLocalTimeMapWithDecreasingDateTime();
            var byteArrayWrapper = new ByteArrayWrapper(localTimeMap.ToSortedMap());
            long firstTicksFromLocalTimeMap = DateTimeHelpers.GetStartOfDay(
                byteArrayWrapper[byteArrayWrapper.Count - 1].Value);


            Assert.Throws<ArgumentNullException>(() => DateTimeHelpers.GetStartOfDayFromTicks(null));
            Assert.That(DateTimeHelpers.GetStartOfDayFromTicks(byteArrayWrapperMock),
               Is.EqualTo(0));
            Assert.That(DateTimeHelpers.GetStartOfDayFromTicks(byteArrayWrapper),
                Is.EqualTo(DateTimeHelpers.GetStartOfDay(firstTicksFromLocalTimeMap)));
        }
    }    
}
