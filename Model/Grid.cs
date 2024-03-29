
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
                int counter = 0;
                for (int c = 0; c < columns; c++)
                {
                    if (_squares[r, c] != null)
                    {
                        counter++;
                        if (counter >= shipSize)
                        {
                            List<Square> temp = new List<Square>();
                            for (int cl = c - shipSize + 1; cl <= c; cl++)
                            {
                                temp.Add(_squares[r, cl]!);
                            }
                            result.Add(temp);
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


        private IEnumerable<IEnumerable<Square>> GetVerticalAvailablePlacements(int shipSize)
        {
            List<IEnumerable<Square>> result = new List<IEnumerable<Square>>();

            for (int c = 0; c < columns; c++)
            {
                int counter = 0;
                for(int r = 0; r < rows; r++)
                {
                    if (_squares[r, c] != null)
                    {
                        counter++;
                        if(counter >= shipSize)
                        {
                            List<Square> temp = new();
                            for(int r1 = r - shipSize + 1; r1 <= r; r1++)
                            {
                                temp.Add(_squares[r1, c]!);
                            }
                            result.Add(temp);
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
