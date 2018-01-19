using Microsoft.VisualStudio.TestTools.UnitTesting;
using PI.src.enumerators;
using PI.src.general;
using PI.src.parameters;
using PITests.src.tests;
using System;
using System.Collections.Generic;
using System.Linq;

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
            IList<IList<double>> list4 = new List<IList<double>>() { list1, list2, list3 };
            IList<IList<double>> list5 = new List<IList<double>>() { new List<double>() { 1.0 }, new List<double>() { 2.0, 1.0 } };
            const double EXPECTED_1 = 5.0;
            const double EXPECTED_2 = (5.0 + 6.0) / 2.0;
            const double EXPECTED_3 = (4 + 7) / 2.0;
            IList<double> expected4 = new List<double>() { EXPECTED_1, EXPECTED_2, EXPECTED_3 };
            IList<double> expected5 = new List<double>() { 1.0, (2.0 + 1.0) / 2.0 };

            // when
            double result1 = Averages.Median( list1 ).Value;
            double result2 = Averages.Median( list2 ).Value;
            double result3 = Averages.Median( list3 ).Value;
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
            IList<IList<double>> list5 = new List<IList<double>>() { list1, list2, new List<double>() { 1.0, 1.1, 1.2, 1.3, 1.4 } };
            double expected1 = 1.0;
            double expected2 = 2.0;
            IList<double> expected5 = new List<double>() { expected1, expected2, 1.4 };

            // when
            double result1 = Averages.Maximum( list1 ).Value;
            double result2 = Averages.Maximum( list2 ).Value;
            IList<double> result5 = Averages.Maximum( list5 );

            // then
            Assert.AreEqual( expected1, result1, Assertions.IBM_FLOAT_SURROUNDING );
            Assert.AreEqual( expected2, result2, Assertions.IBM_FLOAT_SURROUNDING );
            Assertions.SameValues( result5, expected5 );
        }

        [TestMethod]
        public void Minimum()
        {
            // given
            IList<double> list1 = new List<double>() { -1.0, 0.0, 1.0 };
            IList<double> list2 = new List<double>() { 2.0, -1.0, 0.0 };
            IList<IList<double>> list5 = new List<IList<double>>() { list1, list2, new List<double>() { 1.0, 1.1, 1.2, 1.3, 1.4 } };
            double expected1 = -1.0;
            double expected2 = -1.0;
            IList<double> expected5 = new List<double>() { expected1, expected2, 1.0 };

            // when
            double result1 = Averages.Minimum( list1 ).Value;
            double result2 = Averages.Minimum( list2 ).Value;
            IList<double> result5 = Averages.Minimum( list5 );

            // then
            Assert.AreEqual( expected1, result1, Assertions.IBM_FLOAT_SURROUNDING );
            Assert.AreEqual( expected2, result2, Assertions.IBM_FLOAT_SURROUNDING );
            Assertions.SameValues( result5, expected5 );
        }

        [TestMethod]
        public void AGM()
        {
            // given
            IList<double> list1 = new List<double>() { 2, 3, 5, 7, 9, 10, 11, 23 };
            IList<double> list3 = new List<double>() { -1, 0, 2, -3, 4, -5, 8, 7 };
            IList<IList<double>> list4 = new List<IList<double>>() { list1, list3 };
            const double EXPECTED1 = 7.76470249483093;
            const double EXPECTED2 = 8.0652853391755;
            const double EXPECTED3 = 0.555265813041983;
            IList<double> expected4 = new List<double>() { EXPECTED2, EXPECTED3 };

            // when
            double result1 = Averages.AGM( list1, GeometricMeanVariant.Absolute ).Value;
            double result2 = Averages.AGM( list1, GeometricMeanVariant.Offset ).Value;
            double result3 = Averages.AGM( list3, GeometricMeanVariant.Offset ).Value;
            IList<double> result4 = Averages.AGM( list4, GeometricMeanVariant.Offset );

            // then
            Assert.AreEqual( EXPECTED1, result1, Assertions.IBM_FLOAT_SURROUNDING );
            Assert.AreEqual( EXPECTED2, result2, Assertions.IBM_FLOAT_SURROUNDING );
            Assert.AreEqual( EXPECTED3, result3, Assertions.IBM_FLOAT_SURROUNDING );
            Assertions.SameValues( expected4, result4 );
        }

        [TestMethod]
        public void Heronian()
        {
            // given
            IList<double> list1 = new List<double>() { -1, 0, 1, 2, 3 };
            IList<double> list2 = new List<double>() { 0, 0 };
            IList<IList<double>> list3 = new List<IList<double>>() { list1, list2 };
            const double EXPECTED1 = 0.934195180782892;
            const double EXPECTED2 = 0.0;
            IList<double> expected3 = new List<double>() { EXPECTED1, EXPECTED2 };

            // when
            double result1 = Averages.Heronian( list1, GeometricMeanVariant.Offset ).Value;
            double result2 = Averages.Heronian( list2, GeometricMeanVariant.Offset ).Value;
            IList<double> result3 = Averages.Heronian( list3, GeometricMeanVariant.Offset );

            // then
            Assert.AreEqual( EXPECTED1, result1, Assertions.IBM_FLOAT_SURROUNDING );
            Assert.AreEqual( EXPECTED2, result2, Assertions.IBM_FLOAT_SURROUNDING );
            Assertions.SameValues( expected3, result3, 1.0E-14 );
        }

        [TestMethod]
        public void Harmonic()
        {
            // given
            IList<double> list1 = new List<double>() { 5, 20, 40, 80, 100 };
            IList<double> list2 = new List<double>() { 2, 3 };
            IList<double> list3 = new List<double>() { 2, -3 };
            IList<IList<double>> list4 = new List<IList<double>>() { list1, list2 };
            const double EXPECTED1 = 16.8067226890756;
            const double EXPECTED2 = 2.4;
            const double EXPECTED3 = -16.0 / 7.0;
            IList<double> expected4 = new List<double>() { EXPECTED1, EXPECTED2 };

            // when
            double result1 = Averages.Harmonic( list1, StandardMeanVariants.Straight ).Value;
            double result2 = Averages.Harmonic( list2, StandardMeanVariants.Straight ).Value;
            double result3 = Averages.Harmonic( list3, StandardMeanVariants.Offset ).Value;
            IList<double> result4 = Averages.Harmonic( list4, StandardMeanVariants.Straight );

            // then
            Assert.AreEqual( EXPECTED1, result1, Assertions.IBM_FLOAT_SURROUNDING );
            Assert.AreEqual( EXPECTED2, result2, Assertions.IBM_FLOAT_SURROUNDING );
            Assert.AreEqual( EXPECTED3, result3, Assertions.IBM_FLOAT_SURROUNDING );
            Assertions.SameValues( expected4, result4, 1.0E-13 );
        }

        [TestMethod]
        public void Generalized()
        {
            // given
            IList<double> list1 = new List<double>() { 1, 2, 3, 4, 5 };
            IList<double> list2 = new List<double>() { 3, 7, 11, 23, 111 };
            IList<double> list3 = new List<double>() { -2, 0, 1, 5 };
            IList<IList<double>> list4 = new List<IList<double>>() { list1, new List<double>() { 2, 1 } };
            int rank1 = 2;
            int rank2 = 3;
            int rank3 = 4;
            double expected1 = Math.Sqrt( 11.0 );
            double expected2 = Mathematics.Root( 1381499.0 / 5.0, rank2 );
            double expected3 = Mathematics.Root( 2217.0 / 2.0, rank3 ) - (Math.Abs( list3.Min() ) + 1.0);
            IList<double> expected4 = new List<double>() { expected1, Math.Sqrt( 5.0 / 2.0 ) };

            // when
            double result1 = Averages.Generalized( list1, StandardMeanVariants.Straight, rank1 ).Value;
            double result2 = Averages.Generalized( list2, StandardMeanVariants.Straight, rank2 ).Value;
            double result3 = Averages.Generalized( list3, StandardMeanVariants.Offset, rank3 ).Value;
            IList<double> result4 = Averages.Generalized( list4, StandardMeanVariants.Straight, rank1 );

            // then
            Assert.AreEqual( expected1, result1, Assertions.IBM_FLOAT_SURROUNDING );
            Assert.AreEqual( expected2, result2, Assertions.IBM_FLOAT_SURROUNDING );
            Assert.AreEqual( expected3, result3, Assertions.IBM_FLOAT_SURROUNDING );
            Assertions.SameValues( expected4, result4 );
        }

        [TestMethod]
        public void Moving()
        {
            // given
            IList<double> list1 = new List<double>() { 1, 2, 1, 2, 3, 4, 5, 2, 2, 1, 1 };
            IList<double> list2 = new List<double>() { 3, 4 };
            IList<double> list3 = new List<double>() { Math.Sqrt( 2.0 ), 7.0 / 3.0, Math.Log( 2.0 ) };
            IList<IList<double>> list4 = new List<IList<double>>() { list1, list2, list3 };
            const double EXPECTED1 = 2.0 + (13.0 / 66.0);
            const double EXPECTED2 = 3.5;
            double expected3 = (((list3[0] + list3[1]) / 2) + ((list3[0] + list3[1] + list3[2]) / 3) + ((list3[1] + list3[2]) / 2)) / 3.0;
            IList<double> expected4 = new List<double>() { EXPECTED1, EXPECTED2, expected3 };

            // when
            double result1 = Averages.Moving( list1, MovingAverageType.Simple ).Value;
            double result2 = Averages.Moving( list2, MovingAverageType.Simple ).Value;
            double result3 = Averages.Moving( list3, MovingAverageType.Simple ).Value;
            IList<double> result4 = Averages.Moving( list4, MovingAverageType.Simple );

            // then
            Assert.AreEqual( EXPECTED1, result1, Assertions.IBM_FLOAT_SURROUNDING );
            Assert.AreEqual( EXPECTED2, result2, Assertions.IBM_FLOAT_SURROUNDING );
            Assert.AreEqual( expected3, result3, Assertions.IBM_FLOAT_SURROUNDING );
            Assertions.SameValues( expected4, result4, 1.0E-14 );
        }

        [TestMethod]
        public void Tolerance()
        {
            // given
            IList<double> list1 = new List<double>() { 1, 2, 3, 3, 1, 2, 5, 5, 5, 2, 1, 3, 3, 2, 1 };
            IList<double> list4 = new List<double>() { 10, 100, 1000 };
            IList<IList<double>> list5 = new List<IList<double>>() { list1, list4 };
            double classifier1 = 2.0;
            double classifier4 = 11.0;
            double tolerance5 = 0.1;
            MeansParameters params4 = new MeansParameters();
            params4.Harmonic.Variant = StandardMeanVariants.Straight;
            double expected1 = 3.0;
            double expected2 = 1.0;
            double expected3 = 2.0;
            double expected4 = list4[1];
            IList<double> expected5 = new List<double>() { list1.Max(), list4[1] };

            // when
            double result1 = Averages.Tolerance( list1, classifier1, MeanType.Maximum ).Value;
            double result2 = Averages.Tolerance( list1, classifier1, MeanType.Minimum ).Value;
            double result3 = Averages.Tolerance( list1, classifier1, MeanType.Arithmetic ).Value;
            double result4 = Averages.Tolerance( list4, classifier4, MeanType.Harmonic, params4 ).Value;
            IList<double> result5 = Averages.Tolerance( list5, tolerance5, MeanType.Maximum );

            // then
            Assert.AreEqual( expected1, result1, Assertions.IBM_FLOAT_SURROUNDING );
            Assert.AreEqual( expected2, result2, Assertions.IBM_FLOAT_SURROUNDING );
            Assert.AreEqual( expected3, result3, Assertions.IBM_FLOAT_SURROUNDING );
            Assert.AreEqual( expected4, result4, Assertions.IBM_FLOAT_SURROUNDING );
            Assertions.SameValues( expected5, result5 );
        }
    }
}
