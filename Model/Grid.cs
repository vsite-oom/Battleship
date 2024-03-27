using System.Reflection.Metadata.Ecma335;

namespace Vsite.Oom.Battleship.Model
{
    public class Grid
    {
        public readonly int Rows;
        public readonly int Columns;
        private readonly Square?[,] squares;

        public Grid(int rows, int columns)
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

        public IEnumerable<Square> Squares
        {
            get
            {
                return squares.Cast<Square>().Where(s => s != null);
            }
        }
        
        public IEnumerable<IEnumerable<Square>> GetAvailablePlacements(int length)
        {
            return GetHorizonalAvailablePlacements(length);
        }

        private IEnumerable<IEnumerable<Square>> GetHorizonalAvailablePlacements(int length)
        {
            List<IEnumerable<Square>> result = new();

            for (int r = 0; r < Rows; r++)
            {
                int counter = 0;
                for (int c = 0; c < Columns; c++)
                {
                    if (squares[r, c] != null)
                    {
                        ++counter;
                        
                        if(counter >= length)
                        {
                            List<Square> temp = new();
                            for(int c1 = c - length + 1; c1 <= c; ++c1)
                            {
                                temp.Add(squares[r, c1]!);
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
