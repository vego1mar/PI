using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PI.src.general;
using PITests.src.tests;

namespace PITests.src.general
{
    [TestClass]
    public class ChartAssistTest
    {
        [TestMethod]
        [ExpectedException( typeof( ArgumentOutOfRangeException ) )]
        public void Refresh1()
        {
            // given
            Series series1 = TestObjects.GetSeries();
            Chart chart1 = TestObjects.GetChart( series1 );

            // when
            ChartAssist.Refresh( series1, Color.Indigo, chart1, 0, -1 );
        }

        [TestMethod]
        [ExpectedException( typeof( ArgumentOutOfRangeException ) )]
        public void Refresh2()
        {
            // given
            Series series1 = TestObjects.GetSeries();
            Chart chart1 = TestObjects.GetChart( series1 );

            // when
            ChartAssist.Refresh( series1, Color.Indigo, chart1, -1, 0 );
        }

        [TestMethod]
        [ExpectedException( typeof( NullReferenceException ) )]
        public void Refresh3()
        {
            // given
            Chart chart1 = TestObjects.GetChart( null );

            // when
            ChartAssist.Refresh( null, Color.Indigo, chart1, 0, 0 );
        }

        [TestMethod]
        [ExpectedException( typeof( InvalidOperationException ) )]
        public void Refresh4()
        {
            // given
            IList<double> set = new List<double>() { double.MinValue, double.NaN, double.MaxValue };
            Series series1 = TestObjects.GetSeries( set );
            Chart chart1 = TestObjects.GetChart( series1 );

            // when
            ChartAssist.Refresh( series1, Color.Indigo, chart1 );
        }

        [TestMethod]
        [ExpectedException( typeof( OverflowException ) )]
        public void Refresh5()
        {
            // given
            IList<double> set = new List<double>() { -7.92E28, double.NaN, 7.92E28 };
            Series series2 = TestObjects.GetSeries( set );
            Chart chart2 = TestObjects.GetChart( series2 );

            // when
            ChartAssist.Refresh( series2, Color.Indigo, chart2 );
        }
    }
}
