
namespace Vsite.Oom.Battleship.Model
{
    public class Grid
    {
        public readonly int Rows;
        public readonly int Columns;

        private readonly Square?[,] _squares;
        public IEnumerable<Square> Squares
        {
            get { return _squares.Cast<Square>().Where(x => x != null); }
        }


        public Grid(int rows, int columns)
        {
            this.Rows = rows;
            this.Columns= columns;

            _squares = new Square[rows, columns];

            for (int r = 0; r < rows; r++)
                for (int c = 0; c < columns; c++)
                    _squares[r, c] = new Square(r, c);

        }

        public IEnumerable<IEnumerable<Square>> GetAvailablePlacements(int shipSize)
        {
            return SearchForAvailablePlacements(shipSize, (r, c) => _squares[r, c], Rows, Columns)
                .Concat(SearchForAvailablePlacements(shipSize, (r, c) => _squares[c, r], Columns, Rows));
        }

        /// <summary>
        /// Function that has function delegate to access grid squares.
        /// In above call we first get horizontal positions, then we transpose (r -> c, c -> r) our 2d matrix with Squares,
        /// and get positions for new matrix. (in short, we iterate 2 times trough rows to get positions, first time for horizontal positions,
        /// but second time we transpose matrix for vertical positions)
        /// </summary>
        /// <param name="shipSize"></param>
        /// <param name="getSquare"></param>
        /// <param name="outerLimit"></param>
        /// <param name="innerLimit"></param>
        /// <returns></returns>
        private IEnumerable<IEnumerable<Square>> SearchForAvailablePlacements(int shipSize, Func<int, int, Square?> getSquare, int outerLimit, int innerLimit)
        {
            List<IEnumerable<Square>> result = new List<IEnumerable<Square>>();

            for (int outerIndex = 0; outerIndex < outerLimit; outerIndex++)
            {
                //Limiting queue for size of ship
                var limitedQueue = new LimitedQueue<Square>(shipSize);

                //Algorithm for iterating trough row from left to right
                for (int innerIndex = 0; innerIndex < innerLimit; innerIndex++)
                {
                    //Function delegate to access square on [x,y] position
                    Square? square = getSquare(outerIndex, innerIndex);

                    if (square != null)
                    {
                        limitedQueue.Enqueue(square);

                        if (limitedQueue.Count() == shipSize)
                            result.Add(limitedQueue.ToArray());
                    }
                    else
                    {
                        limitedQueue.Clear();
                    }
                }
            }
            return result;
        }

        public void EleminateSquare(int row, int column)
        {
            _squares[row, column] = null;
        }


    }
}
