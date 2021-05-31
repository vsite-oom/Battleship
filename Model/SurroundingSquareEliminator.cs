using System;
using System.Collections.Generic;
using System.Linq;

namespace Vsite.Oom.Battleship.Model
{
    public class SurroundingSquareEliminator : ISquareEliminator
    {
        public SurroundingSquareEliminator(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;
        }
        public IEnumerable<Square> ToEliminate(IEnumerable<Square> shipSquares)
        {
            int rowMin = Math.Max(shipSquares.Min(s => s.Row) - 1, 0);
            int columnMin = Math.Max(shipSquares.Min(s => s.Column) - 1, 0);
            int rowMax = Math.Min(shipSquares.Max(s => s.Row) + 2, rows);
            int columnMax = Math.Min(shipSquares.Max(s => s.Column) + 2, columns);

            List<Square> squares = new List<Square>();
            for (int r = rowMin; r < rowMax; ++r)
            {
                for (int c = columnMin; c < columnMax; ++c)
                    squares.Add(new Square(r, c));
            }
            return squares;
        }
        private readonly int rows;
        private readonly int columns;
    }
}
