using Microsoft.VisualStudio.TestTools.UnitTesting;
using PI.src.general;
using PITests.src.tests;
using System.Collections.Generic;

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
    }
}
