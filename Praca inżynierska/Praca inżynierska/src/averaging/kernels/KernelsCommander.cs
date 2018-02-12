using PI.src.enumerators;
using System.Collections.Generic;

namespace PI.src.averaging.kernels
{
    public class KernelsCommander
    {
        private IKernel kernel;

        public KernelsCommander( KernelType kernel )
        {
            SetStrategy( kernel );
        }

        public void SetStrategy( KernelType kernel )
        {
            switch ( kernel ) {
            case KernelType.Boxcar:
                this.kernel = new BoxcarKernel();
                break;
            case KernelType.Epanechnikov:
                this.kernel = new EpanechnikovKernel();
                break;
            case KernelType.Gaussian:
                this.kernel = new GaussianKernel();
                break;
            }
        }

        public double Execute( IList<double> selector, IList<double> comparator, double size )
        {
            return kernel.GetKernel( selector, comparator, size );
        }
    }
}
