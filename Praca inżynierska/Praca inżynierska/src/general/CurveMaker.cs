using PI.src.settings;
using System;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;

namespace PI.src.general
{
    public static class CurveMaker
    {
        public static IList<DataPoint> OfPolynomial( ArgumentsMaker args, PolynomialParameters @params )
        {
            IList<DataPoint> points = new List<DataPoint>();
            double x;
            double y;

            while ( args.HasNextArgument() ) {
                x = args.GetNextArgument();
                y = ((@params.A * Math.Pow( x, @params.B )) / @params.C) + ((@params.D * Math.Pow( x, @params.E )) / @params.F) + @params.I;
                points.Add( new DataPoint( x, y ) );
            }

            return points;
        }

        public static IList<DataPoint> OfHyperbolic( ArgumentsMaker args, HyperbolicParameters @params )
        {
            IList<DataPoint> points = new List<DataPoint>();
            double x;
            double y;

            while ( args.HasNextArgument() ) {
                x = args.GetNextArgument();
                y = (((@params.A * Math.Pow( Math.E, @params.B * x )) - (@params.C * Math.Pow( Math.E, @params.D * (-x) ))) / @params.F) + @params.I;
                points.Add( new DataPoint( x, y ) );
            }

            return points;
        }

        public static IList<DataPoint> OfSineWave( ArgumentsMaker args, WaveformParameters @params )
        {
            IList<DataPoint> points = new List<DataPoint>();
            double x;
            double y;

            while ( args.HasNextArgument() ) {
                x = args.GetNextArgument();
                y = (@params.M * Math.Sin( (@params.N * x) + @params.O )) + @params.K;
                points.Add( new DataPoint( x, y ) );
            }

            return points;
        }

        public static IList<DataPoint> OfSquareWave( ArgumentsMaker args, WaveformParameters @params )
        {
            IList<DataPoint> points = new List<DataPoint>();
            double x;
            double arg;
            double y;

            while ( args.HasNextArgument() ) {
                x = args.GetNextArgument();
                arg = (2.0 * Math.PI * x) / @params.N;
                y = (Math.Abs( Math.Sin( arg ) ) * (@params.M * (1.0 / Math.Sin( arg )))) + @params.K;
                points.Add( new DataPoint( x, y ) );
            }

            return points;
        }

        public static IList<DataPoint> OfTriangleWave( ArgumentsMaker args, WaveformParameters @params )
        {
            IList<DataPoint> points = new List<DataPoint>();
            double x;
            double y;

            while ( args.HasNextArgument() ) {
                x = args.GetNextArgument();
                y = (((2.0 * @params.M) / Math.PI) * Math.Asin( Math.Sin( (2.0 * Math.PI * x) / @params.N ) )) + @params.K;
                points.Add( new DataPoint( x, y ) );
            }

            return points;
        }

        public static IList<DataPoint> OfSawtoothWave( ArgumentsMaker args, WaveformParameters @params )
        {
            IList<DataPoint> points = new List<DataPoint>();
            double x;
            double y;

            while ( args.HasNextArgument() ) {
                x = args.GetNextArgument();
                y = ((-2.0 * @params.M) / Math.PI) * Math.Atan( 1.0 / Math.Tan( (Math.PI * x) / @params.N ) ) + @params.K;
                points.Add( new DataPoint( x, y ) );
            }

            return points;
        }
    }
}
