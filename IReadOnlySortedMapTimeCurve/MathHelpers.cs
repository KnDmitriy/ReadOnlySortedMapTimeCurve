using ReadOnlySortedMapTimeCurve;
using System;

namespace IReadOnlySortedMapTimeCurve
{
    public static class MathHelpers
    {

        public static double GetLinearInterpolation(double x, double x0, double x1, double y0, double y1)
        {
            if (x < x0 || x > x1)
                throw new ArgumentException("Point argument have to be between interpolation nodes.");
            return y0 + (x - x0) * (y1 - y0) / (x1 - x0);
        }
    }
}