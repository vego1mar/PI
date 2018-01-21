using Microsoft.VisualStudio.TestTools.UnitTesting;
using PI.src.general;
using PITests.src.tests;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PITests.src.general
{
    [TestClass]
    public class List
    {
        [TestMethod]
        public void GetSorted()
        {
            // given
            IList<int> list1 = new List<int>() { 1, 9, 5, 4, 6, 3, 8, 7, 2, 0 };
            IList<int> expected1 = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            IList<double> list2 = new List<double>() { 1.1, 2.2, 1.2, 2.1, -1.2, 1.3, 1.8, 0.1, 2.0, 0.5 };
            IList<double> expected2 = new List<double>() { -1.2, 0.1, 0.5, 1.1, 1.2, 1.3, 1.8, 2.0, 2.1, 2.2 };

            // when
            IList<int> result1 = Lists.GetSorted( list1 );
            IList<double> result2 = Lists.GetSorted( list2 );

            // then
            Assertions.SameValues( result1, expected1 );
            Assertions.SameValues( result2, expected2 );
        }

        [TestMethod]
        public void GetSortedIntoHistogram()
        {
            // given
            IList<double> set1 = new List<double>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            IList<double> set2 = new List<double>() { -2.2, -0.4, 0.5, 1.79, 3.2, 5.1 };
            int divisions1 = 10;
            int divisions2 = 11;

            // when
            IList<IList<double>> result1 = Lists.GetSortedIntoHistogram( set1, divisions1 );
            IList<IList<double>> result2 = Lists.GetSortedIntoHistogram( set2, divisions2 );

            // then
            Assert.IsTrue( result1.Count == divisions1 );
            Assert.IsTrue( result2.Count == divisions2 );
            foreach ( var list in result1 ) { Assert.IsTrue( list.Count == (divisions1 / set1.Count) ); }
            for ( int i = 0; i < result2.Count; i++ ) { Assert.IsTrue( (i % 2 == 0) ? (result2[i].Count == 1) : (result2[i].Count == 0) ); }
        }

        [TestMethod]
        public void Concat()
        {
            // given
            IList<double> set1 = new List<double>() { 0 };
            IList<double> set2 = new List<double>() { 1, 2 };
            IList<double> set3 = new List<double>() { 3, 4, 5 };
            IList<double> target = new List<double>();
            IList<double> expected = new List<double>() { 0, 3, 4, 5, 1, 2 };

            // when
            Lists.Concat( target, set1 );
            Lists.Concat( target, set3 );
            Lists.Concat( target, set2 );

            // then
            Assertions.SameValues( target, expected );
        }

        [TestMethod]
        public void Sum()
        {
            // given
            IList<double> list1 = new List<double>() { 1.0, 2.1, 3.2, 4.3, 5.4, 6.5, 7.6, 8.7, 9.8, 9.9 };
            IList<double> list2 = new List<double>() { -1.0, 0.0, 1.0, Math.Sqrt( 7.25 ) };
            double sum1 = list1.Sum();
            double sum2 = list2.Sum();

            // when
            double result1 = Lists.Sum( list1 );
            double result2 = Lists.Sum( list2 );

            // then
            Assert.AreEqual( sum1, result1, Assertions.IBM_FLOAT_SURROUNDING );
            Assert.AreEqual( sum2, result2, Assertions.IBM_FLOAT_SURROUNDING );
        }

        [TestMethod]
        public void Product()
        {
            // given
            IList<double> list1 = new List<double>() { 1.0, 2.0, 3.0, 4.0, 5.0 };
            double product1 = 120.0;

            // when
            double result1 = Lists.Product( list1 );

            // then
            Assert.AreEqual( product1, result1, Assertions.IBM_FLOAT_SURROUNDING );
        }

        [TestMethod]
        public void Reciprocal()
        {
            // given
            IList<double> list1 = new List<double>() { 1.0, 2.0, 3.0, 4.0, Math.Sqrt( 5.0 / 6.0 ) };
            IList<double> expected1 = new List<double>() { 1.0 / list1[0], 1.0 / list1[1], 1.0 / list1[2], 1.0 / list1[3], 1.0 / list1[4] };

            // when
            Lists.Reciprocal( list1 );

            // then
            Assertions.SameValues( list1, expected1 );
        }

        [TestMethod]
        public void Exponentiate()
        {
            // given
            IList<double> list1 = new List<double>() { 1, 2, 3, 4, 5 };
            IList<double> expected1 = new List<double>() { 1, 4, 9, 16, 25 };
            const double EXPONENT1 = 2.0;

            // when
            Lists.Exponentiate( list1, EXPONENT1 );

            // then
            Assertions.SameValues( list1, expected1 );
        }
    }
}
