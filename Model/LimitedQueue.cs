using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class LimitedQueue<T> : Queue<T>
    {
        public LimitedQueue(int maxItems)
        {
            this.maxItems = maxItems;
        }

        private readonly int maxItems;

        public new void Enqueue(T item)
        {
            while (Count >= maxItems)
            {
                Dequeue();
            }
            base.Enqueue(item);
        }
    }
}