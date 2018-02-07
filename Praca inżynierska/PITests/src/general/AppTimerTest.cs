using Microsoft.VisualStudio.TestTools.UnitTesting;
using PI.src.general;
using System.Collections.Generic;
using System.Threading;

namespace PITests.src.general
{
    [TestClass]
    public class AppTimerTest
    {
        [TestMethod]
        public void Ctor()
        {
            // given
            AppTimer timer1 = new AppTimer();
            IList<string> times1 = new List<string>();

            // when
            timer1.OnCount += ( object sender, OnCountEventArgs e ) => { times1.Add( e.Time ); };
            timer1.Start();
            Thread.Sleep( 6_000 );
            timer1.Start();

            // then
            Assert.IsTrue( times1.Count == 5 );
        }
    }
}
