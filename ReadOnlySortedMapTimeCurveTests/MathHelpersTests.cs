using Collections;
using IReadOnlySortedMapTimeCurve;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using ReadOnlySortedMapTimeCurve;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace ReadOnlySortedMapTimeCurveTests
{
    [TestFixture]
    public class MathHelpersTests
    {
        [Test]
        public void ShouldGetLinearInterpolation()
        {
            // arrange
            // Краевое значение
            double experiment1X0 = 0.5;
            double experiment1X1 = 1.5;
            double experiment1Y0 = 3.1;
            double experiment1Y1 = 10.1;
            double experiment1X = 1.5;
            double experiment1Y = 10.1;

            // "Средеинное" (некраевое) значение
            double experiment2X0 = 1000;
            double experiment2X1 = 2000;
            double experiment2Y0 = 10000;
            double experiment2Y1 = 20000;
            double experiment2X = 1500;
            double experiment2Y = 15000;

            // "Средеинное" (некраевое) значение
            double experiment3X0 = -2000;
            double experiment3X1 = -1000;
            double experiment3Y0 = -20000;
            double experiment3Y1 = -10000;
            double experiment3X = -1500;
            double experiment3Y = -15000;

            // Искомое значение за пределами отрезка интерполяции. (Есть ли такое понятие, как отрезок интерполяции?)
            double experiment4X0 = 1000;
            double experiment4X1 = 2000;
            double experiment4Y0 = 10000;
            double experiment4Y1 = 20000;
            double experiment4X = 500;


            // act
            double experiment1InterpolatedSeconds = MathHelpers.GetLinearInterpolation(experiment1X, experiment1X0,
                experiment1X1, experiment1Y0, experiment1Y1);
            double experiment2InterpolatedSeconds = MathHelpers.GetLinearInterpolation(experiment2X, experiment2X0, 
                experiment2X1, experiment2Y0, experiment2Y1);
            double experiment3InterpolatedSeconds = MathHelpers.GetLinearInterpolation(experiment3X, experiment3X0,
                experiment3X1, experiment3Y0, experiment3Y1);


            // assert
            ClassicAssert.AreEqual(experiment1Y, experiment1InterpolatedSeconds, 1e-10);
            ClassicAssert.AreEqual(experiment2Y, experiment2InterpolatedSeconds, 1e-10);
            ClassicAssert.AreEqual(experiment3Y, experiment3InterpolatedSeconds, 1e-10);
            //Should it be ArgumentException or ArgumentOutOfRangeException?
            Assert.Throws<ArgumentException>(() => MathHelpers.GetLinearInterpolation(experiment4X, experiment4X0,
                experiment4X1, experiment4Y0, experiment4Y1));
        }
    }
}
