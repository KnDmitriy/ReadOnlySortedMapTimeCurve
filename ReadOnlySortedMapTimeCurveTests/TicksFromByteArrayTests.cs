using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Collections;
using IReadOnlySortedMapTimeCurve;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using ReadOnlySortedMapTimeCurve;
using Moq;
using System.Runtime.Remoting.Messaging;

namespace ReadOnlySortedMapTimeCurveTests
{
    [TestFixture]
    public class TicksFromByteArrayTests
    {
        // private readonly PieList<double, byte[]> curveLocalTime = new PieList<double, byte[]>();
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

            IReadOnlyList<byte[]> byteList = new List<byte[]>();
            localTime.Setup(l => l.Values).Returns(byteList);

            var ticksFromByteArray = new TicksFromByteArray(localTime.Object);
            Assert.That(ticksFromByteArray, Is.Not.Null);
        }

        [Test]
        public void ShouldThrowNullExceptionDuringInitializationTicksList()
        {
            var localTime = new Mock<IReadOnlySortedMap<double, byte[]>>();
            IReadOnlyList<byte[]> readOnlyByteList = null;

            localTime.Setup(l => l.Values).Returns(readOnlyByteList);
            Assert.Throws<ArgumentNullException>(() => new TicksFromByteArray(localTime.Object));
        }

        [Test]
        public void ShouldThrowExceptionWrongArraysSize()
        {
            var localTime = new Mock<IReadOnlySortedMap<double, byte[]>>();
            int rightArraySize = 8;
            int wrongArraySize = 4;

            var byteList = new List<byte[]>
            {
                new byte[rightArraySize],
                new byte[wrongArraySize],
            };
            IReadOnlyList<byte[]> readOnlyByteList = byteList;
            
            localTime.Setup(l => l.Values).Returns(readOnlyByteList);

            Assert.Throws<ArgumentException>(() => new TicksFromByteArray(localTime.Object));
        }

        [Test]
        public void ShouldGetRightValueByKey()
        {
            var localTime = TestCurvesHelper.GetLocalTimeCurveWithIncreasingDateTime();
            TicksFromByteArray widthTicksCurve = new TicksFromByteArray(localTime.ToSortedMap());
            long rightTicks = 20 * ticksPerSecond;
            double key = 1100;

            long obtainedTicks = widthTicksCurve[key];

            Assert.That(Equals(rightTicks, obtainedTicks));
        }

        [Test]
        public void ShouldGetRightItemFromTicksListByIndex()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void ShouldGetRightKeyValuePair()
        {
            //TicksFromByteArray curveWidthTicks = new TicksFromByteArray(curveLocalTime.ToSortedMap());

            //long ticks = curveWidthTicks[(double)1100];

            //Assert.That(Equals(20 * ticksPerSecond, ticks));
            throw new NotImplementedException();
        }

        [Test]
        public void ShouldGetRightCountForTicksList()
        {
            var localTime = new Mock<IReadOnlySortedMap<double, byte[]>>();
            int arraySize = 8;
            int rightCount = 3;
            var byteList = new List<byte[]>
            {
                new byte[arraySize],
                new byte[arraySize],
                new byte[arraySize],
            };
            IReadOnlyList<byte[]> readOnlyByteList = byteList;
            localTime.Setup(l => l.Count).Returns(readOnlyByteList.Count);
            localTime.Setup(l => l.Values).Returns(readOnlyByteList);

            var localTimeTicksFromByteArrayObject = new TicksFromByteArray(localTime.Object);

            Assert.That(rightCount, Is.EqualTo(localTimeTicksFromByteArrayObject.Count));
        }

        
    }
}
