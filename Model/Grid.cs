namespace Vsite.Oom.Battleship.Model
{
    using SquareSequence = IEnumerable<Square>;
    using Sequences = IEnumerable<IEnumerable<Square>>;
    public class Grid
    {
        public readonly int Rows;
        public readonly int Columns;

        private readonly Square[,] squares;

        public Grid(int rows, int columns)
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

        public IEnumerable<Square> AvalibleSquares
        {
            get
            {
                return squares.Cast<Square>();
            }
        }
        public IEnumerable<IEnumerable<Square>> GetAvailableSequences(int length)
        {
            return GetAvailableHorizontalSequences(length).Concat(GetAvailableVerticalSequences(length));
        }
        private Sequences GetAvailableHorizontalSequences(int length)
        {
            var result = new List<SquareSequence>();
            for (int r = 0; r < Rows; ++r)
            {
                int counter = 0;
                for (int c = 0; c < Columns; ++c)
                {
                    if (squares[r, c] != null)
                    {
                        ++counter;
                        if (counter >= length)
                        {
                            // Add previous squares to result
                            var squaresToAdd = new List<Square>();
                            for (int cc = c - length + 1; cc <= c; ++cc)
                            {
                                squaresToAdd.Add(squares[r, cc]);
                            }
                            result.Add(squaresToAdd);
                        }
                    }
                    else
                    {
                        counter = 0;
                    }
                }
            }
            return result;
        }
        private Sequences GetAvailableVerticalSequences(int length)
        {
            var result = new List<SquareSequence>();
            for (int c = 0; c < Columns; ++c)
            {
                int counter = 0;
                for (int r = 0; r < Rows; ++r)
                {
                    if (squares[r, c] != null)
                    {
                        ++counter;
                        if (counter >= length)
                        {
                            // Add previous squares to result
                            var squaresToAdd = new List<Square>();
                            for (int rr = r - length + 1; rr <= r; ++rr)
                            {
                                squaresToAdd.Add(squares[rr, c]);
                            }
                            result.Add(squaresToAdd);
                        }
                    }
                    else
                    {
                        counter = 0;
                    }
                }
            }
            return result;
        }

    }
}

