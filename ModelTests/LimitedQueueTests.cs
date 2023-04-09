using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Vsite.Oom.Battleship.Model;

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
            queue.Enqueue(2);
            queue.Enqueue(3);
            Assert.AreEqual(3, queue.Count);

        }
        [TestMethod]
        public void EnqueuRemovesOverflow()
        {
            var queue = new LimitedQueue<int>(3);
            Assert.AreEqual(0, queue.Count);
            queue.Enqueue(1);
            Assert.AreEqual(1, queue.Count);
            queue.Enqueue(2);
            queue.Enqueue(3);
            Assert.AreEqual(3, queue.Count);
            queue.Enqueue(4);
            Assert.AreEqual(3, queue.Count);
            Assert.IsTrue(queue.Contains(2));
            Assert.IsTrue(queue.Contains(3));
            Assert.IsTrue(queue.Contains(4));
            Assert.IsFalse(queue.Contains(1));
        }
    }
}