using PI.src.helpers;
using System.Threading;

namespace PI.src.settings
{
    internal class MainWindowGeneratorSettings
    {
        public int CurvesNo { get; set; } = 125;
        public double StartX { get; set; } = -2.0;
        public double EndX { get; set; } = 2.0;
        public int PointsNo { get; set; } = 200;

        public override string ToString()
        {
            return '[' +
                CurvesNo.ToString( Thread.CurrentThread.CurrentCulture ) + ',' +
                Strings.TryFormatAsNumeric( 4, StartX, Thread.CurrentThread.CurrentCulture ) + ',' +
                Strings.TryFormatAsNumeric( 4, EndX, Thread.CurrentThread.CurrentCulture ) + ',' +
                PointsNo.ToString( Thread.CurrentThread.CurrentCulture ) +
                ']';
        }
    }
}
