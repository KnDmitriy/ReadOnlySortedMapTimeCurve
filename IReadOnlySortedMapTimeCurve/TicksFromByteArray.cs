using System;
using System.Collections;
using System.Collections.Generic;
using Collections;

namespace IReadOnlySortedMapTimeCurve
{
    public class TicksFromByteArray : IReadOnlySortedMap<double, long>
    {
        private class TicksList : IReadOnlyList<long>
        {
            private readonly IReadOnlyList<byte[]> source;

            public TicksList(IReadOnlyList<byte[]> source)
            {
                if (source is  null) 
                    throw new ArgumentNullException(nameof(source));
                for (int i = 0; i < source.Count; i++)
                {
                    if (source[i].Length != 8)
                        throw new ArgumentException(
                            $"The byte array in {nameof(source)} for element with index {i} has an incorrect size.");
                }
                this.source = source;
            }

            public long this[int index]
            {
                get
                {
                    var array = source[index];                    
                    var ticks = BitConverter.ToInt64(array, 0);
                    return ticks;
                }
            }

            public int Count => source.Count;

            public IEnumerator<long> GetEnumerator()
            {
                for (var i = 0; i < Count; i++)
                    yield return this[i];
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();            
        }

        private readonly IReadOnlySortedMap<double, byte[]> localTime;
        private readonly IReadOnlyList<long> ticksList;

        public TicksFromByteArray(IReadOnlySortedMap<double, byte[]> localTime)
        {
            this.localTime = localTime ?? throw new ArgumentNullException(nameof(localTime));
            ticksList = new TicksList(localTime.Values);
        }

        public long this[double key]
        {
            get 
            {
                var array = localTime[key];
                return BitConverter.ToInt64(array, 0);
            }
        }

        public KeyValuePair<double, long> this[int index]
        {
            get
            {
                var key = localTime[index].Key;                
                return new KeyValuePair<double, long>(key, ticksList[index]);
            }
        }

        public IReadOnlyList<double> Keys => localTime.Keys;

        public IReadOnlyList<long> Values => ticksList;

        public int Count => localTime.Count;

        public int BinarySearch(double key) => localTime.BinarySearch(key);
        
        public bool ContainsKey(double key) => localTime.ContainsKey(key);
        

        public IEnumerator<KeyValuePair<double, long>> GetEnumerator()
        {
            for (var i = 0; i < Count; i++)
            {
                yield return this[i];
            }
        }

        public bool TryGetValue(double key, out long value)
        {
            value = default;

            if (!ContainsKey(key))
                return false;

            value = this[key];
            return true;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
