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
            Assert.Throws<ArgumentNullException>(() => new TicksFromByteArray(null));
        }

        [Test]
        public void ShouldNotThrowNullExceptionOnInitializationTicksFromByteArray()
        {
            var localTime = new Mock<IReadOnlySortedMap<double, byte[]>>();

            IReadOnlyList<byte[]> byteArraysList = new List<byte[]>();
            localTime.Setup(l => l.Values).Returns(byteArraysList);

            var ticksFromByteArray = new TicksFromByteArray(localTime.Object);
            Assert.That(ticksFromByteArray, Is.Not.Null);
        }

        [Test]
        public void ShouldThrowNullExceptionOnInitializationTicksList()
        {
            var localTime = new Mock<IReadOnlySortedMap<double, byte[]>>();
            IReadOnlyList<byte[]> readOnlyByteArraysList = null;

            localTime.Setup(l => l.Values).Returns(readOnlyByteArraysList);
            Assert.Throws<ArgumentNullException>(() => new TicksFromByteArray(localTime.Object));
        }

        [Test]
        public void ShouldThrowExceptionWrongArraySize()
        {
            const int rightArraySize = 8;
            const int wrongArraySize = 4;
            var localTime = new Mock<IReadOnlySortedMap<double, byte[]>>();
            IReadOnlyList<byte[]> readOnlyByteArraysList = new List<byte[]>
            {
                new byte[rightArraySize],
                new byte[wrongArraySize],
            };

            localTime.Setup(l => l.Values).Returns(readOnlyByteArraysList);
            Assert.Throws<ArgumentException>(() => new TicksFromByteArray(localTime.Object));
        }

        //[Test]
        //public void ShouldGetRightValueByKey()
        //{
        //    var localTime = new Mock<IReadOnlySortedMap<double, byte[]>>();
        //    TicksFromByteArray testedObject = new TicksFromByteArray(localTime.Object);
        //    IReadOnlyList<byte[]> readOnlyByteArraysList = new List<byte[]>();
        //    localTime.Setup(l => l.Values).Returns(readOnlyByteArraysList);
        //    localTime.Setup(l => l[100]).Returns(1000);


        //    var localTime = TestCurvesHelper.GetLocalTimeCurveWithIncreasingDateTime();
        //    TicksFromByteArray depthTicksCurve = new TicksFromByteArray(localTime.ToSortedMap());
        //    const long rightTicks = 20 * ticksPerSecond;
        //    const double key = 1100;

        //    Assert.That(Equals(rightTicks, testedObject[key]));
        //}

        //[Test]
        //public void ShouldGetRightKeyValuePair()
        //{
        //    var localTime = TestCurvesHelper.GetLocalTimeWithIncreasingDateTime();
        //    TicksFromByteArray depthTicksCurve = new TicksFromByteArray(localTime.ToSortedMap());
        //    KeyValuePair<double, long> rightKeyValuePair = 
        //        new KeyValuePair<double, long>(1100, (long)0.9 * ticksPerDay);
        //    const int index = 1;

        //    Assert.That(depthTicksCurve[index], Is.EqualTo(rightKeyValuePair));
        //}

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
            Assert.That(testedObject.Count, Is.EqualTo(rightCount));
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
        public void ShouldBinarySearchInLocalTimeWithIncreasingDateTime()
        {
            var localTime = TestCurvesHelper.GetLocalTimeWithIncreasingDateTime();
            var testedObject = new TicksFromByteArray(localTime.ToSortedMap());
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
            var testedObject = new TicksFromByteArray(localTime.ToSortedMap());
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
            var localTime = new Mock<IReadOnlySortedMap<double, byte[]>>();
            localTime.Setup(l => l.Values).Returns(new List<byte[]>());
            var testedObject = new TicksFromByteArray(localTime.Object);
            var enumerator = testedObject.GetEnumerator();
            Assert.That(enumerator, Is.Not.Null);
        }

        [Test]
        public void ShouldGetRightKeyByEnumerator()
        {
            var localTime = TestCurvesHelper.GetLocalTimeWithIncreasingDateTime();
            var localTimeEnumerator = localTime.GetEnumerator();
            var testedObject = new TicksFromByteArray(localTime.ToSortedMap());
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

            var testedObject = new TicksFromByteArray(localTime.ToSortedMap());

            Assert.That(testedObject.TryGetValue(localTime[0].Key, out long outValue), Is.True);
            Assert.That(DateTimeHelpers.ToDateTime(localTime[0].Value).Ticks, Is.EqualTo(outValue));

            Assert.That(testedObject.TryGetValue(nonExistingKey, out outValue), Is.False);
            Assert.That(0, Is.EqualTo(outValue));
        }
    }
}
