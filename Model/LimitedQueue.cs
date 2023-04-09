using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class LimitedQueue<T> : Queue<T>
    {
        private readonly int maxLength;
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
    }
}
