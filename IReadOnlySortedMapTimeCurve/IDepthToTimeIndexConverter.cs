using Collections;

namespace TimeReadOnlySortedMap
{
    public interface IDepthToTimeIndexConverter
    {
        IReadOnlySortedMap<double, double> Convert(IReadOnlySortedMap<double, double> valuesByDepthMap);
    }
}
