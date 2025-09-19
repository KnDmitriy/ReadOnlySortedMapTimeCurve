using System;
using Collections;
using TimeReadOnlySortedMap;

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
                    minTicksFromLocalTime = DateTimeHelpers.GetStartOfDayInTicks(depthTicks);
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
            for (var indexForDepthValue = 0; indexForDepthValue < depthValue.Count; indexForDepthValue++)
            {
                double depth = depthValue[indexForDepthValue].Key;
                int index = depthTicks.BinarySearch(depth);

                if (index >= 0)
                {
                    result.Insert((depthTicks[index].Value - minTicksFromLocalTime).ToSeconds(), depthValue[indexForDepthValue].Value);
                }
                else
                {
                    index = ~index; 
                    if (index == 0)
                        result.Insert((depthTicks[0].Value - minTicksFromLocalTime).ToSeconds(), depthValue[0].Value);
                    else if (index == depthValue.Count)
                        result.Insert((depthTicks[depthTicks.Count - 1].Value - minTicksFromLocalTime).ToSeconds(), depthValue[depthValue.Count - 1].Value);
                    else
                    {
                        double interpolatedTicks = MathHelpers.InterpolateLinear(depth, depthTicks[index - 1].Key,
                            depthTicks[index - 1].Value, depthTicks[index].Key, depthTicks[index].Value);
                        result.Insert((interpolatedTicks - minTicksFromLocalTime).ToSeconds(), depthValue[indexForDepthValue].Value);
                    }
                }
            }            
            return result.ToSortedMap();
        }
    }
}
