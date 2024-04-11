using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vsite.Oom.Batelship.Model;

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
            return GetHorizontalAvailablePlacements(length).Concat(GetVerticalAvailablePlacements(length));
        }

        private IEnumerable<IEnumerable<Square>> GetAvailablePlacements(int length, bool isHorizontal)
        {
            List<IEnumerable<Square>> result = new List<IEnumerable<Square>>();

            for (int i = 0; i < (isHorizontal ? Rows : Columns); ++i)
            {
                var queue = new LimitedQueue<Square>(length);

                for (int j = 0; j < (isHorizontal ? Columns : Rows); ++j)
                {
                    int row = isHorizontal ? i : j;
                    int col = isHorizontal ? j : i;

                    if (squares[row, col] != null)
                    {
                        queue.Enqueue(squares[row, col]!);
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

        public IEnumerable<IEnumerable<Square>> GetHorizontalAvailablePlacements(int length)
        {
            return GetAvailablePlacements(length, true);
        }

        public IEnumerable<IEnumerable<Square>> GetVerticalAvailablePlacements(int length)
        {
            return GetAvailablePlacements(length, false);
        }






        public void EliminateSquare(int row, int column)
        {
            squares[row, column] = null;
        }
    }
}
