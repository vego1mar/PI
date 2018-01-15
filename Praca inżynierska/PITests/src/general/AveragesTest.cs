using Microsoft.VisualStudio.TestTools.UnitTesting;
using PI.src.general;
using PITests.src.tests;
using System.Collections.Generic;

namespace PITests.src.general
{
    [TestClass]
    public class AveragesTest
    {
        [TestMethod]
        public void Median()
        {
            // given
            IList<double> list1 = new List<double>() { 2.0, 9.0, 1.0, 5.0, 3.0, 8.0, 6.0, 7.0, 4.0 };
            IList<double> list2 = new List<double>() { 10.0, 9.0, 2.0, 8.0, 6.0, 1.0, 5.0, 3.0, 4.0, 7.0 };
            IList<double> list3 = new List<double>() { 7, 8, 3, 4, 9, 2 };
            IList<IList<double>> list4 = new List<IList<double>>() { list1, list2, list3, null, new List<double>() };
            IList<IList<double>> list5 = new List<IList<double>>() { new List<double>() { 1.0 }, new List<double>() { 2.0, 1.0 } };
            const double EXPECTED_1 = 5.0;
            const double EXPECTED_2 = (5.0 + 6.0) / 2.0;
            const double EXPECTED_3 = (4 + 7) / 2.0;
            IList<double> expected4 = new List<double>() { EXPECTED_1, EXPECTED_2, EXPECTED_3, -1.0, -1.0 };
            IList<double> expected5 = new List<double>() { 1.0, (2.0 + 1.0) / 2.0 };

            // when
            double result1 = Averages.Median( list1 );
            double result2 = Averages.Median( list2 );
            double result3 = Averages.Median( list3 );
            IList<double> result4 = Averages.Median( list4 );
            IList<double> result5 = Averages.Median( list5 );

            // then
            Assert.AreEqual( EXPECTED_1, result1, Assertions.IBM_FLOAT_SURROUNDING );
            Assert.AreEqual( EXPECTED_2, result2, Assertions.IBM_FLOAT_SURROUNDING );
            Assert.AreEqual( EXPECTED_3, result3, Assertions.IBM_FLOAT_SURROUNDING );
            Assertions.SameValues( result4, expected4 );
            Assertions.SameValues( result5, expected5 );
        }

        [TestMethod]
        public void Maximum()
        {
            // given
            IList<double> list1 = new List<double>() { -1.0, 0.0, 1.0 };
            IList<double> list2 = new List<double>() { 2.0, -1.0, 0.0 };
            IList<double> list3 = null;
            IList<double> list4 = new List<double>();
            IList<IList<double>> list5 = new List<IList<double>>() { list1, list2, list3, list4, new List<double>() { 1.0, 1.1, 1.2, 1.3, 1.4 } };
            double expected1 = 1.0;
            double expected2 = 2.0;
            double expected3 = -1.0;
            double expected4 = -1.0;
            IList<double> expected5 = new List<double>() { expected1, expected2, expected3, expected4, 1.4 };

            // when
            double result1 = Averages.Maximum( list1 );
            double result2 = Averages.Maximum( list2 );
            double result3 = Averages.Maximum( list3 );
            double result4 = Averages.Maximum( list4 );
            IList<double> result5 = Averages.Maximum( list5 );

            // then
            Assert.AreEqual( expected1, result1, Assertions.IBM_FLOAT_SURROUNDING );
            Assert.AreEqual( expected2, result2, Assertions.IBM_FLOAT_SURROUNDING );
            Assert.AreEqual( expected3, result3, Assertions.IBM_FLOAT_SURROUNDING );
            Assert.AreEqual( expected4, result4, Assertions.IBM_FLOAT_SURROUNDING );
            Assertions.SameValues( result5, expected5 );
        }
    }
}
