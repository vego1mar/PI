using Microsoft.VisualStudio.TestTools.UnitTesting;
using PI.src.helpers;
using System.Threading;

namespace PITests.src.helpers
{
    [TestClass]
    public class ThreadsTest
    {
        [TestMethod]
        public void TryStart()
        {
            // given
            Thread thread1 = new Thread( () => { } );

            // when
            bool result1 = Threads.TryStart( null );
            bool result2 = Threads.TryStart( thread1 );

            // then
            Assert.IsFalse( result1 );
            Assert.IsTrue( result2 );
        }
    }
}
