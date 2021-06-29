using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Vsite.Oom.Battleship.Model.UnitTests
{
    [TestClass]
    public class TestLimitedQueue
    {
        [TestMethod]
        public void EnqueueMethodAddsElementsToTheQueue()
        {
            LimitedQueue<int> queue = new LimitedQueue<int>(3);
            queue.Enqueue(1);
            Assert.IsTrue(queue.Contains(1));
            queue.Enqueue(2);
            Assert.IsTrue(queue.Contains(2));
            queue.Enqueue(3);
            Assert.IsTrue(queue.Contains(1));
            Assert.IsTrue(queue.Contains(2));
            Assert.IsTrue(queue.Contains(3));
        }

        [TestMethod]
        public void EnqueueMethodRemovesExcessiveElementsFromTheQueue()
        {
            LimitedQueue<int> queue = new LimitedQueue<int>(3);
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            queue.Enqueue(4);
            Assert.IsFalse(queue.Contains(1));
            Assert.IsTrue(queue.Contains(2));
            Assert.IsTrue(queue.Contains(3));
            Assert.IsTrue(queue.Contains(4));
        }
    }
}
