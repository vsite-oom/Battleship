using Model;

namespace ModelTests
{
    [TestClass]
    public class LimitedQueueTests
    {
        [TestMethod]
        public void Enqueue_RespectsMaxSize()
        {
            var queue = new LimitedQueue<int>(3);
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            queue.Enqueue(4);

            Assert.AreEqual(3, queue.Count);
            Assert.AreEqual(2, queue.Peek());
        }

        [TestMethod]
        public void Enqueue_BelowLimit_KeepsAll()
        {
            var queue = new LimitedQueue<int>(5);
            queue.Enqueue(10);
            queue.Enqueue(20);

            Assert.AreEqual(2, queue.Count);
        }
    }
}
