using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    // "SquareEliminator ne zna za Squareove koji su okolo. Umjesto da te okolne stvaramo preko konstruktora, mi ćemo umjesto Squareova vraćati parove redak, stupac."
    // Za to u Model moramo dodati strukturu SquareCoordinate.
    // Prijašnjih godina su se koristili Squareovi, ali to je kompliciralo kod."
    public class SquareEliminator
    {
        public  IEnumerable<SquareCoordinate> ToEliminate(List<Square> shipSquares, int rows, int columns)
        {
            // Grid vraća već sortirane Squareove od najlijevijeg, najgornjeg, prema dolje, pa ovdje možemo pretpostaviti da će nam prvi imati najmanje koordinate.
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
            if (lastRow < rows - 1)
            {
                ++lastRow;
            }            
            if (lastColumn < columns - 1)
            {
                ++lastColumn;
            }

            var result = new List<SquareCoordinate>();
            for (int r = firstRow; r <= lastRow; ++r)
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
