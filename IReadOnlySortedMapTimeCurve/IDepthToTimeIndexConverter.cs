using Collections;

namespace TimeReadOnlySortedMap
{
    public interface IDepthToTimeIndexConverter
    {
        IReadOnlySortedMap<double, T> Convert<T>(IReadOnlySortedMap<double, T> valuesByDepthMap);
    }
}
