
namespace Vsite.Oom.Battleship.Model
{
    public abstract class Grid
    {
        public readonly int Rows;
        public readonly int Columns;
        protected readonly Square?[,] squares;

        protected Grid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;

            squares = new Square[Rows, Columns];

            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    squares[r, c] = new Square(r, c);
                }
            }
        }

        public virtual IEnumerable<Square> Squares
        {
            get => squares.Cast<Square>();
        }

        protected abstract bool IsSquareAvailable(int row, int column);

        public IEnumerable<IEnumerable<Square>> GetAvailablePlacements(int length)
        {

            return GetAvailablePlacements(length, true).Concat(GetAvailablePlacements(length, false));
        }
        private IEnumerable<IEnumerable<Square>> GetAvailablePlacements(int length, bool isHorizontal)
        {
            List<IEnumerable<Square>> result = new();

            for (int i = 0; i < (isHorizontal ? Rows : Columns); i++)
            {
                var queue = new LimitedQueue<Square>(length);

                for (int j = 0; j < (isHorizontal ? Columns : Rows); j++)
                {
                    int rowId = isHorizontal ? i : j;
                    int colId = isHorizontal ? j : i;

                    if (IsSquareAvailable(rowId, colId))
                    {
                        queue.Enqueue(squares[rowId, colId]!);

                        if (queue.Count >= length)
                        {
                            result.Add(queue.ToArray());
                        }
                    }
                    else
                    {
                        queue.Clear();
                    }
                }
            }

            return result;
        }
    }
}
