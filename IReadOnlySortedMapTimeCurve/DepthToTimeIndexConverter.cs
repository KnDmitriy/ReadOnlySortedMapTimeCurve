using System;
using Collections;
using IReadOnlySortedMapTimeCurve;
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
        //private readonly IReadOnlySortedMap<double, byte[]> localTime;
        private readonly TicksFromByteArray depthTicks;
        private readonly long minTicksFromLocalTime;

        public DepthToTimeIndexConverter(IReadOnlySortedMap<double, byte[]> localTime, TypeOfTimeCalculation type)
        {
            //this.localTime = localTime ?? throw new ArgumentNullException(nameof(localTime));
            depthTicks = new TicksFromByteArray(localTime) ?? throw new ArgumentNullException(nameof(localTime));
            switch (type)
            {
                case TypeOfTimeCalculation.LocalTimeMin:
                    minTicksFromLocalTime = DateTimeHelpers.GetMinTicksFromLocalTime(depthTicks);
                    break;
                case TypeOfTimeCalculation.StartOfDay:
                    minTicksFromLocalTime = DateTimeHelpers.GetMinTicksFromStartOfDay(depthTicks);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type));
            }
        }
        
        public IReadOnlySortedMap<double, double> Convert(IReadOnlySortedMap<double, double> source)
        {
            if (source is null)            
                throw new ArgumentNullException(nameof(source));
            
            var result = new PieList<double, double>();
            
            for (var i = 0; i < source.Count; i++)
            {
                double depth = source[i].Key;
                int index = depthTicks.BinarySearch(depth);

                if (index >= 0)
                {
                    result.Insert(ToSeconds(depthTicks[index].Value - minTicksFromLocalTime), source[i].Value);
                }
                else
                {
                    index = ~index; // Добавить проверки на index == 0 и index == depthTicksCurve.Count
                    double interpolatedTicks = MathHelpers.InterpolateLinear(depth, depthTicks[index - 1].Key,
                        depthTicks[index - 1].Value, depthTicks[index].Key, depthTicks[index].Value);
                    result.Insert(depth, interpolatedTicks - (double)minTicksFromLocalTime);// Разобраться!!!
                }
            }            
            return result.ToSortedMap();
        }

        private static double ToSeconds(long ticks)
            => ticks / 10_000_000d;
        
    }
}
