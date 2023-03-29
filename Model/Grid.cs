namespace Vsite.Oom.Battleship.Model
{
    using SquareSequence = IEnumerable<Square>;
    using Sequences = IEnumerable<IEnumerable<Square>>;

    public class Grid
    {
        public Grid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            squares = new Square[Rows, Columns];
            for(int r = 0; r < Rows; ++r)
            {
                for(int c = 0; c < Columns; ++c)
                {
                    squares[r, c] = new Square(r, c);
                }
            }
        }

        public readonly int Rows;
        public readonly int Columns;

        private readonly Square[,] squares;

        public SquareSequence AvailableSquares()
        {
            return squares.Cast<Square>();
            //throw new NotImplementedException();
        }

        public void RemoveSquare(int row, int column)
        {
            squares[row, column] = null;
        }

        public Sequences GetAvailableSequences(int length)
        {
            return GetAvailableHorizontalSequences(length).Concat(GetAvailableVerticalSequences(length));
        }

        private Sequences GetAvailableHorizontalSequences(int length) 
        { 
            var result = new List<SquareSequence>();
            for (int r = 0; r < Rows; ++r)
            {
                var queue = new LimitedQueue<Square>(length);
                for (int c = 0; c < Columns; ++c)
                {
                    if (squares[r, c] != null)
                    {
                        queue.Enqueue(squares[r, c]);
                        if (queue.Count == length)
                            result.Add(queue.ToArray());
                    }
                    else
                        queue.Clear();
                }
            }
            return result;
        }
        
        private Sequences GetAvailableVerticalSequences(int length) 
        {
            var result = new List<SquareSequence>();
            return result;
        }
    }
}
