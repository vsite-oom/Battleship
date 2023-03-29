namespace Vsite.Oom.Battleship.Model
{
    public class LimitedQueue<T> : Queue<T>
    {
        private readonly int _maxLength;

        public LimitedQueue(int maxLength)
        {
            _maxLength = maxLength;
        }

        public new void Enqueue(T item) 
        {
            base.Enqueue(item);

            if (Count > _maxLength)
            {
                Dequeue();
            }
        }
    }
}
