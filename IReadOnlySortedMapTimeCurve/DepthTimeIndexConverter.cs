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
    public class DepthTimeIndexConverter : IDepthTimeIndexConverter
    {
        private readonly IReadOnlySortedMap<double, byte[]> localTime;
        private double minTimeInLocalTime;
        public DepthTimeIndexConverter(IReadOnlySortedMap<double, byte[]> localTime)
        {
            this.localTime = localTime ?? throw new ArgumentNullException(nameof(localTime));
        }        
        public IReadOnlySortedMap<double, double> Convert(IReadOnlySortedMap<double, double> source)
        {
            if (source is null)            
                throw new ArgumentNullException(nameof(source));
            
            var result = new PieList<double, double>();
                                    
            for (var i = 0; i < source.Count - 1; i++)
            {
                double depth = source[i].Key;
                int index = localTime.BinarySearch(depth);

                if (index > 0)
                {
                    if (index == 0 || index == source.Count - 1)
                    {

                    }
                    else
                    {
                        var dateTime = DateTimeHelpers.CreateFromByteArray(localTime[index].Value);
                        double seconds = dateTime.Second; // Returns int. Is this accuracy enough? 
                                // Or, is it better to convert ticks to seconds in order to get a more accurate result?
                        result.Insert(seconds, source[i].Value);
                    }
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
                    double interpolatedSeconds = DateTimeHelpers.GetLinearInterpolation(localTime, depth, index);

                    result.Insert(interpolatedSeconds, source[i].Value);
                    
                }
            }            
            return result.ToSortedMap();
        }


        
    }
}
