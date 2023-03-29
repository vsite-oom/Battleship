using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class LimitedQueue<T> : Queue<T>
    {
        public LimitedQueue(int maxLength)
        {
            this.maxLength = maxLength;
        }
        public new void Enque(T item)
        {
            base.Enqueue(item);
            if (this.Count > maxLength)
            {
                Dequeue();
            }
        }
}
