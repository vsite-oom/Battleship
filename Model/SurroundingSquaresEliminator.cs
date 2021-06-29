using System;
using System.Collections.Generic;
using System.Linq;

namespace Vsite.Oom.Battleship.Model
{
    public class SurroundingSquaresEliminator : ISquareEliminator
    {
        private readonly int rows, columns;

        public SurroundingSquaresEliminator(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;
        }

        public IEnumerable<Square> ToEliminate(IEnumerable<Square> shipSquares)
        {
            int rowMin = Math.Max(shipSquares.Min(s => s.row) - 1, 0);
            int columnMin = Math.Max(shipSquares.Min(s => s.column) - 1, 0);
            int rowMax = Math.Min(shipSquares.Max(s => s.row) + 2, rows);
            int columnMax = Math.Min(shipSquares.Max(s => s.column) + 2, columns);

            List<Square> elim = new List<Square>();

            for (int i = rowMin; i < rowMax; i++)
            {
                for (int j = columnMin; j < columnMax; j++)
                {
                    elim.Add(new Square(i, j));
                }
            }
            return elim;
        }
    }
}