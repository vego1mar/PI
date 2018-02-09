using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using PI.src.general;
using PITests.src.tests;
using System.Linq;
using System;

namespace PITests.src.general
{
    [TestClass]
    public class RandomsTest
    {
        [TestMethod]
        public void NextDouble()
        {
            // given
            const int DOUBLES_NO = 10_000;
            const double MIN_VALUE = -11;
            const double MAX_VALUE = 23;
            const double EXPECTED_MEAN = (MAX_VALUE - MIN_VALUE) / 2.0;
            const double ERROR = 1.0E-2;
            IList<double> doubles = new List<double>();

            // when
            for ( int i = 0; i < DOUBLES_NO; i++ ) {
                doubles.Add( Randoms.NextDouble( MIN_VALUE, MAX_VALUE ) );
            }

            // then
            Assertions.DoubleWithin( doubles, MIN_VALUE, MAX_VALUE );
            Assert.AreEqual( EXPECTED_MEAN, (doubles.Max() - doubles.Min()) / 2.0, ERROR );
        }

        [TestMethod]
        public void GetGaussian()
        {
            // given
            const int CYCLES = 10_000;
            const double ACCEPTABLE_ERROR = 1.0E-1;
            double mean1 = 0.0;
            double mean2 = 101.612;
            double mean3 = 1.0;
            double stdDeviation1 = 1.0;
            double stdDeviation2 = Math.Sqrt( mean2 );
            double stdDeviation3 = 1E-10;
            IList<double> gaussians1 = new List<double>();
            IList<double> gaussians2 = new List<double>();
            IList<double> gaussians3 = new List<double>();
            double sum1 = 0.0;
            double sum2 = 0.0;
            double sum3 = 0.0;

            // when
            for ( int i = 0; i < CYCLES; i++ ) {
                gaussians1.Add( Randoms.GetGaussian( mean1, stdDeviation1 ) );
                gaussians2.Add( Randoms.GetGaussian( mean2, stdDeviation2 ) );
                gaussians3.Add( Randoms.GetGaussian( mean3, stdDeviation3 ) );
            }

            gaussians1.ToList().ForEach( v => sum1 += v );
            gaussians2.ToList().ForEach( v => sum2 += v );
            gaussians3.ToList().ForEach( v => sum3 += v );

            // then
            Assertions.DoubleWithin( gaussians1, mean1 - stdDeviation1, stdDeviation1 + mean1 );
            Assertions.DoubleWithin( gaussians2, mean2 - stdDeviation2, stdDeviation2 + mean2 );
            Assertions.DoubleWithin( gaussians3, mean3 - stdDeviation3, stdDeviation3 + mean3 );
            Assert.AreEqual( mean1, sum1 / gaussians1.Count, ACCEPTABLE_ERROR );
            Assert.AreEqual( mean2, sum2 / gaussians2.Count, ACCEPTABLE_ERROR );
            Assert.AreEqual( mean3, sum3 / gaussians3.Count, ACCEPTABLE_ERROR );
        }

        [TestMethod]
        public void NextGaussian()
        {
            // given
            const int CYCLES = 10_000;
            double min1 = -1.0;
            double min2 = 111.23;
            double max1 = 1.0;
            double max2 = 999.67;
            IList<double> gaussians1 = new List<double>();
            IList<double> gaussians2 = new List<double>();
            double sum1 = 0.0;
            double sum2 = 0.0;

            // when
            for ( int i = 0; i < CYCLES; i++ ) {
                gaussians1.Add( Randoms.NextGaussian( min1, max1 ) );
                gaussians2.Add( Randoms.NextGaussian( min2, max2 ) );
            }

            gaussians1.ToList().ForEach( v => sum1 += v );
            gaussians2.ToList().ForEach( v => sum2 += v );

            // then
            Assertions.DoubleWithin( gaussians1, min1, max1 );
            Assertions.DoubleWithin( gaussians2, min2, max2 );
            Assert.AreEqual( (max1 + min1) / 2.0, sum1 / gaussians1.Count, 1.0E-2 );
            Assert.AreEqual( (max2 + min2) / 2.0, sum2 / gaussians2.Count, 10.0 );
        }
    }
}
