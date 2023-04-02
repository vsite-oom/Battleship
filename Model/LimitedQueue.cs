using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsie.Oom.Battleship.Model
{
    public class LimitedQueue<T> : Queue<T>
    {
        public LimitedQueue(int maxLength)
        {
            this.maxLength = maxLength;
        }

        public new void Enqueue(T item)
        {
            base.Enqueue(item);
            if (Count > maxLength)
            {
                Dequeue();
            }
        }

        private readonly int maxLength;
    }
}
