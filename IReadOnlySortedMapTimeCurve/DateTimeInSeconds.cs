using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Collections;
using ReadOnlySortedMapTimeCurve;

namespace IReadOnlySortedMapTimeCurve
{
    // НЕ ИСПОЛЬЗУЕТСЯ
    public class DateTimeInSeconds : IReadOnlySortedMap<double, double>
    {
        private readonly IReadOnlySortedMap<double, byte[]> source;
        private readonly double minSeconds;

        public DateTimeInSeconds(IReadOnlySortedMap<double, byte[]> source, TypeOfTimeCalculation typeOfTimeCalculation)
        {
            this.source = source ?? throw new ArgumentNullException(nameof(source));
            minSeconds = GetMinSeconds(source, typeOfTimeCalculation);
        }

        private static double GetMinSeconds(IReadOnlySortedMap<double, byte[]> source, TypeOfTimeCalculation typeOfTimeCalculation)
        {
            switch (typeOfTimeCalculation)
            {
                case TypeOfTimeCalculation.LocalTimeMinimum:
                    return GetMinSecondsByLocalTimeMin(source);
                case TypeOfTimeCalculation.StartOfDay:
                    return GetMinSecondsByStartOfDay(source);
                default:
                    throw new NotImplementedException();
            }
        }

        private static double GetMinSecondsByStartOfDay(IReadOnlySortedMap<double, byte[]> source)
        {
            throw new NotImplementedException();
        }

        private static double GetMinSecondsByLocalTimeMin(IReadOnlySortedMap<double, byte[]> source)
        {
            throw new NotImplementedException();
        }

        public double this[double key] => throw new NotImplementedException();

        public KeyValuePair<double, double> this[int Index] => throw new NotImplementedException();

        public IReadOnlyList<double> Keys => source.Keys;

        private List<double> values;
        public IReadOnlyList<double> Values => values;

        public int Count => source.Count;

        public int BinarySearch(double key) => source.BinarySearch(key);
        
        public bool ContainsKey(double key) => source.ContainsKey(key);
        
        public IEnumerator<KeyValuePair<double, double>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(double key, out double value)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
