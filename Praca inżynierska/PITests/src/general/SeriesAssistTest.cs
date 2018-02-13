using System;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PI.src.curves;
using PI.src.enumerators;
using PI.src.general;
using PITests.src.tests;

namespace PITests.src.general
{
    [TestClass]
    public class SeriesAssistTest
    {
        [TestMethod]
        public void GetCopy1()
        {
            // given
            Series series1 = TestObjects.GetSeries();
            Series series2 = TestObjects.GetSeries( new List<double>() { -1, 0, 1 } );

            // when
            Series result1 = SeriesAssist.GetCopy( series1, 0, false );
            Series result2 = SeriesAssist.GetCopy( series2 );

            // then
            Assertions.SameValues( series1, result1 );
            Assertions.SameValues( series2, result2 );
        }

        [TestMethod]
        public void Alter1()
        {
            // given
            const int START_INDEX = 1;
            const int END_INDEX = 3;
            IList<double> set = new List<double>() { 1, 2, 3, 4, 5 };

            Series series1 = TestObjects.GetSeries( set );
            Series series2 = TestObjects.GetSeries( set );
            Series series3 = TestObjects.GetSeries( set );
            Series series4 = TestObjects.GetSeries( set );
            Series series5 = TestObjects.GetSeries( set );
            Series series6 = TestObjects.GetSeries( set );
            Series series7 = TestObjects.GetSeries( set );
            Series series8 = TestObjects.GetSeries( set );
            Series series9 = TestObjects.GetSeries( set );
            Series series10 = TestObjects.GetSeries( set );
            double operand1 = Math.Sqrt( 2.0 );
            double operand2 = Math.Sqrt( 3.1 );
            double operand3 = Math.Sqrt( Math.PI );
            double operand4 = Math.Sqrt( 2.0 * Math.PI );
            double operand5 = Math.Sqrt( Math.Cos( 0.5 ) );
            double operand6 = Math.Log10( 10.123 );
            double operand7 = Mathematics.Root( 8.0, Math.Sqrt( 2.0 ) );
            double operand8 = Mathematics.Reciprocal( Mathematics.Root( 8.0, 2.0 ) );
            IList<double> expected1 = new List<double>() { set[0], set[1] + operand1, set[2] + operand1, set[3] + operand1, set[4] };
            IList<double> expected2 = new List<double>() { set[0], set[1] - operand2, set[2] - operand2, set[3] - operand2, set[4] };
            IList<double> expected3 = new List<double>() { set[0], set[1] * operand3, set[2] * operand3, set[3] * operand3, set[4] };
            IList<double> expected4 = new List<double>() { set[0], set[1] / operand4, set[2] / operand4, set[3] / operand4, set[4] };
            IList<double> expected5 = new List<double>() { set[0], Math.Pow( set[1], operand5 ), Math.Pow( set[2], operand5 ), Math.Pow( set[3], operand5 ), set[4] };
            IList<double> expected6 = new List<double>() { set[0], Math.Log( set[1], operand6 ), Math.Log( set[2], operand6 ), Math.Log( set[3], operand6 ), set[4] };
            IList<double> expected7 = new List<double>() { set[0], Mathematics.Root( set[1], operand7 ), Mathematics.Root( set[2], operand7 ), Mathematics.Root( set[3], operand7 ), set[4] };
            IList<double> expected8 = new List<double>() { set[0], operand8, operand8, operand8, set[4] };
            IList<double> expected10 = new List<double>() { set[0], -Math.Abs( set[1] ), -Math.Abs( set[2] ), -Math.Abs( set[3] ), set[4] };

            // when
            SeriesAssist.Alter( Operation.Addition, operand1, series1, START_INDEX, END_INDEX );
            SeriesAssist.Alter( Operation.Subtraction, operand2, series2, START_INDEX, END_INDEX );
            SeriesAssist.Alter( Operation.Multiplication, operand3, series3, START_INDEX, END_INDEX );
            SeriesAssist.Alter( Operation.Division, operand4, series4, START_INDEX, END_INDEX );
            SeriesAssist.Alter( Operation.Exponentiation, operand5, series5, START_INDEX, END_INDEX );
            SeriesAssist.Alter( Operation.Logarithmic, operand6, series6, START_INDEX, END_INDEX );
            SeriesAssist.Alter( Operation.Rooting, operand7, series7, START_INDEX, END_INDEX );
            SeriesAssist.Alter( Operation.Constant, operand8, series8, START_INDEX, END_INDEX );
            SeriesAssist.Alter( Operation.Positive, 0.0, series9, START_INDEX, END_INDEX );
            SeriesAssist.Alter( Operation.Negative, 0.0, series10, START_INDEX, END_INDEX );

            // then
            Assertions.SameValues( expected1, series1 );
            Assertions.SameValues( expected2, series2 );
            Assertions.SameValues( expected3, series3 );
            Assertions.SameValues( expected4, series4 );
            Assertions.SameValues( expected5, series5 );
            Assertions.SameValues( expected6, series6 );
            Assertions.SameValues( expected7, series7 );
            Assertions.SameValues( expected8, series8 );
            Assertions.SameValues( set, series9 );
            Assertions.SameValues( expected10, series10 );
        }

        [TestMethod]
        [ExpectedException( typeof( ArgumentOutOfRangeException ) )]
        public void Alter2()
        {
            // given
            Series series = TestObjects.GetSeries();

            // when
            SeriesAssist.Alter( Operation.Constant, 0.0, series, -1, 0, 0 );
        }

        [TestMethod]
        [ExpectedException( typeof( ArgumentOutOfRangeException ) )]
        public void Alter3()
        {
            // given
            Series series = TestObjects.GetSeries();

            // when
            SeriesAssist.Alter( Operation.Constant, 0.0, series, 0, -1, 0 );
        }

        [TestMethod]
        [ExpectedException( typeof( ArgumentOutOfRangeException ) )]
        public void Alter4()
        {
            // given
            Series series = TestObjects.GetSeries();

            // when
            SeriesAssist.Alter( Operation.Constant, 0.0, series, 0, 0, -1 );
        }

        [TestMethod]
        [ExpectedException( typeof( ArgumentOutOfRangeException ) )]
        public void Alter5()
        {
            // given
            Series series = TestObjects.GetSeries();

            // when
            SeriesAssist.Alter( Operation.Constant, 0.0, series, 2, 1, 0 );
        }

        [TestMethod]
        [ExpectedException( typeof( ArgumentNullException ) )]
        public void Alter6()
        {
            // when
            SeriesAssist.Alter( Operation.Constant, 0.0, null, 0, 0, 0 );
        }

        [TestMethod]
        [ExpectedException( typeof( NotFiniteNumberException ) )]
        public void Alter7()
        {
            // given
            Series series = TestObjects.GetSeries();
            const double OPERAND = 123.0;

            // when
            SeriesAssist.Alter( Operation.Exponentiation, OPERAND, series, 0, series.Points.Count - 1 );
        }
    }
}
