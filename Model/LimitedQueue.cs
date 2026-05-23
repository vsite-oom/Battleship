using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model;

public class LimitedQueue<T> : Queue<T>
{
    private readonly int _maxItems;
    public LimitedQueue(int maxItems)
    {
        _maxItems = maxItems;
    }
    public new void Enqueue(T item)
    {
        while (Count >= _maxItems)
        {
            Dequeue();
        }
        base.Enqueue(item);
    }
}
