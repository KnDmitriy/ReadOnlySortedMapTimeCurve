using System;
using ReadOnlySortedMapTimeCurve;


namespace TimeReadOnlySortedMap
{
    public static class MathHelpers
    {
        public static double InterpolateLinear(double x, double x0, double y0, double x1, double y1)
        {
            if (x < x0 || x > x1)
                throw new ArgumentOutOfRangeException(nameof(x), "The point have to be between interpolation nodes.");
            return y0 + (x - x0) * (y1 - y0) / (x1 - x0);
        }
    }
}