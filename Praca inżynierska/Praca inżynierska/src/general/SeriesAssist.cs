using PI.src.enumerators;
using System;
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

        public static bool IsChartAcceptable( IList<double> values )
        {
            foreach ( double value in values ) {
                if ( !IsChartAcceptable( value ) ) {
                    return false;
                }
            }

            return true;
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

        public static bool IsChartAcceptable( IList<DataPoint> set, int yValueIndex )
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

        public static IList<double> GetArguments( Series series )
        {
            if ( series == null ) {
                return new List<double>().AsReadOnly();
            }

            IList<double> xValues = new List<double>();

            for ( int i = 0; i < series.Points.Count; i++ ) {
                xValues.Add( series.Points[i].XValue );
            }

            return xValues;
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

        public static void OverrideValues( Series series, IList<double> values, int yValuesIndex = 0 )
        {
            if ( series == null || values == null || series.Points.Count != values.Count || yValuesIndex < 0 ) {
                return;
            }

            for ( int i = 0; i < series.Points.Count; i++ ) {
                series.Points[i].YValues[yValuesIndex] = values[i];
            }
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

        public static Series GetCopy( Series source, int yValuesIndex = 0, bool isChartable = true )
        {
            Series series = new Series();

            if ( isChartable ) {
                SetDefaultSettings( series );
            }

            if ( source == null || yValuesIndex < 0 ) {
                return series;
            }

            double x;
            double y;

            for ( int i = 0; i < source.Points.Count; i++ ) {
                x = source.Points[i].XValue;
                y = source.Points[i].YValues[yValuesIndex];
                series.Points.AddXY( x, y );
            }

            return series;
        }

        public static IList<Series> GetCopy( IList<Series> source, int yValuesIndex, bool isChartable = true )
        {
            IList<Series> set = new List<Series>();

            if ( source == null ) {
                return new List<Series>().AsReadOnly();
            }

            for ( int i = 0; i < source.Count; i++ ) {
                set.Add( GetCopy( source[i], yValuesIndex, isChartable ) );
            }

            return set;
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

        /// <exception cref="ArgumentOutOfRangeException">Indices are negative or improper.</exception>
        /// <exception cref="ArgumentNullException">Series data are not provided.</exception>
        /// <exception cref="NotFiniteNumberException">Performed operation turnes values into not finite or not chart acceptable values.</exception>
        /// <exception cref="KeyNotFoundException">Requested operation has not been implemented.</exception>
        public static void Alter( Operation @operator, double operand, Series series, int startIndex, int endIndex, int yValuesIndex = 0 )
        {
            if ( startIndex < 0 || endIndex < 0 || endIndex < startIndex || yValuesIndex < 0 ) {
                string message = nameof( startIndex ) + ',' + nameof( endIndex ) + ',' + nameof( yValuesIndex );
                throw new ArgumentOutOfRangeException( message );
            }

            if ( series == null ) {
                throw new ArgumentNullException( nameof( series ) );
            }

            var operationSwitch = new Dictionary<Operation, Action>() {
                { Operation.Addition, () => AlterOfAddition(series, operand, startIndex, endIndex, yValuesIndex) },
                { Operation.Subtraction, () => AlterOfSubtraction(series, operand, startIndex, endIndex, yValuesIndex) },
                { Operation.Multiplication, () => AlterOfMultiplication(series, operand, startIndex, endIndex, yValuesIndex) },
                { Operation.Division, () => AlterOfDivision(series, operand, startIndex, endIndex, yValuesIndex) },
                { Operation.Exponentiation, () => AlterOfExponentiation(series, operand, startIndex, endIndex, yValuesIndex) },
                { Operation.Logarithmic, () => AlterOfLogarithmic(series, operand, startIndex, endIndex, yValuesIndex) },
                { Operation.Rooting, () => AlterOfRooting(series, operand, startIndex, endIndex, yValuesIndex) },
                { Operation.Constant, () => AlterOfConstant(series, operand, startIndex, endIndex, yValuesIndex) },
                { Operation.Positive, () => AlterOfPositive(series, startIndex, endIndex, yValuesIndex) },
                { Operation.Negative, () => AlterOfNegative(series, startIndex, endIndex, yValuesIndex) }
            };

            operationSwitch[@operator]();

            if ( !IsChartAcceptable( series, yValuesIndex ) ) {
                throw new NotFiniteNumberException();
            }
        }

        private static void AlterOfAddition( Series series, double addend, int startIndex, int endIndex, int yValuesIndex = 0 )
        {
            for ( int i = startIndex; i <= endIndex; i++ ) {
                series.Points[i].YValues[yValuesIndex] += addend;
            }
        }

        private static void AlterOfSubtraction( Series series, double subtrahend, int startIndex, int endIndex, int yValuesIndex = 0 )
        {
            for ( int i = startIndex; i <= endIndex; i++ ) {
                series.Points[i].YValues[yValuesIndex] -= subtrahend;
            }
        }

        private static void AlterOfMultiplication( Series series, double multiplier, int startIndex, int endIndex, int yValuesIndex = 0 )
        {
            for ( int i = startIndex; i <= endIndex; i++ ) {
                series.Points[i].YValues[yValuesIndex] *= multiplier;
            }
        }

        private static void AlterOfDivision( Series series, double divisor, int startIndex, int endIndex, int yValuesIndex = 0 )
        {
            for ( int i = startIndex; i <= endIndex; i++ ) {
                series.Points[i].YValues[yValuesIndex] /= divisor;
            }
        }

        private static void AlterOfExponentiation( Series series, double exponent, int startIndex, int endIndex, int yValuesIndex = 0 )
        {
            double y;

            for ( int i = startIndex; i <= endIndex; i++ ) {
                y = series.Points[i].YValues[yValuesIndex];
                series.Points[i].YValues[yValuesIndex] = Math.Pow( y, exponent );
            }
        }

        private static void AlterOfLogarithmic( Series series, double basis, int startIndex, int endIndex, int yValuesIndex = 0 )
        {
            double y;

            for ( int i = startIndex; i <= endIndex; i++ ) {
                y = series.Points[i].YValues[yValuesIndex];
                series.Points[i].YValues[yValuesIndex] = Math.Log( y, basis );
            }
        }

        private static void AlterOfRooting( Series series, double level, int startIndex, int endIndex, int yValuesIndex = 0 )
        {
            double y;

            for ( int i = startIndex; i <= endIndex; i++ ) {
                y = series.Points[i].YValues[yValuesIndex];
                series.Points[i].YValues[yValuesIndex] = Mathematics.Root( y, level );
            }
        }

        private static void AlterOfConstant( Series series, double value, int startIndex, int endIndex, int yValuesIndex = 0 )
        {
            for ( int i = startIndex; i <= endIndex; i++ ) {
                series.Points[i].YValues[yValuesIndex] = value;
            }
        }

        private static void AlterOfPositive( Series series, int startIndex, int endIndex, int yValuesIndex = 0 )
        {
            double y;

            for ( int i = startIndex; i <= endIndex; i++ ) {
                y = series.Points[i].YValues[yValuesIndex];
                series.Points[i].YValues[yValuesIndex] = Math.Abs( y );
            }
        }

        private static void AlterOfNegative( Series series, int startIndex, int endIndex, int yValuesIndex = 0 )
        {
            double y;

            for ( int i = startIndex; i <= endIndex; i++ ) {
                y = series.Points[i].YValues[yValuesIndex];
                series.Points[i].YValues[yValuesIndex] = -Math.Abs( y );
            }
        }
    }
}
