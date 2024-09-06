using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    // "SquareEliminator ne zna za Squareove koji su okolo. Umjesto da te okolne stvaramo preko
    // konstruktora, mi ćemo umjesto Squareova vraćati parove redak, stupac."
    // Za to u Model moramo dodati strukturu SquareCoordinate.
    // Prijašnjih godina su se koristili Squareovi, ali to je kompliciralo kod (vidi L6)."
    public class SquareEliminator
    {
        // Moramo proslijediti i broj redaka i stupaca jer ne znamo veličinu polja.
        // shipSquares moraju biti sortirani da bi ova metoda radila kako treba.
        public IEnumerable<SquareCoordinate> ToEliminate(IEnumerable<Square> shipSquares, int rows, int columns)
        {
            var first = shipSquares.First();
            int firstRow = first.Row;
            int firstColumn = first.Column;
            if (firstRow > 0)
            {
                --firstRow;  // Ako je prvi redak veći od 0, onda možemo eliminirati i kvadrat iznad njega.
            }
            if (firstColumn > 0)
            {
                --firstColumn;  // Ako je prvi stupac veći od 0, onda možemo eliminirati i kvadrat lijevo od njega.
            }

            var last = shipSquares.Last();
            int lastRow = last.Row;
            int lastColumn = last.Column;
            if (lastRow < rows - 1)
            {
                ++lastRow;  // Ako je zadnji redak manji od broja redaka, onda možemo eliminirati i kvadrat ispod njega.
            }            
            if (lastColumn < columns - 1)
            {
                ++lastColumn;  // Ako je zadnji stupac manji od broja stupaca, onda možemo eliminirati i kvadrat desno od njega.
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
