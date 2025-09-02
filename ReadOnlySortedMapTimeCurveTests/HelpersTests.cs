using IReadOnlySortedMapTimeCurve;
using NUnit.Framework;
using Collections;
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

        [SetUp]
        public void Setup()
        {
            curve = new PieList<double, double>();
            curve.Insert(1000.1, 1.6);
            curve.Insert(1001.1, 1.3);
            curve.Insert(1002.1, -0.5);
            curve.Insert(1003.1, 3.2);
            curve.Insert(1004.1, 0.2);
        }

        [Test]
        public void ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => DateTimeHelpers.CreateFromByteArray(null));
        }

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

        private PieList<double, double> curve;


        //[Test]
        //public void ShouldGetDataFromCurve()
        //{
        //    // arrange
        //    Setup();

        //    // act
        //    double val = curve.GetValue(10);

        //    // assert
        //    Assert.That(1000, Is.EqualTo((double)val));

        //}


    }
}
