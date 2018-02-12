using PI.src.general;
using System.Collections.Generic;

namespace PI.src.averaging.kernels
{
    public class BoxcarKernel : IKernel
    {
        public double GetKernel( IList<double> selector, IList<double> comparator, double size )
        {
            double t = Mathematics.GetKernelDistanceArgument( selector, comparator, size );
            return (t < 1) ? (1.0 / 2.0) : 0.0;
        }
    }
}
