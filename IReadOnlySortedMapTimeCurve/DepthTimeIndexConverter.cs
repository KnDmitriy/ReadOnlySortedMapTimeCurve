using Collections;
using IReadOnlySortedMapTimeCurve;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ReadOnlySortedMapTimeCurve
{
    public class DepthTimeIndexConverter : IDepthTimeIndexConverter
    {
        private readonly IReadOnlySortedMap<double, byte[]> localTime;

        public DepthTimeIndexConverter(IReadOnlySortedMap<double, byte[]> localTime)
        {
            this.localTime = localTime ?? throw new ArgumentNullException(nameof(localTime));
        }
        // private PieList<double, double> curve = new PieList<double, double>();

        public IReadOnlySortedMap<double, double> Convert(IReadOnlySortedMap<double, double> curveDepthValue)
        {
            var curve = new PieList<double, double>();
            for (var i = 0; i < curveDepthValue.Count; i++)
            {
                double depth = curveDepthValue[i].Key;
                int index = localTime.BinarySearch(depth);
                if (index > 0)
                {
                    DateTime dateTime = DateTimeHelpers.CreateFromByteArray(localTime[index].Value);
                    double seconds = dateTime.Second; // Returns int. Is this accuracy enough? 
                    // Or, is it better to convert ticks to seconds in order to get a more accurate measure?
                    curve.Insert(seconds, curveDepthValue[i].Value);
                    
                }
                else
                {
                    index = ~index;
                    if (index == 0 || index == curveDepthValue.Count - 1)
                    {

                    }
                    else
                    {
                        DateTime dateTime1 = DateTimeHelpers.CreateFromByteArray(localTime[index - 1].Value);
                        double seconds1 = dateTime1.Second;
                        DateTime dateTime2 = DateTimeHelpers.CreateFromByteArray(localTime[index].Value);
                        double seconds2 = dateTime2.Second;
                        double interpolatedSeconds = DepthTimeIndexConverter.GetLinearInterpolation(depth, seconds1, seconds2);
                        curve.Insert(interpolatedSeconds, curveDepthValue[i].Value);
                    }
                }
            }
            var result = curve.ToSortedMap();
            return result;
        }
    }
}
