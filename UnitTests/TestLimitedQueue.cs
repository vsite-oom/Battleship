using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Vsite.Oom.Battleship.Model.UnitTests
{
    [TestClass]
    public class TestLimitedQueue
    {
        [TestMethod]
        public void LimitedQueueEnqueueMethodAddsElementsToQueue()
        {
            LimitedQueue<int> queue = new LimitedQueue<int>(3);
            queue.Enqueue(1);
            Assert.AreEqual(1, queue.Count);
            queue.Enqueue(5);
            Assert.AreEqual(2, queue.Count);
            queue.Enqueue(8);
            Assert.AreEqual(3, queue.Count);
        }

        [TestMethod]
        public void LimitedQueueEnqueueMethodDequeuesExcessElementsFromQueue()
        {
            LimitedQueue<int> queue = new LimitedQueue<int>(3);
            queue.Enqueue(1);
            queue.Enqueue(5);
            queue.Enqueue(8);

            queue.Enqueue(10);
            Assert.AreEqual(3, queue.Count);
            Assert.AreEqual(5, queue.Peek());
        }

        [TestMethod]
        public void LimitedQueueToArrayMethodReturnsCurrentElements()
        {
            LimitedQueue<int> queue = new LimitedQueue<int>(3);
            queue.Enqueue(1);
            queue.Enqueue(5);
            queue.Enqueue(8);

            queue.Enqueue(10);
            var array = queue.ToArray();
            Assert.AreEqual(5, array[0]);
            Assert.AreEqual(8, array[1]);
            Assert.AreEqual(10, array[2]);
        }
    }
}
