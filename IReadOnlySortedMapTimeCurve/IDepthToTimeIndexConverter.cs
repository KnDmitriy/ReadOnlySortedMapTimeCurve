using Collections;

namespace TimeReadOnlySortedMap
{
    public interface IDepthToTimeIndexConverter
    {
        IReadOnlySortedMap<double, double> Convert<T>(IReadOnlySortedMap<double, double> valuesByDepthMap);
    }
}
