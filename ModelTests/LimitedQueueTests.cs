using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Vsite.Oom.Battleship.Model.Tests
{
    [TestClass]
    public class LimitedQueueTests
    {
        [TestMethod]
        public void EnqueueInsertsItemToQueue()
        {
            var queue = new LimitedQueue<int>(3);

            Assert.AreEqual(0, queue.Count);

            queue.Enqueue(1);
            Assert.AreEqual(1, queue.Count);

            queue.Enqueue(2);
            Assert.AreEqual(2, queue.Count);
        }

        [TestMethod]
        public void QueueRemovesExtraItemsAfterItIsFilled()
        {
            var queue = new LimitedQueue<int>(3);

            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);

            Assert.AreEqual(3, queue.Count);

            queue.Enqueue(4);
            Assert.AreEqual(3, queue.Count);            
            Assert.IsFalse(queue.Contains(1));

            queue.Enqueue(5);
            Assert.AreEqual(3, queue.Count);
            Assert.IsFalse(queue.Contains(1));
            Assert.IsFalse(queue.Contains(2));
        }
    }
}