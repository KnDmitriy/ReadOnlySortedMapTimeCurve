using Collections;
using Format;

namespace TimeReadOnlySortedMap
{
    public interface IDepthToTimeIndexConverter
    {
        IReadOnlySortedMap<double, T> Convert<T>(IReadOnlySortedMap<double, T> valuesByDepthMap);
        IReadOnlySortedMap<double, RecordWaveValue> Convert(IReadOnlySortedMap<double, RecordWaveValue> valuesByDepthMap);
    }
}
