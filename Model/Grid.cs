
namespace Vsite.Oom.Battleship.Model
{
    public class Grid
    {
        public readonly int rows;
        public readonly int columns;

        private readonly Square?[,] _squares;
        public IEnumerable<Square> Squares
        {
            get { return _squares.Cast<Square>().Where(x => x != null); }
        }


        public Grid(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;

            _squares = new Square[rows, columns];

            for (int r = 0; r < rows; r++)
                for (int c = 0; c < columns; c++)
                    _squares[r, c] = new Square(r, c);

        }

        public IEnumerable<IEnumerable<Square>> GetAvailablePlacements(int shipSize)
        {

            return GetHorizontalAvailablePlacements(shipSize).Concat(GetVerticalAvailablePlacements(shipSize));
        }

        private IEnumerable<IEnumerable<Square>> GetHorizontalAvailablePlacements(int shipSize)
        {
            List<IEnumerable<Square>> result = new List<IEnumerable<Square>>();

            for (int r = 0; r < rows; r++)
            {
                var limitedQueue = new LimitedQueue<Square>(shipSize);
                for (int c = 0; c < columns; c++)
                {
                    if (_squares[r, c] != null)
                    {
                        limitedQueue.Enqueue(_squares[r, c]!);
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


        private IEnumerable<IEnumerable<Square>> GetVerticalAvailablePlacements(int shipSize)
        {
            List<IEnumerable<Square>> result = new List<IEnumerable<Square>>();

            for (int c = 0; c < columns; c++)
            {
                var limitedQueue = new LimitedQueue<Square>(shipSize);
                for (int r = 0; r < rows; r++)
                {
                    if (_squares[r, c] != null)
                    {
                        limitedQueue.Enqueue(_squares[r, c]!);
                        if (limitedQueue.Count() >= shipSize)
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

        private IEnumerable<IEnumerable<Square>> GetBidirectionalPositions(int shipSize)
        {
            List<IEnumerable<Square>> result = new List<IEnumerable<Square>>();

            var x = columns;
            var y = rows;

            //HomeWork -> one method for everything :) 

            throw new NotImplementedException();
        }
    }
}
