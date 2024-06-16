using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vsite.Oom.Battleship.Model;

namespace vsite.oom.battleship.model
{
    public abstract class Grid
    {
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



        public readonly int Rows;
        public readonly int Columns;

        protected readonly Square?[,] squares;

        public virtual IEnumerable<Square> Squares
        {
            get { return squares.Cast<Square>().Where(s => s != null); }
        }
        protected abstract bool IsSquareAvailable(int row, int column);

        public IEnumerable<IEnumerable<Square>> GetAvailablePlacements(int length)
        {

            return GetHorizontalAvailablePlacements(length).Concat(GetVerticalAvailablePlacements(length));
        }

        private IEnumerable<IEnumerable<Square>> GetHorizontalAvailablePlacements(int length)
        {
            List<IEnumerable<Square>> result = new List<IEnumerable<Square>>();
            for (int r = 0; r < Rows; r++)
            {
                int counter = 0;
                for (int c = 0; c < Columns; c++)
                {
                    if (IsSquareAvailable(r, c))
                    {
                        ++counter;
                        if (counter >= length)
                        {
                            List<Square> temp = new List<Square>();
                            for (int c1 = c - length + 1; c1 <= c; ++c1)
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
        private IEnumerable<IEnumerable<Square>> GetVerticalAvailablePlacements(int length)
        {
            List<IEnumerable<Square>> result = new List<IEnumerable<Square>>();
            for (int c = 0; c < Columns; c++)
            {
                int counter = 0;
                for (int r = 0; r < Rows; r++)
                {
                    if (IsSquareAvailable(r, c))
                    {
                        ++counter;
                        if (counter >= length)
                        {
                            List<Square> temp = new List<Square>();
                            for (int r1 = r - length + 1; r1 <= r; ++r1)
                            {
                                temp.Add(squares[r1, c]!);
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
