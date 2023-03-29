using System.Security.Cryptography.X509Certificates;
using Vsite.Oom.Battleship.Model;

namespace ModelTests
{
    [TestClass]
    public class LimitedQueueTests
    {
        [TestMethod]
        public void EnqueueNewElement()
        {
            var queue = new LimitedQueue<int>(3);
            Assert.AreEqual(0, queue.Count);

            queue.Enqueue(22);
            Assert.AreEqual(1, queue.Count);

            queue.Enqueue(44);
            Assert.AreEqual(2, queue.Count);

            queue.Enqueue(66);
            Assert.AreEqual(3, queue.Count);
        }

        [TestMethod]
        public void RemovesExcessElement()
        {
            var queue = new LimitedQueue<int>(3);
            Assert.AreEqual(0, queue.Count);

            queue.Enqueue(22);
            Assert.AreEqual(1, queue.Count);

            queue.Enqueue(44);
            Assert.AreEqual(2, queue.Count);

            queue.Enqueue(66);
            Assert.AreEqual(3, queue.Count);

            queue.Enqueue(88);
            Assert.AreEqual(3, queue.Count);

            Assert.IsFalse(queue.Contains(22));
            Assert.IsTrue(queue.Contains(44));
            Assert.IsTrue(queue.Contains(66));
            Assert.IsTrue(queue.Contains(88));
        }
    }
}