using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Collections;

namespace IReadOnlySortedMapTimeCurve
{
    public interface IDepthToTimeIndexConverter
    {
        IReadOnlySortedMap<double, double> Convert(IReadOnlySortedMap<double, double> source);
    }
}
