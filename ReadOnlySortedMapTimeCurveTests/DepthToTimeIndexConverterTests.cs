using System;
using System.Collections.Generic;
using Collections;
using TimeReadOnlySortedMap;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using ReadOnlySortedMapTimeCurve;

namespace TimeReadOnlySortedMapTests
{
    [TestFixture]
    public class DepthToTimeIndexConverterTests
    {
        private const double tolerance = 1e-10;

        [Test]
        public void ShouldThrowArgumentNullExceptionOnNullLocalTime()
        {
            Assert.Throws<ArgumentNullException>(
                () => new DepthToTimeIndexConverter(null, TypeOfTimeCalculation.LocalTimeMin));
            Assert.Throws<ArgumentNullException>(
                () => new DepthToTimeIndexConverter(null, TypeOfTimeCalculation.StartOfDay));
        }

        [Test]
        public void ShouldThrowArgumentOutOfRangeExceptionOnWrongType()
        {
            var nonExistingEnumItem = (TypeOfTimeCalculation)int.MaxValue;
            var localTime = new Mock<IReadOnlySortedMap<double, byte[]>>();
            localTime.Setup(l => l.Values).Returns(new List<byte[]>());
            Assert.Throws<ArgumentOutOfRangeException>(
                () => new DepthToTimeIndexConverter(localTime.Object, nonExistingEnumItem));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionOnConvert()
        {
            var localTimeMock = new Mock<IReadOnlySortedMap<double, byte[]>>();
            localTimeMock.Setup(l => l.Values).Returns(new List<byte[]>());
            var converter = new DepthToTimeIndexConverter(localTimeMock.Object, TypeOfTimeCalculation.LocalTimeMin);
            Assert.Throws<ArgumentNullException>(() => converter.Convert(null));
            converter = new DepthToTimeIndexConverter(localTimeMock.Object, TypeOfTimeCalculation.StartOfDay);
            Assert.Throws<ArgumentNullException>(() => converter.Convert(null));
        }

        #region Convert from local time min

        [Test]
        public void ShouldConvertRightOnLocalTimeMinWithIncreasingDateTime()
        {
            var localTime = TestCurvesHelper.GetLocalTimeWithIncreasingDateTime();
            var converter = new DepthToTimeIndexConverter(localTime.ToSortedMap(), TypeOfTimeCalculation.LocalTimeMin);
            var obtainedResult = converter.Convert(TestCurvesHelper.GetDepthValue().ToSortedMap());
            var expectedResult = TestCurvesHelper.GetSecondsValueWithIncreasingDateTimeFromLocalTimeMin();
            Assert.That(obtainedResult, Is.Not.Null);
            for (var i = 0; i < obtainedResult.Count; i++)
            {
                ClassicAssert.AreEqual(expectedResult[i].Key, obtainedResult[i].Key, tolerance);
                ClassicAssert.AreEqual(expectedResult[i].Value, obtainedResult[i].Value, tolerance);
            }
        }

        [Test]
        public void ShouldConvertRightOnLocalTimeMinWithDecreasingDateTime()
        {
            var localTime = TestCurvesHelper.GetLocalTimeWithDecreasingDateTime();
            var converter = new DepthToTimeIndexConverter(localTime.ToSortedMap(), TypeOfTimeCalculation.LocalTimeMin);
            var obtainedResult = converter.Convert(TestCurvesHelper.GetDepthValue().ToSortedMap());
            var expectedResult = TestCurvesHelper.GetSecondsValueWithDecreasingDateTimeFromLocalTimeMin();
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
        public void ShouldConvertRightOnStartOfDayWithIncreasingDateTime()
        {
            var localTime = TestCurvesHelper.GetLocalTimeWithIncreasingDateTime();
            var converter = new DepthToTimeIndexConverter(localTime.ToSortedMap(), TypeOfTimeCalculation.StartOfDay);
            var obtainedResult = converter.Convert(TestCurvesHelper.GetDepthValue().ToSortedMap());
            var expectedResult = TestCurvesHelper.GetSecondsValueWithIncreasingDateTimeFromStartOfDay();
            Assert.That(obtainedResult, Is.Not.Null);
            for (var i = 0; i < obtainedResult.Count; i++)
            {
                ClassicAssert.AreEqual(expectedResult[i].Key, obtainedResult[i].Key, tolerance);
                ClassicAssert.AreEqual(expectedResult[i].Value, obtainedResult[i].Value, tolerance);
            }
        }

        [Test]
        public void ShouldConvertRightOnStartOfDayWithDecreasingDateTime()
        {
            var localTime = TestCurvesHelper.GetLocalTimeWithDecreasingDateTime();
            var converter = new DepthToTimeIndexConverter(localTime.ToSortedMap(), TypeOfTimeCalculation.StartOfDay);
            var obtainedResult = converter.Convert(TestCurvesHelper.GetDepthValue().ToSortedMap());
            var expectedResult = TestCurvesHelper.GetSecondsValueWithDecreasingDateTimeFromStartOfDay();
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
