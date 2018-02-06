using PI.src.general;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace PITests.src.tests
{
    public static class TestObjects
    {
        public static Series GetSeries( IList<double> values = null )
        {
            if ( values == null ) {
                values = new List<double>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            }

            Series series = new Series();

            for ( int i = 0; i < values.Count; i++ ) {
                series.Points.AddXY( i, values[i] );
            }

            return series;
        }

        public static Chart GetChart( Series series )
        {
            Chart chart = new Chart() {
                Size = new Size( 666, 666 ),
                Location = new Point( 1, 1 ),
                SuppressExceptions = true
            };

            chart.ChartAreas.Add( new ChartArea() );
            chart.ChartAreas[0].Axes.Initialize();
            chart.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Number;
            chart.ChartAreas[0].AxisY.IntervalType = DateTimeIntervalType.Number;
            chart.ChartAreas[0].AxisX2.IntervalType = DateTimeIntervalType.Number;
            chart.ChartAreas[0].AxisY2.IntervalType = DateTimeIntervalType.Number;
            chart.Legends.Add( new Legend() );
            chart.Series.Add( series );
            return chart;
        }

    }
}
