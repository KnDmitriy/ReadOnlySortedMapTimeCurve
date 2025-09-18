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
        private readonly TicksFromByteArray depthTicks;
        private readonly long minTicksFromLocalTime;

        public DepthToTimeIndexConverter(IReadOnlySortedMap<double, byte[]> localTime, TypeOfTimeCalculation type)
        {
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
        
        public IReadOnlySortedMap<double, double> Convert(IReadOnlySortedMap<double, double> depthValue)
        {
            if (depthValue is null)            
                throw new ArgumentNullException(nameof(depthValue));
            
            var result = new PieList<double, double>();
            
            for (var i = 0; i < depthValue.Count; i++)
            {
                double depth = depthValue[i].Key;
                int index = depthTicks.BinarySearch(depth);

                if (index >= 0)
                {
                    result.Insert(ToSeconds(depthTicks[index].Value - minTicksFromLocalTime), depthValue[i].Value);
                }
                else
                {
                    index = ~index; // Добавить проверки на index == 0 и index == depthTicksCurve.Count
                    if (index == 0)
                    {
                        // result.Insert();
                    }
                    double interpolatedTicks = MathHelpers.InterpolateLinear(depth, depthTicks[index - 1].Key,
                        depthTicks[index - 1].Value, depthTicks[index].Key, depthTicks[index].Value);
                    result.Insert(interpolatedTicks - minTicksFromLocalTime, );// Разобраться!!!
                }
            }            
            return result.ToSortedMap();
        }

        private static double ToSeconds(long ticks)
            => ticks / 10_000_000d;
        
    }
}
