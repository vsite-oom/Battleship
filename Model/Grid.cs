using System.Data;

namespace Vsite.Oom.Battleship.Model
{
    using Sequences = IEnumerable<IEnumerable<Square>>;
    using SquareAccess = Func<int, int, Square>;
    using SquareSequence = IEnumerable<Square>;

    public enum Direction
    {
        Upwards,
        Rightwards,
        Downwards,
        Leftwards
    }

    public abstract class Grid
    {
        protected Grid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            squares = new Square[Rows, Columns];
            for (int r = 0; r < Rows; ++r)
            {
                for (int c = 0; c < Columns; ++c)
                {
                    squares[r, c] = new Square(r, c);
                }
            }
        }

        public readonly int Rows;
        public readonly int Columns;

        protected readonly Square[,] squares;

        public SquareSequence AvailableSquares()
        {
            return squares.Cast<Square>().Where(sq => IsAvailable(sq));
        }

        public Sequences GetAvailableSequences(int length)
        {
            var result = GetAvailableHorizontalSequences(length);
            if (length == 1)
            {
                return result;
            }
            return GetAvailableHorizontalSequences(length).Concat(GetAvailableVerticalSequences(length));
        }

        private Sequences GetAvailableHorizontalSequences(int length)
        {
            return GetAvailableSequences(Rows, Columns, (a, b) => squares[a, b], length);
        }

        private Sequences GetAvailableVerticalSequences(int length)
        {
            return GetAvailableSequences(Columns, Rows, (a, b) => squares[b, a], length);
        }

        protected abstract bool IsAvailable(Square square);


        private Sequences GetAvailableSequences(int outerLoopLimit, int innerLoopLimit, SquareAccess squareaccess, int length)
        {
            var result = new List<SquareSequence>();
            for (int o = 0; o < outerLoopLimit; ++o)
            {
                var queue = new LimitedQueue<Square>(length);
                for (int i = 0; i < innerLoopLimit; ++i)
                {
                    if (IsAvailable(squareaccess(o, i)))
                    {
                        queue.Enqueue(squareaccess(o, i));
                        if (queue.Count == length)
                            result.Add(queue.ToArray());
                    }
                    else
                        queue.Clear();
                }
            }
            return result;
        }
    }
}
