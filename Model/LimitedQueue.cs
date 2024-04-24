using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class LimitedQueue<T> : Queue<T>
    {
        public LimitedQueue(int maxItems)  // Konstruktor prima duljinu queuea (duljinu broda).
        {
            this.maxItems = maxItems;
        }
        
        private readonly int maxItems;

        // Ako ne napišemo override, onda u potpunosti skrivamo
        // istoimenu metodu iz bazne klase, ali u tom slučaju
        // trebamo napisati 'new' da ne dobivamo warning:
        public new void Enqueue(T item)
        {
            while (Count >= maxItems)  // Count je property klase Queue.
            {
                Dequeue();
            }
            base.Enqueue(item);
        }
    }
}
