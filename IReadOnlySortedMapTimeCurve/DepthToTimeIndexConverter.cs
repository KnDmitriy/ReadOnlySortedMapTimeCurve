using Collections;
using IReadOnlySortedMapTimeCurve;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ReadOnlySortedMapTimeCurve
{
    public class DepthToTimeIndexConverter : IDepthToTimeIndexConverter
    {
        private readonly IReadOnlySortedMap<double, byte[]> localTime;
        private readonly double? minDateTimeFromCurveLocalTime = null;

        public DepthToTimeIndexConverter(IReadOnlySortedMap<double, byte[]> localTime)
        {
            this.localTime = localTime ?? throw new ArgumentNullException(nameof(localTime));
            
            //minDateTimeFromCurveLocalTime = DateTimeHelpers.GetMinDateTimeFromLocalTime(localTime);
        }
        
        public IReadOnlySortedMap<double, double> Convert(IReadOnlySortedMap<double, double> source)
        {
            if (source is null)            
                throw new ArgumentNullException(nameof(source));
            
            var result = new PieList<double, double>();
            
            for (var i = 0; i < source.Count; i++)
            {
                double depth = source[i].Key;
                int index = localTime.BinarySearch(depth);

                if (index >= 0)
                {
                    DateTime dateTime = DateTimeHelpers.CreateFromByteArray(localTime[index].Value);
                    
                    //double seconds = DateTimeHelpers.GetDateTimeInSecondsFromBeginingOfCurve(localTime, minDateTime);
                    //result.Insert(seconds, source[i].Value);
                }
                else
                {
                    index = ~index;
                    
                    //DateTime dateTime0 = DateTimeHelpers.CreateFromByteArray(localTime[index - 1].Value);
                    //double seconds0 = dateTime0.Second;
                    //DateTime dateTime1 = DateTimeHelpers.CreateFromByteArray(localTime[index].Value);
                    //double seconds1 = dateTime1.Second;
                    //double interpolatedSeconds = DateTimeHelpers.GetLinearInterpolation(depth, localTime[index - 1].Key,
                    //    localTime[index].Key, seconds0, seconds1);

                    // Будет ли это работать? Ведь localTime - private field
                    //double interpolatedSeconds = DateTimeHelpers.GetLinearInterpolation(localTime, depth, index);

                    //result.Insert(interpolatedSeconds, source[i].Value);
                    
                }
            }            
            return result.ToSortedMap();
        }


        
    }
}
