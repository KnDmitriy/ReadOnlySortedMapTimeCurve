using System;
using IReadOnlySortedMapTimeCurve;
using NUnit.Framework;
using NUnit.Framework.Legacy;


namespace ReadOnlySortedMapTimeCurveTests
{
    [TestFixture]
    public class MathHelpersTests
    {
        [Test]
        public void ShouldInterpolateLinear()
        {
            // "Срединное" (некраевое) значение
            double experiment1X0 = 1000;
            double experiment1Y0 = 10000;
            double experiment1X1 = 2000;
            double experiment1Y1 = 20000;
            double experiment1X = 1500;
            double experiment1Y = 15000;

            // "Срединное" (некраевое) значение
            double experiment2X0 = -2000;
            double experiment2Y0 = -20000;
            double experiment2X1 = -1000;
            double experiment2Y1 = -10000;
            double experiment2X = -1500;
            double experiment2Y = -15000;

            double experiment3InterpolatedSeconds = MathHelpers.InterpolateLinear(experiment1X, experiment1X0,
                experiment1Y0, experiment1X1, experiment1Y1);
            double experiment2InterpolatedSeconds = MathHelpers.InterpolateLinear(experiment2X, experiment2X0,
                experiment2Y0, experiment2X1, experiment2Y1);

            ClassicAssert.AreEqual(experiment1Y, experiment3InterpolatedSeconds, 1e-10);
            ClassicAssert.AreEqual(experiment2Y, experiment2InterpolatedSeconds, 1e-10);
        }

        [Test]
        public void ShouldInterpolateLinearBoundaryValues()
        {
            // Краевое значение
            double experiment1X0 = 0.5;
            double experiment1Y0 = 3.1;
            double experiment1X1 = 1.5;
            double experiment1Y1 = 10.1;
            double experiment1X = 1.5;
            double experiment1Y = 10.1;

            double experiment2X0 = 0.5;
            double experiment2Y0 = 3.1;
            double experiment2X1 = 1.5;
            double experiment2Y1 = 10.1;
            double experiment2X = 0.5;
            double experiment2Y = 3.1;

            double experiment1InterpolatedSeconds = MathHelpers.InterpolateLinear(experiment1X, experiment1X0,
               experiment1Y0, experiment1X1, experiment1Y1);
            double experiment2InterpolatedSeconds = MathHelpers.InterpolateLinear(experiment2X, experiment2X0,
               experiment2Y0, experiment2X1, experiment2Y1);

            ClassicAssert.AreEqual(experiment1Y, experiment1InterpolatedSeconds, 1e-10);
            ClassicAssert.AreEqual(experiment2Y, experiment2InterpolatedSeconds, 1e-10);
        }

        [Test]
        public void ShouldThrowOutOfRangeExceptionDuringLinearInterpolation()
        {
            //var pointX = new Processing.Common.Points.PointD
            // arrange
            // Искомое значение за пределами отрезка интерполяции. (Есть ли такое понятие, как отрезок интерполяции?)
            double experiment1X0 = 1000;
            double experiment1Y0 = 10000;
            double experiment1X1 = 2000;
            double experiment1Y1 = 20000;
            double experiment1X = 500;

            // act

            // assert
            Assert.Throws<ArgumentOutOfRangeException>(() => MathHelpers.InterpolateLinear(experiment1X, experiment1X0,
                experiment1Y0, experiment1X1, experiment1Y1));
        }
    }
}
