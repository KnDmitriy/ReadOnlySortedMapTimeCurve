using System;
using System.Collections.Generic;
using System.Linq;
using Collections;
using IReadOnlySortedMapTimeCurve;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace ReadOnlySortedMapTimeCurveTests
{
    [TestFixture]
    public class TicksFromByteArrayTests
    {
        private const long ticksPerSecond = 10_000_000L;

        [Test]
        public void ShouldThrowNullExceptionDuringInitializationTicksFromByteArray()
        {            
            Assert.Throws<ArgumentNullException>(() => new TicksFromByteArray(null));
        }

        [Test]
        public void ShouldNotThrowNullExceptionDuringInitializationTicksFromByteArray()
        {
            var localTime = new Mock<IReadOnlySortedMap<double, byte[]>>();

            IReadOnlyList<byte[]> byteArraysList = new List<byte[]>();
            localTime.Setup(l => l.Values).Returns(byteArraysList);

            var ticksFromByteArray = new TicksFromByteArray(localTime.Object);
            Assert.That(ticksFromByteArray, Is.Not.Null);
        }

        [Test]
        public void ShouldThrowNullExceptionDuringInitializationTicksList()
        {
            var localTime = new Mock<IReadOnlySortedMap<double, byte[]>>();
            IReadOnlyList<byte[]> readOnlybyteArraysList = null;

            localTime.Setup(l => l.Values).Returns(readOnlybyteArraysList);
            Assert.Throws<ArgumentNullException>(() => new TicksFromByteArray(localTime.Object));
        }

        [Test]
        public void ShouldThrowExceptionWrongArraySize()
        {
            const int rightArraySize = 8;
            const int wrongArraySize = 4;
            var localTime = new Mock<IReadOnlySortedMap<double, byte[]>>();
            IReadOnlyList<byte[]> readOnlybyteArraysList = new List<byte[]>
            {
                new byte[rightArraySize],
                new byte[wrongArraySize],
            };

            localTime.Setup(l => l.Values).Returns(readOnlybyteArraysList);
            Assert.Throws<ArgumentException>(() => new TicksFromByteArray(localTime.Object));
        }

        [Test]
        public void ShouldGetRightValueByKey()
        {
            var localTime = TestCurvesHelper.GetLocalTimeCurveWithIncreasingDateTime();
            TicksFromByteArray widthTicksCurve = new TicksFromByteArray(localTime.ToSortedMap());
            const long rightTicks = 20 * ticksPerSecond;
            const double key = 1100;

            Assert.That(Equals(rightTicks, widthTicksCurve[key]));
        }

        [Test]
        public void ShouldGetRightItemFromTicksListByIndex()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void ShouldGetRightKeyValuePair()
        {
            var localTime = TestCurvesHelper.GetLocalTimeCurveWithIncreasingDateTime();
            TicksFromByteArray widthTicksCurve = new TicksFromByteArray(localTime.ToSortedMap());
            KeyValuePair<double, long> rightKeyValuePair = new KeyValuePair<double, long>(1100, 20 * ticksPerSecond);
            const int index = 1;

            Assert.That(rightKeyValuePair, Is.EqualTo(widthTicksCurve[index]));
        }

        [Test]
        public void ShouldGetRightCountForTicksList()
        {
            const int arraySize = 8;
            const int rightCount = 3;
            IReadOnlyList<byte[]> source = Enumerable.Repeat(new byte[arraySize], rightCount).ToArray();

            var localTime = new Mock<IReadOnlySortedMap<double, byte[]>>();
            localTime.Setup(l => l.Count).Returns(source.Count);
            localTime.Setup(l => l.Values).Returns(source);

            var testedObject = new TicksFromByteArray(localTime.Object);
            Assert.That(rightCount, Is.EqualTo(testedObject.Count));
        }

        [Test]
        public void ShouldGetRightKeys()
        {
            var localTime = new Mock<IReadOnlySortedMap<double, byte[]>>();
            localTime.Setup(l => l.Keys).Returns(new double[] { 1000, 1100 });
            localTime.Setup(l => l.Values).Returns(new List<byte[]>());

            var testedObject = new TicksFromByteArray(localTime.Object);
            Assert.That(localTime.Object.Keys.SequenceEqual(testedObject.Keys));
        }

        [Test]
        public void ShouldGetRightValues()
        {
            const long ticks1 = 10 * ticksPerSecond;
            const long ticks2 = 20 * ticksPerSecond;
            IReadOnlyList<long> ticksList = new long[] { ticks1, ticks2 };
            IReadOnlyList<byte[]> byteArraysList = new List<byte[]>
            {
                DateTimeHelpers.ToByteArray(new DateTime(ticks1)),
                DateTimeHelpers.ToByteArray(new DateTime(ticks2))
            };
            var localTime = new Mock<IReadOnlySortedMap<double, byte[]>>();
            localTime.Setup(l => l.Values).Returns(byteArraysList);

            var testedObject = new TicksFromByteArray(localTime.Object);
            Assert.That(ticksList.SequenceEqual(testedObject.Values));
        }

        [Test]
        public void ShouldBinarySearch()
        {
           throw new NotImplementedException();
        }

        [Test]
        public void ShouldRightContainsKey()
        {
            const double key1 = 1000;
            const double key2 = 1100;
            const double nonExistingKey = -1000;
            var localTime = new Mock<IReadOnlySortedMap<double, byte[]>>();
            localTime.Setup(l => l.Keys).Returns(new double[] { key1, key2 });
            localTime.Setup(l => l.Values).Returns(new List<byte[]>());

            var testedObject = new TicksFromByteArray(localTime.Object);
            Assert.That(localTime.Object.ContainsKey(key2), Is.EqualTo(testedObject.ContainsKey(key2)));
            Assert.That(localTime.Object.ContainsKey(nonExistingKey), 
                Is.EqualTo(testedObject.ContainsKey(nonExistingKey)));
        }

        [Test]
        public void ShouldGetEnumerator()
        {
            throw new NotImplementedException();
        }


        [Test]
        public void ShouldTryGetValue()
        {
            const double nonExistingKey = 1;
            var localTime = TestCurvesHelper.GetLocalTimeCurveWithIncreasingDateTime();

            var testedObject = new TicksFromByteArray(localTime.ToSortedMap());

            Assert.That(testedObject.TryGetValue(localTime[0].Key, out long outValue), Is.True);
            Assert.That(DateTimeHelpers.CreateFromByteArray(localTime[0].Value).Ticks, Is.EqualTo(outValue));

            Assert.That(testedObject.TryGetValue(nonExistingKey, out outValue), Is.False);
            Assert.That(0, Is.EqualTo(outValue));
        }

        [Test]
        public void ShouldIEnumerableGetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
