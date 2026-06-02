namespace Battleship.Model;

/// <summary>Represents a queue with a fixed maximum capacity that discards the oldest item when full.</summary>
/// <typeparam name="T">The type of elements in the queue.</typeparam>
public class LimitedQueue<T> : Queue<T>
{
    private readonly int _maxItems;

    /// <summary>Initializes a new instance of the <see cref="LimitedQueue{T}"/> class.</summary>
    /// <param name="maxItems">The maximum number of items the queue can hold.</param>
    public LimitedQueue(int maxItems)
    {
        _maxItems = maxItems;
    }

    /// <summary>Adds an item to the end of the queue, removing the oldest item first if the queue is at capacity.</summary>
    /// <param name="item">The item to add.</param>
    public new void Enqueue(T item)
    {
        while (Count >= _maxItems)
        {
            Dequeue();
        }
        base.Enqueue(item);
    }
}
