namespace Vsite.Oom.Battleship.Model
{
    public class LimitedQueue<T> : Queue<T>
    {
        public LimitedQueue(int maxLenght)
        {
            this.maxLenght = maxLenght;
        }

        public new void Enqueue(T item)
        {
            base.Enqueue(item);
            if (Count > maxLenght)
            {
                Dequeue();
            }
        }
        private readonly int maxLenght;
    }
}
