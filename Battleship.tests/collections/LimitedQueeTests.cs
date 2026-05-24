using Battleship.Model;
namespace Battleship.tests;

[TestClass]
public sealed class LimitedQueueTests
{
    [TestMethod]
    public void EnqueueInsertsItemToQueue()
    {
        var queue = new LimitedQueue<int>(3);

        Assert.IsEmpty(queue);

        queue.Enqueue(1);

        Assert.HasCount(1, queue);

        queue.Enqueue(3);

        Assert.HasCount(2, queue);
    }

    [TestMethod]
    public void QueueRemovesExtraItemsAfterItIsFilled()
    {
        var queue = new LimitedQueue<int>(3);
        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);

        Assert.HasCount(3, queue);

        queue.Enqueue(4);
        Assert.HasCount(3, queue);
        Assert.DoesNotContain(1, queue);

        queue.Enqueue(5);
        Assert.HasCount(3, queue);
        Assert.DoesNotContain(2, queue);
    }
}