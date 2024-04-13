using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.OOM.Battleship
{
    public class LimitedQueue<T> : Queue<T>
    {
        public LimitedQueue(int maxItems)
        {
            this.maxItems = maxItems;
        }
        private int maxItems;

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