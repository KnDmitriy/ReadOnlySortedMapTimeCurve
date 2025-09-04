using IReadOnlySortedMapTimeCurve;
using NUnit.Framework;
using Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using ReadOnlySortedMapTimeCurve;

namespace ReadOnlySortedMapTimeCurveTests
{
    [TestFixture]
    public class HelpersTests
    {
        private readonly PieList<double, byte[]> curveLocalTime = new PieList<double, byte[]>();
        private readonly PieList<double, double> curveDepthValue = new PieList<double, double>();
        private readonly PieList<double, double> curveSecondsValue = new PieList<double, double>();
        private DateTime dateTimeNow = DateTime.Now;
        [SetUp]
        public void SetupDepthTicks()
        {
            byte[] dateTimeByteArray1 = DateTimeHelpers.CreateFromDateTime(dateTimeNow);
            byte[] dateTimeByteArray2 = DateTimeHelpers.CreateFromDateTime(dateTimeNow.AddMinutes(1));
            byte[] dateTimeByteArray3 = DateTimeHelpers.CreateFromDateTime(dateTimeNow.AddMinutes(2));
            byte[] dateTimeByteArray4 = DateTimeHelpers.CreateFromDateTime(dateTimeNow.AddMinutes(3));
            byte[] dateTimeByteArray5 = DateTimeHelpers.CreateFromDateTime(dateTimeNow.AddMinutes(4));
            curveLocalTime.Insert(1000.1, dateTimeByteArray1);
            curveLocalTime.Insert(1001.1, dateTimeByteArray2);
            curveLocalTime.Insert(1002.1, dateTimeByteArray3);
            curveLocalTime.Insert(1003.1, dateTimeByteArray4);
            curveLocalTime.Insert(1004.1, dateTimeByteArray5);
        }
        [SetUp]
        public void SetupDepthValue()
        {
            curveDepthValue.Insert(1000.1, 1.6);
            curveDepthValue.Insert(1001.1, 1.3);
            curveDepthValue.Insert(1002.1, -0.5);
            curveDepthValue.Insert(1003.1, 3.2);
            curveDepthValue.Insert(1004.1, 0.2);
        }
        [SetUp]
        public void SetupSecondsValue()
        {
            curveSecondsValue.Insert(10000.1, 1.6);
            curveSecondsValue.Insert(10010.1, 1.3);
            curveSecondsValue.Insert(10020.1, -0.5);
            curveSecondsValue.Insert(10030.1, 3.2);
            curveSecondsValue.Insert(10040.1, 0.2);
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




        [Test]
        public void ShouldGetDataFromCurve()
        {
            // arrange
            SetupSecondsValue();

            // act
            double val = curveDepthValue[1].Value;

            // assert
            Assert.That(1.3, Is.EqualTo(val));
        }
        [Test]
        public void ShouldConvertToDateTime1()
        {
            var map = curveLocalTime.ToSortedMap();
            
            // arrange
            var dt = DateTime.Now;
            byte[] bytes = DateTimeHelpers.CreateFromDateTime(dt);            

            // act
            var dateTime = DateTimeHelpers.CreateFromByteArray(bytes);

            // assert
            Assert.That(dt, Is.EqualTo(dateTime));
        }
        [Test]
        public void ShouldGetDateTimeFromBeginningOfDay()
        {
            // arrange
            var dtNow = DateTime.Now;
            var today = DateTime.Today;
            TimeSpan time = dtNow - today;
            double secondsFromBeginningOfDay = time.TotalSeconds; // Can be inaccuracy because of discarding of remainder  

            // act
            //var dateTime = Main.GetTimeInSecondsFromBeginningOfDay(dtNow);

            // assert
            //Assert.That(secondsFromBeginningOfDay, Is.EqualTo(dateTime));
        }
        [Test]
        public void ShouldGetDateTimeFromBeginningOfCurve()
        {
            // arrange
            SetupDepthTicks();

            // act
            //// DateTime rightDateTime = DateTimeHelpers.CreateFromByteArray(curveDepthTicks.First.Value); // упорядочены ли значения времени?
            DateTime dateTime = DateTimeHelpers.GetDateTimeFromBeginningOfCurve(curveLocalTime);

            // assert
            //Assert.That(Is.Equals(dateTime, DateTime.Now));
        }
        [Test]
        public void ShouldGetLinearInterpolation()
        {
            // arrange
            //double x0 = 0.5;
            //double x1 = 1.5;
            //double y0 = 3.1;
            //double y1 = 10.1;
            //double x = 1.5;
            //double unknownY = 10.1;
            SetupDepthTicks();



            // act
            //// DateTime rightDateTime = DateTimeHelpers.CreateFromByteArray(curveDepthTicks.First.Value); // упорядочены ли значения времени?
            
            
            DateTime dateTime = .GetLinearInterpolation(curveLocalTime);

            // assert
            //Assert.That(Is.Equals(dateTime, DateTime.Now));
        }
        


        [Test]
        public void ShouldGetResultSortedMap()
        {
            // arrange
            SetupDepthTicks();
            SetupDepthValue();
            SetupSecondsValue();
            var curveDepthValueSortedMap = curveDepthValue.ToSortedMap();
            var curveDepthTicksSortedMap = curveLocalTime.ToSortedMap();
            // act
            //// DateTime rightDateTime = DateTimeHelpers.CreateFromByteArray(curveDepthTicks.First.Value); // упорядочены ли значения времени?
            DepthTimeIndexConverter converter = new DepthTimeIndexConverter(curveDepthTicksSortedMap);
            var timeIndexCurve = converter.Convert(curveDepthValueSortedMap);

            //for (var i = 0; i < curveDepthValue.Count; i++)
            //{
            //    Console.WriteLine(curveSecondsValue[i].ToString());
            //}

            // assert
            Assert.That(Equals(timeIndexCurve, null));
            Assert.That(Equals(timeIndexCurve.Count, curveSecondsValue.Count));
            for (int i = 0; i < timeIndexCurve.Count; i++)
            {
                Assert.That(Equals(timeIndexCurve[i].Key, curveSecondsValue[i].Key));
                Assert.That(Equals(timeIndexCurve[i].Value, curveSecondsValue[i].Value));
            }
            
        }

        
    }    
}
