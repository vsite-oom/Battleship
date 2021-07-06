using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Battleships
{
    public class BlockingQueue<T>
    {
        private readonly Queue<T> queue = new Queue<T>();
        public BlockingQueue() { }

        public void Close()
        {
            lock(queue)
            {
                while (queue.Count != 0)
                {
                    queue.Dequeue();
                }
            }

        }

        public void Enqueue(T item)
        {
            lock (queue)
            {
                queue.Enqueue(item);

                // wake up any blocked dequeue
                Monitor.PulseAll(queue);
            }
        }

        public T Dequeue()
        {
            lock (queue)
            {
                while (queue.Count == 0)
                {
                    Monitor.Wait(queue);
                }
                
                return queue.Dequeue();
            }
        }
    }
}
