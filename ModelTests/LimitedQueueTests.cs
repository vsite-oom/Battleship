using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vsie.Oom.Battleship.Model;

namespace ModelTests
{
    [TestClass]
    public class LimitedQueueTests
    {
        [TestMethod]
        public void EnqueuAddsNewElement()
        {
            var queue = new LimitedQueue<int>(3);
            Assert.AreEqual(0, queue.Count);

            queue.Enqueue(1);
            Assert.AreEqual(1, queue.Count);

            queue.Enqueue(3);
            Assert.AreEqual(2, queue.Count);

            queue.Enqueue(5);
            Assert.AreEqual(3, queue.Count);

        }
        [TestMethod]
        public void EnqueuRemovessExcessiveElement()
        {
            var queue = new LimitedQueue<int>(3);
            Assert.AreEqual(0, queue.Count);

            queue.Enqueue(1);
            Assert.AreEqual(1, queue.Count);

            queue.Enqueue(3);
            Assert.AreEqual(2, queue.Count);

            queue.Enqueue(5);
            Assert.AreEqual(3, queue.Count);

            queue.Enqueue(7);
            Assert.AreEqual(3, queue.Count);

            Assert.IsTrue(queue.Contains(3));
            Assert.IsTrue(queue.Contains(5));
            Assert.IsTrue(queue.Contains(7));
            Assert.IsFalse(queue.Contains(1));
        }
    }
}