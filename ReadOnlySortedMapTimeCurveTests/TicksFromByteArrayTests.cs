using System;
using System.Collections.Generic;
using System.Linq;
using Collections;
using TimeReadOnlySortedMap;
using Moq;
using NUnit.Framework;

namespace TimeReadOnlySortedMapTests
{
    [TestFixture]
    public class TicksFromByteArrayTests
    {
        private const long ticksPerSecond = 10_000_000L;

        [Test]
        public void ShouldThrowNullExceptionOnInitializationTicksFromByteArray()
        {            
            Assert.Throws<ArgumentNullException>(() => new ByteArrayWrapper(null));
        }

        [Test]
        public void ShouldNotThrowNullExceptionOnInitializationTicksFromByteArray()
        {
            var localTimeMock = new Mock<IReadOnlySortedMap<double, byte[]>>();

            IReadOnlyList<byte[]> byteArraysList = new List<byte[]>();
            localTimeMock.Setup(l => l.Values).Returns(byteArraysList);

            var ticksFromByteArray = new ByteArrayWrapper(localTimeMock.Object);
            Assert.That(ticksFromByteArray, Is.Not.Null);
        }

        [Test]
        public void ShouldThrowNullExceptionOnInitializationTicksList()
        {
            var localTimeMock = new Mock<IReadOnlySortedMap<double, byte[]>>();
            IReadOnlyList<byte[]> readOnlyByteArraysList = null;

            localTimeMock.Setup(l => l.Values).Returns(readOnlyByteArraysList);
            Assert.Throws<ArgumentNullException>(() => new ByteArrayWrapper(localTimeMock.Object));
        }

        [Test]
        public void ShouldGetRightCountForTicksList()
        {
            const int arraySize = 8;
            const int rightCount = 3;
            IReadOnlyList<byte[]> byteArraysArray = Enumerable.Repeat(new byte[arraySize], rightCount).ToArray();

            var localTimeMock = new Mock<IReadOnlySortedMap<double, byte[]>>();
            localTimeMock.Setup(l => l.Count).Returns(byteArraysArray.Count);
            localTimeMock.Setup(l => l.Values).Returns(byteArraysArray);

            var testedObject = new ByteArrayWrapper(localTimeMock.Object);
            Assert.That(testedObject.Count, Is.EqualTo(rightCount));
        }

        [Test]
        public void ShouldGetRightKeys()
        {
            var localTimeMock = new Mock<IReadOnlySortedMap<double, byte[]>>();
            localTimeMock.Setup(l => l.Keys).Returns(new double[] { 1000, 1100 });
            localTimeMock.Setup(l => l.Values).Returns(new List<byte[]>());

            var testedObject = new ByteArrayWrapper(localTimeMock.Object);
            Assert.That(localTimeMock.Object.Keys.SequenceEqual(testedObject.Keys));
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
            var localTimeMock = new Mock<IReadOnlySortedMap<double, byte[]>>();
            localTimeMock.Setup(l => l.Values).Returns(byteArraysList);

            var testedObject = new ByteArrayWrapper(localTimeMock.Object);
            Assert.That(ticksList.SequenceEqual(testedObject.Values));
        }

        [Test]
        public void ShouldBinarySearchInLocalTimeWithIncreasingDateTime()
        {
            var localTime = TestCurvesHelper.GetLocalTimeWithIncreasingDateTime();
            var testedObject = new ByteArrayWrapper(localTime.ToSortedMap());
            double key = localTime[0].Key;
            Assert.That(localTime.BinarySearch(key), Is.EqualTo(testedObject.BinarySearch(key)));
            key = localTime[localTime.Count - 1].Key;
            Assert.That(localTime.BinarySearch(key), Is.EqualTo(testedObject.BinarySearch(key)));

            key = localTime[1].Key;
            Assert.That(localTime.BinarySearch(key - 0.1),
                Is.EqualTo(testedObject.BinarySearch(key - 0.1)));

            Assert.That(localTime.BinarySearch(double.MinValue), 
                Is.EqualTo(testedObject.BinarySearch(double.MinValue)));
            Assert.That(localTime.BinarySearch(double.MaxValue), 
                Is.EqualTo(testedObject.BinarySearch(double.MaxValue)));
            Assert.That(localTime.BinarySearch(double.NegativeInfinity), 
                Is.EqualTo(testedObject.BinarySearch(double.NegativeInfinity)));
        }

