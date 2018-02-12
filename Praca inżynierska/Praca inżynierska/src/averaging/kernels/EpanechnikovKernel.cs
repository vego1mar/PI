using PI.src.general;
using System.Collections.Generic;

namespace PI.src.averaging.kernels
{
    public class EpanechnikovKernel : IKernel
    {
        public double GetKernel( IList<double> selector, IList<double> comparator, double size )
        {
            double t = Mathematics.GetKernelDistanceArgument( selector, comparator, size );
            return (t < 1.0) ? ((3.0 / 4.0) * (1.0 - (t * t))) : 0.0;
        }
    }
}
