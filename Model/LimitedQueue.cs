namespace Vsite.Oom.Battleship.Model
{
    public class LimitedQueue<T> : Queue<T>
    {
        private int maxLenght;
        public LimitedQueue(int maxLenght)
        {
             this.maxLenght = maxLenght;
        }

        public new void Enqueue(T item)
        {
            base.Enqueue(item);
            if (this.Count() > maxLenght)
            {
                this.Dequeue();
            }
        }
    }
}
