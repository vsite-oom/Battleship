namespace Vsite.Oom.Battleship.Model;

public class LimitedQueue<T>(int maxItems) : Queue<T>
{
    private readonly int maxItems = maxItems;

    public new void Enqueue(T item)
    {
        if (Count >= maxItems)
            Dequeue();
        base.Enqueue(item);
    }
}