        [Test]
        public void ShouldBinarySearchInLocalTimeWithDecreasingDateTime()
        {
            var localTime = TestCurvesHelper.GetLocalTimeWithDecreasingDateTime();
            var testedObject = new ByteArrayWrapper(localTime.ToSortedMap());
            double key = localTime[0].Key;
            Assert.That(localTime.BinarySearch(key), Is.EqualTo(testedObject.BinarySearch(key)));
            key = localTime[localTime.Count - 1].Key;
            Assert.That(localTime.BinarySearch(key), Is.EqualTo(testedObject.BinarySearch(key)));

            key = localTime[1].Key;
            Assert.That(localTime.BinarySearch(key - 0.1),
                Is.EqualTo(testedObject.BinarySearch(key - 0.1)));

            Assert.That(localTime.BinarySearch(double.MinValue),
                Is.EqualTo(testedObject.BinarySearch(double.MinValue)));
            Assert.That(localTime.BinarySearch(double.MaxValue),
                Is.EqualTo(testedObject.BinarySearch(double.MaxValue)));
            Assert.That(localTime.BinarySearch(double.NegativeInfinity),
                Is.EqualTo(testedObject.BinarySearch(double.NegativeInfinity)));
        }

        [Test]
        public void ShouldRightContainKey()
        {
            const double key1 = 1000;
            const double key2 = 1100;
            const double nonExistingKey = -1000;
            var localTimeMock = new Mock<IReadOnlySortedMap<double, byte[]>>();
            localTimeMock.Setup(l => l.Keys).Returns(new double[] { key1, key2 });
            localTimeMock.Setup(l => l.Values).Returns(new List<byte[]>());

            var testedObject = new ByteArrayWrapper(localTimeMock.Object);
            Assert.That(testedObject.ContainsKey(key2), Is.EqualTo(localTimeMock.Object.ContainsKey(key2)));
            Assert.That(testedObject.ContainsKey(nonExistingKey), 
                Is.EqualTo(localTimeMock.Object.ContainsKey(nonExistingKey)));
        }

        [Test]
        public void ShouldGetEnumerator()
        {
            var localTimeMock = new Mock<IReadOnlySortedMap<double, byte[]>>();
            localTimeMock.Setup(l => l.Values).Returns(new List<byte[]>());
            var testedObject = new ByteArrayWrapper(localTimeMock.Object);
            var enumerator = testedObject.GetEnumerator();
            Assert.That(enumerator, Is.Not.Null);
        }

        [Test]
        public void ShouldGetRightKeyByEnumerator()
        {
            var localTime = TestCurvesHelper.GetLocalTimeWithIncreasingDateTime();
            var localTimeEnumerator = localTime.GetEnumerator();
            var testedObject = new ByteArrayWrapper(localTime.ToSortedMap());
            var testedObjectEnumerator = testedObject.GetEnumerator();

            localTimeEnumerator.MoveNext();
            testedObjectEnumerator.MoveNext();
            Assert.That(testedObjectEnumerator.Current.Key, Is.EqualTo(localTimeEnumerator.Current.Key));
            localTimeEnumerator.MoveNext();
            testedObjectEnumerator.MoveNext();
            Assert.That(testedObjectEnumerator.Current.Key, Is.EqualTo(localTimeEnumerator.Current.Key));
        }


        [Test]
        public void ShouldTryGetValue()
        {
            const double nonExistingKey = 1;
            var localTime = TestCurvesHelper.GetLocalTimeWithIncreasingDateTime();

            var testedObject = new ByteArrayWrapper(localTime.ToSortedMap());

            Assert.That(testedObject.TryGetValue(localTime[0].Key, out long outValue), Is.True);
            Assert.That(DateTimeHelpers.ToDateTime(localTime[0].Value).Ticks, Is.EqualTo(outValue));

            Assert.That(testedObject.TryGetValue(nonExistingKey, out outValue), Is.False);
            Assert.That(0, Is.EqualTo(outValue));
        }
    }
}
