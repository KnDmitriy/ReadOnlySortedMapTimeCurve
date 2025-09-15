using System;
using ReadOnlySortedMapTimeCurve;


namespace IReadOnlySortedMapTimeCurve
{
    public static class MathHelpers
    {
        public static double InterpolateLinear(double x, double x0, double y0, double x1, double y1)
        {
            if (x < x0 || x > x1)
                throw new ArgumentOutOfRangeException(nameof(x), "The point have to be between interpolation nodes.");
            return y0 + (x - x0) * (y1 - y0) / (x1 - x0);
        }
        //public static double GetLinearInterpolation(IReadOnlySortedMap<double, byte[]> localTime, double depth, int indexOfNextItem)
        //{
        //    if (depth < 0)
        //        throw new ArgumentOutOfRangeException("The depth can't be less than zero.");

        //    DateTime dateTime0 = CreateFromByteArray(localTime[indexOfNextItem - 1].Value);
        //    double seconds0 = dateTime0.Second;  // TODO: заменить!!!
        //    DateTime dateTime1 = CreateFromByteArray(localTime[indexOfNextItem].Value);
        //    double seconds1 = dateTime1.Second;  // TODO: заменить!!!
        //    if (seconds0 < 0 || seconds1 < 0)
        //        throw new ArgumentOutOfRangeException("Time can't be negative.");
        //    double depth0 = localTime[indexOfNextItem - 1].Key;
        //    double depth1 = localTime[indexOfNextItem].Key;

        //    double interpolatedSeconds = seconds0 + (depth - depth0) * (seconds1 - seconds0) / (depth1 - depth0);
        //    return interpolatedSeconds;
        //}
    }
}