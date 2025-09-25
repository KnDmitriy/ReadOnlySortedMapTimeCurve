using System;
using System.Collections.Generic;
using Collections;
using TimeReadOnlySortedMap;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace TimeReadOnlySortedMapTests
{
    [TestFixture]
    public class DepthToTimeIndexConverterTests
    {
        private const double tolerance = 1e-10;

        [Test]
        public void ShouldThrowArgumentNullExceptionOnNullLocalTimeMap()
        {
            Assert.Throws<ArgumentNullException>(
                () => new DepthToTimeIndexConverter(null, TimeOrigin.StartTime));
            Assert.Throws<ArgumentNullException>(
                () => new DepthToTimeIndexConverter(null, TimeOrigin.StartOfDay));
        }

        [Test]
        public void ShouldThrowArgumentOutOfRangeExceptionOnWrongType()
        {
            var nonExistingEnumItem = (TimeOrigin)int.MaxValue;
            var localTimeMap = new Mock<IReadOnlySortedMap<double, byte[]>>();
            localTimeMap.Setup(l => l.Values).Returns(new List<byte[]>());
            Assert.Throws<ArgumentOutOfRangeException>(
                () => new DepthToTimeIndexConverter(localTimeMap.Object, nonExistingEnumItem));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionOnConvert()
        {
            var localTimeMapMock = new Mock<IReadOnlySortedMap<double, byte[]>>();
            localTimeMapMock.Setup(l => l.Values).Returns(new List<byte[]>());
            var converter = new DepthToTimeIndexConverter(localTimeMapMock.Object, TimeOrigin.StartTime);
            Assert.Throws<ArgumentNullException>(() => converter.Convert(null));
            converter = new DepthToTimeIndexConverter(localTimeMapMock.Object, TimeOrigin.StartOfDay);
            Assert.Throws<ArgumentNullException>(() => converter.Convert(null));
        }

        #region Convert from local time min

        [Test]
        public void ShouldConvertRightFromStartTimeWithIncreasingDateTime()
        {
            var localTimeMap = TestCurvesHelper.GetLocalTimeMapWithIncreasingDateTime();
            var converter = new DepthToTimeIndexConverter(localTimeMap.ToSortedMap(), TimeOrigin.StartTime);
            var obtainedResult = converter.Convert(TestCurvesHelper.GetValuesByDepth().ToSortedMap());
            var expectedResult = TestCurvesHelper.GetValuesBySecondsWithIncreasingDateTimeFromStartTime();
            Assert.That(obtainedResult, Is.Not.Null);
            for (var i = 0; i < obtainedResult.Count; i++)
            {
                ClassicAssert.AreEqual(expectedResult[i].Key, obtainedResult[i].Key, tolerance);
                ClassicAssert.AreEqual(expectedResult[i].Value, obtainedResult[i].Value, tolerance);
            }
        }

        [Test]
        public void ShouldConvertRightFromStartTimeWithDecreasingDateTime()
        {
            var localTimeMap = TestCurvesHelper.GetLocalTimeMapWithDecreasingDateTime();
            var converter = new DepthToTimeIndexConverter(localTimeMap.ToSortedMap(), TimeOrigin.StartTime);
            var obtainedResult = converter.Convert(TestCurvesHelper.GetValuesByDepth().ToSortedMap());
            var expectedResult = TestCurvesHelper.GetValuesBySecondsWithDecreasingDateTimeFromStartTime();
            Assert.That(obtainedResult, Is.Not.Null);
            for (var i = 0; i < obtainedResult.Count; i++)
            {
                ClassicAssert.AreEqual(expectedResult[i].Key, obtainedResult[i].Key, tolerance);
                ClassicAssert.AreEqual(expectedResult[i].Value, obtainedResult[i].Value, tolerance);
            }
        }

        #endregion

        #region Convert on start of day

        [Test]
        public void ShouldConvertRightFromStartOfDayWithIncreasingDateTime()
        {
            var localTimeMap = TestCurvesHelper.GetLocalTimeMapWithIncreasingDateTime();
            var converter = new DepthToTimeIndexConverter(localTimeMap.ToSortedMap(), TimeOrigin.StartOfDay);
            var obtainedResult = converter.Convert(TestCurvesHelper.GetValuesByDepth().ToSortedMap());
            var expectedResult = TestCurvesHelper.GetValuesBySecondsWithIncreasingDateTimeFromStartOfDay();
            Assert.That(obtainedResult, Is.Not.Null);
            for (var i = 0; i < obtainedResult.Count; i++)
            {
                ClassicAssert.AreEqual(expectedResult[i].Key, obtainedResult[i].Key, tolerance);
                ClassicAssert.AreEqual(expectedResult[i].Value, obtainedResult[i].Value, tolerance);
            }
        }

        [Test]
        public void ShouldConvertRightFromStartOfDayWithDecreasingDateTime()
        {
            var localTimeMap = TestCurvesHelper.GetLocalTimeMapWithDecreasingDateTime();
            var converter = new DepthToTimeIndexConverter(localTimeMap.ToSortedMap(), TimeOrigin.StartOfDay);
            var obtainedResult = converter.Convert(TestCurvesHelper.GetValuesByDepth().ToSortedMap());
            var expectedResult = TestCurvesHelper.GetValuesBySecondsWithDecreasingDateTimeFromStartOfDay();
            Assert.That(obtainedResult, Is.Not.Null);
            for (var i = 0; i < obtainedResult.Count; i++)
            {
                ClassicAssert.AreEqual(expectedResult[i].Key, obtainedResult[i].Key, tolerance);
                ClassicAssert.AreEqual(expectedResult[i].Value, obtainedResult[i].Value, tolerance);
            }
        }

        #endregion
    }
}
