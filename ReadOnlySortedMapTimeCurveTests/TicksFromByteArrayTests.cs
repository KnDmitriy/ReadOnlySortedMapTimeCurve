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
        private readonly PieList<double, byte[]> curveLocalTime = new PieList<double, byte[]>();
        private const long ticksPerSecond = 10_000_000L;

        [Test]
        public void ShouldThrowNullException()
        {            
            Assert.Throws<ArgumentNullException>(() => new TicksFromByteArray(null));
        }

        [Test]
        public void ShouldNotThrowNullException()
        {
            var localTime = new Mock<IReadOnlySortedMap<double, byte[]>>();

            IReadOnlyList<byte[]> byteList = new List<byte[]>();
            localTime.Setup(l => l.Values).Returns(byteList);

            var ticksFromByteArray = new TicksFromByteArray(localTime.Object);
            Assert.That(ticksFromByteArray, Is.Not.Null);
        }

        [Test]
        public void ShouldBeRightArraySize()
        {
            var localTime = new Mock<IReadOnlySortedMap<double, byte[]>>();

            IReadOnlyList<byte[]> byteList = new List<byte[]>();
            
            localTime.Setup(l => l.Values).Returns(byteList);

            var ticksFromByteArray = new TicksFromByteArray(localTime.Object);
            Assert.That(ticksFromByteArray, Is.Not.Null);
        }

        [Test]
        public void ShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() => new TicksFromByteArray(null));
        }

        [Test]
        public void ShouldGetRightValueByKey()
        {
            TicksFromByteArray curveWidthTicks = new TicksFromByteArray(curveLocalTime.ToSortedMap());

            long ticks = curveWidthTicks[(double) 1100];

            Assert.That(Equals(20 * ticksPerSecond, ticks));
        }

        [Test]
        public void ShouldGetRightKeyValuePair()
        {
            TicksFromByteArray curveWidthTicks = new TicksFromByteArray(curveLocalTime.ToSortedMap());

            long ticks = curveWidthTicks[(double)1100];

            Assert.That(Equals(20 * ticksPerSecond, ticks));
        }
    }
}
