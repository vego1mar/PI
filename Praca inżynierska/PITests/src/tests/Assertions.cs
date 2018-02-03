using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;

namespace PITests.src.tests
{
    public static class Assertions
    {
        public const double IBM_FLOAT_SURROUNDING = 5.96E-8;

        public static void DoubleWithin( double value, double minimum, double maximum )
        {
            if ( value < minimum || value > maximum ) {
                Assert.Fail();
            }
        }

        public static void DoubleWithin( IList<double> doubles, double minimum, double maximum )
        {
            foreach ( double value in doubles ) {
                DoubleWithin( value, minimum, maximum );
            }
        }

        public static void SameValues<T>( IList<T> list1, IList<T> list2 )
        {
            if ( (list1 == null && list2 != null) || (list1 != null && list2 == null) || list1.Count != list2.Count ) {
                Assert.Fail();
            }

            for ( int i = 0; i < list1.Count; i++ ) {
                if ( !EqualityComparer<T>.Default.Equals( list1[i], list2[i] ) ) {
                    Assert.Fail();
                }
            }
        }

        public static void SameValues( IList<double> list1, IList<double> list2, double error = IBM_FLOAT_SURROUNDING )
        {
            if ( (list1 == null && list2 != null) || (list1 != null && list2 == null) || list1.Count != list2.Count ) {
                Assert.Fail();
            }

            for ( int i = 0; i < list1.Count; i++ ) {
                Assert.AreEqual( list1[i], list2[i], error );
            }
        }

        public static void SameValues( IList<double> list, Series series, int yValuesIndex = 0, double error = IBM_FLOAT_SURROUNDING )
        {
            if ( series == null || list == null || series.Points.Count != list.Count ) {
                Assert.Fail();
            }

            for ( int i = 0; i < series.Points.Count; i++ ) {
                Assert.AreEqual( series.Points[i].YValues[yValuesIndex], list[i], error );
            }
        }

        public static void SameValues( Series series1, Series series2, int yValuesIndex = 0, double error = IBM_FLOAT_SURROUNDING )
        {
            if ( series1 == null || series2 == null || yValuesIndex < 0 || series1.Points.Count != series2.Points.Count ) {
                Assert.Fail();
            }

            for ( int i = 0; i < series1.Points.Count; i++ ) {
                Assert.AreEqual( series1.Points[i].YValues[yValuesIndex], series2.Points[i].YValues[yValuesIndex], error );
            }
        }
    }
}
