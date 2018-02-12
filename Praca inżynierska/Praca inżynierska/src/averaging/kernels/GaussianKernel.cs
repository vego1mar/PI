using PI.src.general;
using System;
using System.Collections.Generic;

namespace PI.src.averaging.kernels
{
    public class GaussianKernel : IKernel
    {
        public double GetKernel( IList<double> selector, IList<double> comparator, double size )
        {
            double t = Mathematics.GetKernelDistanceArgument( selector, comparator, size );
            return (1.0 / Math.Sqrt( 2.0 * Math.PI )) * Math.Exp( (-t * t) / 2.0 );
        }
    }
}
