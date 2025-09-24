using System;
using Collections;
using ReadOnlySortedMapTimeCurve;

namespace TimeReadOnlySortedMap
{
    public class DepthToTimeIndexConverter : IDepthToTimeIndexConverter
    {

        private readonly ByteArrayWrapper depthTicks;
        private readonly long minTicksFromLocalTime;

        public DepthToTimeIndexConverter(IReadOnlySortedMap<double, byte[]> localTime, TypeOfTimeCalculation type)
        {
            //private ByteArrayWrapper byteArrayWrapper = new ByteArrayWrapper(localTime, (a) => BitConverter.ToInt64(a, 0));
            depthTicks = new ByteArrayWrapper(localTime) ?? throw new ArgumentNullException(nameof(localTime));
            switch (type)
            {
                case TypeOfTimeCalculation.LocalTimeMin:
                    minTicksFromLocalTime = DateTimeHelpers.GetMinTicksFromLocalTime(depthTicks);
                    break;
                case TypeOfTimeCalculation.StartOfDay:
                    minTicksFromLocalTime = DateTimeHelpers.GetStartOfDayInTicks(depthTicks);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type));
            }
        }
        
        public IReadOnlySortedMap<double, double> Convert(IReadOnlySortedMap<double, double> valuesByDepth)
        {
            if (valuesByDepth is null)            
                throw new ArgumentNullException(nameof(valuesByDepth));
            
            var result = new PieList<double, double>();

            for (var i = 0; i < valuesByDepth.Count; i++)
            {
                double depth = valuesByDepth[i].Key;
                int foundIndex = depthTicks.BinarySearch(depth);

                double value = valuesByDepth[i].Value;
                
                double ticks;

                
                if (foundIndex >= 0)
                {// Значение глубины из valuesByDepth найдено в depthTicks
                    ticks = depthTicks[foundIndex].Value;                    
                }
                else
                {
                    foundIndex = ~foundIndex;
                    if (foundIndex == 0)
                    {// Значение глубины из valuesByDepth меньше минимальной глубины из depthTicks
                        ticks = depthTicks[0].Value;                        
                    }
                    else if (foundIndex == depthTicks.Count)
                    {// Значение глубины из valuesByDepth больше максимальной глубины из depthTicks
                        ticks = depthTicks[depthTicks.Count - 1].Value;                        
                    }
                    else
                    {// Значение глубины из valuesByDepth не выходит за пределы значений глубин из depthTicks
                        var p0 = depthTicks[foundIndex - 1];
                        var p1 = depthTicks[foundIndex];
                        ticks = MathHelpers.InterpolateLinear(depth, p0.Key, p0.Value, p1.Key, p1.Value);                        
                    }
                }
                result.Insert((ticks - minTicksFromLocalTime).ToSeconds(), value);
            }
            return result.ToSortedMap();
        }
    }
}
