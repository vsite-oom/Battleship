using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class SquareEliminator
    {
        public IEnumerable<SquareCoordinate> ToEliminate(IEnumerable<Square> shipSquares, int row, int columns)
        {
            var first = shipSquares.First();
            int firstRow = first.Row;
            int firstColumn = first.Column;
            if (firstRow > 0)
            {
                --firstRow;
            }
            if (firstColumn > 0)
            {
                --firstColumn;
            }

            var last = shipSquares.Last();
            int lastRow = last.Row;
            int lastColumn = last.Column;
            if (lastRow < row - 1)
            {
                ++lastRow;
            }
            if (lastColumn < columns - 1)
            {
                ++lastColumn;
            }
            var result = new List<SquareCoordinate>();
            for(int r= firstRow; r <= lastRow; ++r)
            {
                for (int c = firstColumn; c <= lastColumn; ++c)
                {
                    result.Add(new SquareCoordinate(r, c));
                }
            }
            return result;
        }
    }
}