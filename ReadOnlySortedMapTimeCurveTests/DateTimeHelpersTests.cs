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
    public class DateTimeHelpersTests
    {
        private readonly PieList<double, byte[]> curveLocalTime = new PieList<double, byte[]>();
        private readonly PieList<double, double> curveDepthValue = new PieList<double, double>();
        private readonly PieList<double, double> curveSecondsValue = new PieList<double, double>();
        private static readonly DateTime dateTimeReadonly = new DateTime(2025, 9, 1, 8, 30, 30, 300);
        private DateTime dateTimeForTests = dateTimeReadonly;
        private DateTime dateTimeNow = DateTime.Now;
        private const int coefficientForConvertingTicksToSeconds = 10000000; // 10^7
        //[SetUp]
        //public void SetupLocalTimeIncreasing()
        //{
        //    curveLocalTime.Clear();
        //    byte[] dateTimeByteArray1 = DateTimeHelpers.CreateFromDateTime(dateTimeForTests);
        //    byte[] dateTimeByteArray2 = DateTimeHelpers.CreateFromDateTime(dateTimeForTests.AddMinutes(1));
        //    byte[] dateTimeByteArray3 = DateTimeHelpers.CreateFromDateTime(dateTimeForTests.AddMinutes(2));
        //    byte[] dateTimeByteArray4 = DateTimeHelpers.CreateFromDateTime(dateTimeForTests.AddMinutes(3));
        //    byte[] dateTimeByteArray5 = DateTimeHelpers.CreateFromDateTime(dateTimeForTests.AddMinutes(4));
        //    curveLocalTime.Insert(1000.1, dateTimeByteArray1);
        //    curveLocalTime.Insert(1001.1, dateTimeByteArray2);
        //    curveLocalTime.Insert(1002.1, dateTimeByteArray3);
        //    curveLocalTime.Insert(1003.1, dateTimeByteArray4);
        //    curveLocalTime.Insert(1004.1, dateTimeByteArray5);
        //}
        [SetUp]
        public void SetupLocalTimeIncreasing()
        {
            curveLocalTime.Clear();
            byte[] dateTimeByteArray1 = DateTimeHelpers.CreateFromDateTime(new DateTime(
                10 * coefficientForConvertingTicksToSeconds));
            byte[] dateTimeByteArray2 = DateTimeHelpers.CreateFromDateTime(new DateTime(
                20 * coefficientForConvertingTicksToSeconds));
            byte[] dateTimeByteArray3 = DateTimeHelpers.CreateFromDateTime(new DateTime(
                30 * coefficientForConvertingTicksToSeconds));
            byte[] dateTimeByteArray4 = DateTimeHelpers.CreateFromDateTime(new DateTime(
                40 * coefficientForConvertingTicksToSeconds));
            byte[] dateTimeByteArray5 = DateTimeHelpers.CreateFromDateTime(new DateTime(
                50 * coefficientForConvertingTicksToSeconds));
            curveLocalTime.Insert(1000.1, dateTimeByteArray1);
            curveLocalTime.Insert(1001.1, dateTimeByteArray2);
            curveLocalTime.Insert(1002.1, dateTimeByteArray3);
            curveLocalTime.Insert(1003.1, dateTimeByteArray4);
            curveLocalTime.Insert(1004.1, dateTimeByteArray5);
        }
        [SetUp]
        public void SetupLocalTimeDecreasing()
        {
            curveLocalTime.Clear();
            byte[] dateTimeByteArray1 = DateTimeHelpers.CreateFromDateTime(dateTimeForTests.AddMinutes(4));
            byte[] dateTimeByteArray2 = DateTimeHelpers.CreateFromDateTime(dateTimeForTests.AddMinutes(3));
            byte[] dateTimeByteArray3 = DateTimeHelpers.CreateFromDateTime(dateTimeForTests.AddMinutes(3));
            byte[] dateTimeByteArray4 = DateTimeHelpers.CreateFromDateTime(dateTimeForTests.AddMinutes(1));
            byte[] dateTimeByteArray5 = DateTimeHelpers.CreateFromDateTime(dateTimeForTests);
            curveLocalTime.Insert(1000.1, dateTimeByteArray1);
            curveLocalTime.Insert(1001.1, dateTimeByteArray2);
            curveLocalTime.Insert(1002.1, dateTimeByteArray3);
            curveLocalTime.Insert(1003.1, dateTimeByteArray4);
            curveLocalTime.Insert(1004.1, dateTimeByteArray5);
        }

        [SetUp]
        public void SetupDepthValue()
        {
            curveDepthValue.Clear();
            curveDepthValue.Insert(1000.1, 1.6);
            curveDepthValue.Insert(1001.1, 1.3);
            curveDepthValue.Insert(1002.1, -0.5);
            curveDepthValue.Insert(1003.1, 3.2);
            curveDepthValue.Insert(1004.1, 0.2);
        }
        [SetUp]
        public void SetupSecondsValue()
        {
            curveSecondsValue.Clear();
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
        public void ShouldGetTimeInSecondsFromBeginningOfDay()
        {
            // arrange
            var dtNow = DateTime.Now;
            TimeSpan time = dtNow.TimeOfDay;
            
            double secondsFromBeginningOfDay = time.Ticks * coefficientForConvertingTicksToSeconds;

            // act
            var dateTime = DateTimeHelpers.GetTimeInSecondsFromBeginningOfDay(dtNow);

            // assert
            Assert.That(dtNow, Is.EqualTo(dateTime));
        }
        [Test]
        public void ShouldGetDateTimeFromBeginningOfCurve()
        {
            // arrange
            SetupLocalTimeIncreasing();

            // act
            //// DateTime rightDateTime = DateTimeHelpers.CreateFromByteArray(curveDepthTicks.First.Value);
            DateTime dateTime = DateTimeHelpers.GetDateTimeFromBeginningOfCurve(curveLocalTime);

            // assert
            //Assert.That(Is.Equals(dateTime, DateTime.Now));
        }
        
        


        [Test]
        public void ShouldGetResultSortedMap()
        {
            // arrange
            SetupLocalTimeIncreasing();
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
