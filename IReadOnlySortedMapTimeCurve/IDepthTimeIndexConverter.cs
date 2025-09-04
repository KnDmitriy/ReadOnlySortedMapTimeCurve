using Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IReadOnlySortedMapTimeCurve
{
    public interface IDepthTimeIndexConverter
    {
        IReadOnlySortedMap<double, double> Convert(IReadOnlySortedMap<double, double> source);
    }
}
