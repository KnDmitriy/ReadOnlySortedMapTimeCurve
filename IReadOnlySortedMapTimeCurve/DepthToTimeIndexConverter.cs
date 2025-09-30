using System;
using Collections;
using Format;

namespace TimeReadOnlySortedMap
{
    public class DepthToTimeIndexConverter : IDepthToTimeIndexConverter
    {
        private readonly ByteArrayWrapper ticksByDepthMap;
        private readonly long minTicksFromLocalTime;

        public DepthToTimeIndexConverter(IReadOnlySortedMap<double, byte[]> localTimeMap, TimeOrigin type)
        {
            ticksByDepthMap = new ByteArrayWrapper(localTimeMap) ?? throw new ArgumentNullException(nameof(localTimeMap));
            switch (type)
            {
                case TimeOrigin.StartTime:
                    minTicksFromLocalTime = DateTimeHelpers.GetMinTicks(ticksByDepthMap);
                    break;
                case TimeOrigin.StartOfDay:
                    minTicksFromLocalTime = DateTimeHelpers.GetStartOfDayFromTicks(ticksByDepthMap);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type));
            }
        }
        
        public IReadOnlySortedMap<double, T> Convert<T>(IReadOnlySortedMap<double, T> valuesByDepthMap)
        {
            if (valuesByDepthMap is null)            
                throw new ArgumentNullException(nameof(valuesByDepthMap));
            
            var result = new PieList<double, T>();

            for (var i = 0; i < valuesByDepthMap.Count; i++)
            {
                double depth = valuesByDepthMap[i].Key;
                T value = valuesByDepthMap[i].Value;
                int foundIndex = ticksByDepthMap.BinarySearch(depth);
                
                double ticks = GetTicksByBinarySearchIndex(foundIndex, depth);
                double key = (ticks - minTicksFromLocalTime).ToSeconds();
                result.Insert(key, value);
            }
            return result.ToSortedMap();
        }

        public IReadOnlySortedMap<double, RecordWaveValue> Convert(IReadOnlySortedMap<double, RecordWaveValue> valuesByDepthMap)
        {
            if (valuesByDepthMap is null)
                throw new ArgumentNullException(nameof(valuesByDepthMap));

            var result = new PieList<double, RecordWaveValue>();

            for (var i = 0; i < valuesByDepthMap.Count; i++)
            {
                double depth = valuesByDepthMap[i].Key;
                RecordWaveValue value = valuesByDepthMap[i].Value;
                int foundIndex = ticksByDepthMap.BinarySearch(depth);

                double ticks = GetTicksByBinarySearchIndex(foundIndex, depth);
                double key = (ticks - minTicksFromLocalTime).ToSeconds();

                var newValue = new RecordWaveValue(key)
                {
                    Delay = value.Delay,
                    Step = value.Step,
                    Values = value.Values,
                };
                value = newValue;
                
                result.Insert(key, value);
            }
            return result.ToSortedMap();
        }

        private double GetTicksByBinarySearchIndex(int foundIndex, double depth)
        {
            double ticks;
            if (foundIndex >= 0) // Значение глубины найдено
            {
                ticks = ticksByDepthMap[foundIndex].Value;
            }
            else // Значение глубины не найдено, определяем корректное значение
            {
                foundIndex = ~foundIndex;
                if (foundIndex == 0) // Значение глубины меньше минимальной глубины
                {
                    ticks = ticksByDepthMap[0].Value;
                }
                else if (foundIndex == ticksByDepthMap.Count) // Значение глубины больше максимальной глубины
                {
                    ticks = ticksByDepthMap[ticksByDepthMap.Count - 1].Value;
                }
                else // Значение глубины не выходит за пределы значений глубин
                {
                    var p0 = ticksByDepthMap[foundIndex - 1];
                    var p1 = ticksByDepthMap[foundIndex];
                    ticks = MathHelpers.InterpolateLinear(depth, p0.Key, p0.Value, p1.Key, p1.Value);
                }
            }
            return ticks;
        }
    }
}
