using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        public void LimitedQueueEnqueueMethodDequeuesElementsFromQueue()
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

            var arr = queue.ToArray();

            Assert.AreEqual(5, arr[0]);
            Assert.AreEqual(8, arr[1]);
            Assert.AreEqual(10, arr[2]);
        }
    }
}