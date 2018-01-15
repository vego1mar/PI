using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace PI.src.general
{
    public static class SeriesAssist
    {
        public const int DEFAULT_BORDER_WIDTH = 3;
        public const ChartDashStyle DEFAULT_BORDER_DASH_STYLE = ChartDashStyle.Solid;
        public const SeriesChartType DEFAULT_CHART_TYPE = SeriesChartType.Line;
        private const double CHART_ACCEPTABLE_MAXIMUM_VALUE = 7.92E27;
        private const double CHART_ACCEPTABLE_MINIMUM_VALUE = -7.92E27;

        public static void SetDefaultSettings( Series series )
        {
            series.BorderWidth = DEFAULT_BORDER_WIDTH;
            series.BorderDashStyle = DEFAULT_BORDER_DASH_STYLE;
            series.ChartType = DEFAULT_CHART_TYPE;
            series.Color = Color.Black;
            series.Font = new Font( "Consolas", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 238 );
            series.IsVisibleInLegend = false;
            series.IsXValueIndexed = true;
            series.YValueType = ChartValueType.Double;
            series.XValueType = ChartValueType.Double;
            series.YValuesPerPoint = 1;
        }

        public static bool IsChartAcceptable( Series series, int yValuesIndex = 0 )
        {
            for ( int i = 0; i < series.Points.Count; i++ ) {
                if ( !IsChartAcceptable( series.Points[i], yValuesIndex ) ) {
                    return false;
                }
            }

            return true;
        }

        public static bool IsChartAcceptable( DataPoint point, int yValueIndex = 0 )
        {
            double x = point.XValue;
            double y = point.YValues[yValueIndex];
            return IsChartAcceptable( x ) && IsChartAcceptable( y );
        }

        public static bool IsChartAcceptable( IList<DataPoint> set, int yValueIndex = 0 )
        {
            bool result = true;

            for ( int i = 0; i < set.Count; i++ ) {
                result &= IsChartAcceptable( set[i], yValueIndex );

                if ( !result ) {
                    return false;
                }
            }

            return result;
        }

        public static bool IsChartAcceptable( double value )
        {
            return value < CHART_ACCEPTABLE_MAXIMUM_VALUE && value > CHART_ACCEPTABLE_MINIMUM_VALUE;
        }

        public static IList<double> GetValues( Series series, int yValuesIndex = 0 )
        {
            if ( series == null || yValuesIndex < 0 ) {
                return new List<double>().AsReadOnly();
            }

            IList<double> yValues = new List<double>();

            for ( int i = 0; i < series.Points.Count; i++ ) {
                yValues.Add( series.Points[i].YValues[yValuesIndex] );
            }

            return yValues;
        }

        public static IList<double> GetValues( IList<Series> seriesList, int seriesNo, int yValuesIndex = 0 )
        {
            if ( seriesList == null || seriesNo < 0 || seriesNo >= seriesList[seriesNo].Points.Count ) {
                return new List<double>().AsReadOnly();
            }

            return GetValues( seriesList[seriesNo], yValuesIndex );
        }

        public static void CopyPoints( Series source, Series target, int yValuesIndex = 0 )
        {
            if ( source == null || target == null || yValuesIndex < 0 ) {
                return;
            }

            double x;
            double y;

            for ( int i = 0; i < source.Points.Count; i++ ) {
                x = source.Points[i].XValue;
                y = source.Points[i].YValues[yValuesIndex];
                target.Points.AddXY( x, y );
            }
        }

        public static void CopyPoints( Series target, Series xSource, IList<double> ySource )
        {
            if ( target == null || xSource == null || ySource == null || xSource.Points.Count != ySource.Count ) {
                return;
            }

            double x;
            double y;

            for ( int i = 0; i < xSource.Points.Count; i++ ) {
                x = xSource.Points[i].XValue;
                y = ySource[i];
                target.Points.AddXY( x, y );
            }
        }

        public static void CopyPoints( IList<Series> target, int targetSet, Series source, int yValuesIndex = 0 )
        {
            if ( target == null || source == null || targetSet < 0 || target[targetSet] == null ) {
                return;
            }

            CopyPoints( source, target[targetSet], yValuesIndex );
        }

        public static void CopyPoints( IList<DataPoint> source, Series target, int yValuesIndex = 0 )
        {
            if ( source == null || target == null ) {
                return;
            }

            double x;
            double y;

            for ( int i = 0; i < source.Count; i++ ) {
                x = source[i].XValue;
                y = source[i].YValues[yValuesIndex];
                target.Points.AddXY( x, y );
            }
        }

        public static IList<DataPoint> GetChartAcceptablePoints( Series source, int yValuesIndex = 0 )
        {
            if ( source == null ) {
                return new List<DataPoint>().AsReadOnly();
            }

            IList<DataPoint> points = new List<DataPoint>();
            double x;
            double y;

            for ( int i = 0; i < source.Points.Count; i++ ) {
                x = source.Points[i].XValue;
                y = source.Points[i].YValues[yValuesIndex];

                if ( IsChartAcceptable( source.Points[i], yValuesIndex ) ) {
                    points.Add( new DataPoint( x, y ) );
                }
            }

            return points;
        }

        public static IList<IList<DataPoint>> GetCopy( IList<Series> source, int seriesNo, int yValuesIndex = 0 )
        {
            if ( source == null || seriesNo < 0 || yValuesIndex < 0 ) {
                return new List<IList<DataPoint>>().AsReadOnly();
            }

            IList<IList<DataPoint>> copy = new List<IList<DataPoint>>();
            double x;
            double y;

            for ( int i = 0; i < seriesNo; i++ ) {
                copy.Add( new List<DataPoint>() );

                for ( int j = 0; j < source[i].Points.Count; j++ ) {
                    x = source[i].Points[j].XValue;
                    y = source[i].Points[j].YValues[yValuesIndex];
                    copy[i].Add( new DataPoint( x, y ) );
                }
            }

            return copy;
        }

        /// <summary>Get copy of Series.Points.YValues[yValuesIndex] organized to use like List[x][y].</summary>
        public static IList<IList<double>> GetOrderedCopy( IList<Series> source, int seriesNo, int yValuesIndex = 0 )
        {
            if ( seriesNo < 0 || source == null || source.Count == 0 || yValuesIndex < 0 ) {
                return new List<IList<double>>().AsReadOnly();
            }

            IList<IList<double>> copy = Lists.Get<double>( source[0].Points.Count, seriesNo );

            for ( int i = 0; i < seriesNo; i++ ) {
                for ( int j = 0; j < source[i].Points.Count; j++ ) {
                    copy[j][i] = source[i].Points[j].YValues[yValuesIndex];
                }
            }

            return copy;
        }
    }
}
