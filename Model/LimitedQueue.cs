namespace Vsite.Oom.Battleship.Model
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
