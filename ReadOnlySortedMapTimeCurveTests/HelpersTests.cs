using IReadOnlySortedMapTimeCurve;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ReadOnlySortedMapTimeCurveTests
{
    [TestFixture]
    public class HelpersTests
    {
        [Test]
        public void ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => DateTimeHelpers.CreateFromByteArray(null));
        }

        //[Test]
        //public void ShouldBeRightArraySize()
        //{
        //    // arrange
        //    var dt = DateTime.Now;

        //    // act
        //    var bytes = DateTimeHelpers.CreateFromDateTime(dt);

        //    // assert 
        //    Assert.That(8, Is.EqualTo(bytes.Length));
        //}

        [Test]
        public void ShouldBeRightArraySize()
        {
            // arrange
            byte[] byteArray = new byte[4];

            // act

            // assert 
            Assert.Throws<ArgumentException> (() => DateTimeHelpers.CreateFromByteArray(byteArray), "Byte array is wrong");
        }

        [Test]
        public void ShouldConvertToDateTime()
        {
            // arrange
            var dt = DateTime.Now;
            var bytes = DateTimeHelpers.CreateFromDateTime(dt);

            // act
            var dateTime = DateTimeHelpers.CreateFromByteArray(bytes);

            // assert
            Assert.That(dt, Is.EqualTo(dateTime));
        }        
    }
}
