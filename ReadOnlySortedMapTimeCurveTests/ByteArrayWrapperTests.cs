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
    public class ByteArrayWrapperTests
    {
        private const long ticksPerSecond = 10_000_000L;

        [Test]
        public void ShouldThrowNullExceptionOnInitializationByteArrayWrapper()
        {
            Assert.Throws<ArgumentNullException>(() => new ByteArrayWrapper(null));
        }

        [Test]
        public void ShouldNotThrowNullExceptionOnInitializationByteArrayWrapper()
        {
            var localTimeMapMock = new Mock<IReadOnlySortedMap<double, byte[]>>();

            IReadOnlyList<byte[]> byteArraysList = new List<byte[]>();
            localTimeMapMock.Setup(l => l.Values).Returns(byteArraysList);

            var byteArrayWrapper = new ByteArrayWrapper(localTimeMapMock.Object);
            Assert.That(byteArrayWrapper, Is.Not.Null);
        }

        [Test]
        public void ShouldThrowNullExceptionOnInitializationTicksList()
        {
            var localTimeMapMock = new Mock<IReadOnlySortedMap<double, byte[]>>();
            IReadOnlyList<byte[]> readOnlyByteArraysList = null;

            localTimeMapMock.Setup(l => l.Values).Returns(readOnlyByteArraysList);
            Assert.Throws<ArgumentNullException>(() => new ByteArrayWrapper(localTimeMapMock.Object));
        }

        [Test]
        public void ShouldGetRightCountForTicksList()
        {
            const int arraySize = 8;
            const int rightCount = 3;
            IReadOnlyList<byte[]> byteArraysArray = Enumerable.Repeat(new byte[arraySize], rightCount).ToArray();

            var localTimeMapMock = new Mock<IReadOnlySortedMap<double, byte[]>>();
            localTimeMapMock.Setup(l => l.Count).Returns(byteArraysArray.Count);
            localTimeMapMock.Setup(l => l.Values).Returns(byteArraysArray);

            var testedObject = new ByteArrayWrapper(localTimeMapMock.Object);
            Assert.That(testedObject.Count, Is.EqualTo(rightCount));
        }

        [Test]
        public void ShouldGetRightKeys()
        {
            var localTimeMapMock = new Mock<IReadOnlySortedMap<double, byte[]>>();
            localTimeMapMock.Setup(l => l.Keys).Returns(new double[] { 1000, 1100 });
            localTimeMapMock.Setup(l => l.Values).Returns(new List<byte[]>());

            var testedObject = new ByteArrayWrapper(localTimeMapMock.Object);
            Assert.That(localTimeMapMock.Object.Keys.SequenceEqual(testedObject.Keys));
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
            var localTimeMapMock = new Mock<IReadOnlySortedMap<double, byte[]>>();
            localTimeMapMock.Setup(l => l.Values).Returns(byteArraysList);

            var testedObject = new ByteArrayWrapper(localTimeMapMock.Object);
            Assert.That(ticksList.SequenceEqual(testedObject.Values));
        }

        [Test]
        public void ShouldBinarySearchInLocalTimeMapWithIncreasingDateTime()
        {
            var localTimeMap = TestCurvesHelper.GetLocalTimeMapWithIncreasingDateTime();
            var testedObject = new ByteArrayWrapper(localTimeMap.ToSortedMap());
            double key = localTimeMap[0].Key;
            Assert.That(localTimeMap.BinarySearch(key), Is.EqualTo(testedObject.BinarySearch(key)));
            key = localTimeMap[localTimeMap.Count - 1].Key;
            Assert.That(localTimeMap.BinarySearch(key), Is.EqualTo(testedObject.BinarySearch(key)));

            key = localTimeMap[1].Key;
            Assert.That(localTimeMap.BinarySearch(key - 0.1),
                Is.EqualTo(testedObject.BinarySearch(key - 0.1)));

            Assert.That(localTimeMap.BinarySearch(double.MinValue),
                Is.EqualTo(testedObject.BinarySearch(double.MinValue)));
            Assert.That(localTimeMap.BinarySearch(double.MaxValue),
                Is.EqualTo(testedObject.BinarySearch(double.MaxValue)));
            Assert.That(localTimeMap.BinarySearch(double.NegativeInfinity),
                Is.EqualTo(testedObject.BinarySearch(double.NegativeInfinity)));
        }

        [Test]
        public void ShouldBinarySearchInLocalTimeMapWithDecreasingDateTime()
        {
            var localTimeMap = TestCurvesHelper.GetLocalTimeMapWithDecreasingDateTime();
            var testedObject = new ByteArrayWrapper(localTimeMap.ToSortedMap());
            double key = localTimeMap[0].Key;
            Assert.That(localTimeMap.BinarySearch(key), Is.EqualTo(testedObject.BinarySearch(key)));
            key = localTimeMap[localTimeMap.Count - 1].Key;
            Assert.That(localTimeMap.BinarySearch(key), Is.EqualTo(testedObject.BinarySearch(key)));

            key = localTimeMap[1].Key;
            Assert.That(localTimeMap.BinarySearch(key - 0.1),
                Is.EqualTo(testedObject.BinarySearch(key - 0.1)));

            Assert.That(localTimeMap.BinarySearch(double.MinValue),
                Is.EqualTo(testedObject.BinarySearch(double.MinValue)));
            Assert.That(localTimeMap.BinarySearch(double.MaxValue),
                Is.EqualTo(testedObject.BinarySearch(double.MaxValue)));
            Assert.That(localTimeMap.BinarySearch(double.NegativeInfinity),
                Is.EqualTo(testedObject.BinarySearch(double.NegativeInfinity)));
        }

        [Test]
        public void ShouldRightContainKey()
        {
            const double key1 = 1000;
            const double key2 = 1100;
            const double nonExistingKey = -1000;
            var localTimeMapMock = new Mock<IReadOnlySortedMap<double, byte[]>>();
            localTimeMapMock.Setup(l => l.Keys).Returns(new double[] { key1, key2 });
            localTimeMapMock.Setup(l => l.Values).Returns(new List<byte[]>());

            var testedObject = new ByteArrayWrapper(localTimeMapMock.Object);
            Assert.That(testedObject.ContainsKey(key2), Is.EqualTo(localTimeMapMock.Object.ContainsKey(key2)));
            Assert.That(testedObject.ContainsKey(nonExistingKey),
                Is.EqualTo(localTimeMapMock.Object.ContainsKey(nonExistingKey)));
        }

        [Test]
        public void ShouldGetEnumerator()
        {
            var localTimeMapMock = new Mock<IReadOnlySortedMap<double, byte[]>>();
            localTimeMapMock.Setup(l => l.Values).Returns(new List<byte[]>());
            var testedObject = new ByteArrayWrapper(localTimeMapMock.Object);
            var enumerator = testedObject.GetEnumerator();
            Assert.That(enumerator, Is.Not.Null);
        }

        [Test]
        public void ShouldGetRightKeyByEnumerator()
        {
            var localTimeMap = TestCurvesHelper.GetLocalTimeMapWithIncreasingDateTime();
            var localTimeMapEnumerator = localTimeMap.GetEnumerator();
            var testedObject = new ByteArrayWrapper(localTimeMap.ToSortedMap());
            var testedObjectEnumerator = testedObject.GetEnumerator();

            localTimeMapEnumerator.MoveNext();
            testedObjectEnumerator.MoveNext();
            Assert.That(testedObjectEnumerator.Current.Key, Is.EqualTo(localTimeMapEnumerator.Current.Key));
            localTimeMapEnumerator.MoveNext();
            testedObjectEnumerator.MoveNext();
            Assert.That(testedObjectEnumerator.Current.Key, Is.EqualTo(localTimeMapEnumerator.Current.Key));
        }


        [Test]
        public void ShouldTryGetValue()
        {
            const double nonExistingKey = 1;
            var localTimeMap = TestCurvesHelper.GetLocalTimeMapWithIncreasingDateTime();

            var testedObject = new ByteArrayWrapper(localTimeMap.ToSortedMap());

            Assert.That(testedObject.TryGetValue(localTimeMap[0].Key, out long outValue), Is.True);
            Assert.That(DateTimeHelpers.ToDateTime(localTimeMap[0].Value).Ticks, Is.EqualTo(outValue));

            Assert.That(testedObject.TryGetValue(nonExistingKey, out outValue), Is.False);
            Assert.That(0, Is.EqualTo(outValue));
        }
    }
}
