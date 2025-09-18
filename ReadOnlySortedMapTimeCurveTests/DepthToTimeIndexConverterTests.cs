using System;
using System.Collections.Generic;
using Collections;
using IReadOnlySortedMapTimeCurve;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using ReadOnlySortedMapTimeCurve;

namespace ReadOnlySortedMapTimeCurveTests
{
    [TestFixture]
    public class DepthToTimeIndexConverterTests
    {
        [Test]
        public void ShouldThrowArgumentNullExceptionWithNullLocalTime()
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
        public void ShouldThrowArgumentNullExceptionDuringConverting()
        {
            var localTime = new Mock<IReadOnlySortedMap<double, byte[]>>();
            localTime.Setup(l => l.Values).Returns(new List<byte[]>());
            var converter = new DepthToTimeIndexConverter(localTime.Object, TypeOfTimeCalculation.LocalTimeMin);
            Assert.Throws<ArgumentNullException>(() => converter.Convert(null));
            converter = new DepthToTimeIndexConverter(localTime.Object, TypeOfTimeCalculation.StartOfDay);
            Assert.Throws<ArgumentNullException>(() => converter.Convert(null));
        }

        [Test]
        public void ShouldConvertRightOnLocalTimeMin()
        {
            var localTime = TestCurvesHelper.GetLocalTimeCurveWithIncreasingDateTime();
            var converter = new DepthToTimeIndexConverter(localTime.ToSortedMap(), TypeOfTimeCalculation.LocalTimeMin);
            var obtainedResult = converter.Convert(TestCurvesHelper.GetDepthValueCurve().ToSortedMap());
            var expectedResult = TestCurvesHelper.GetTicksValueCurveWithIncreasingDateTimeFromLocalTimeMin();
            Assert.That(obtainedResult, Is.Not.Null);
            for (var i = 0; i < obtainedResult.Count; i++)
            {
                Assert.That(obtainedResult[i], Is.EqualTo(expectedResult[i]));
            }
        }

        [Test]
        public void ShouldConvertRightOnStartOfDay()
        {
            var localTime = TestCurvesHelper.GetLocalTimeCurveWithIncreasingDateTime();
            var converter = new DepthToTimeIndexConverter(localTime.ToSortedMap(), TypeOfTimeCalculation.StartOfDay);
            var obtainedResult = converter.Convert(TestCurvesHelper.GetDepthValueCurve().ToSortedMap());
            var expectedResult = TestCurvesHelper.GetTicksValueCurveWithIncreasingDateTime();
            Assert.That(obtainedResult, Is.Not.Null);
            for (var i = 0; i < obtainedResult.Count; i++)
            {
                Assert.That(obtainedResult[i], Is.EqualTo(expectedResult[i]));
            }

        }
    }
}
