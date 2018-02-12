using System.Collections.Generic;

namespace PI.src.averaging.kernels
{
    public interface IKernel
    {
        double GetKernel( IList<double> selector, IList<double> comparator, double size );
    }
}
