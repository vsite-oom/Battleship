using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class LimitedQueue<T>(int maxItems) : Queue<T>
    {
        private readonly int maxItems = maxItems;

        public new void Enqueue(T item)
        {
            if(Count >= maxItems)
                Dequeue();
            base.Enqueue(item);
        }
        
    }
}
