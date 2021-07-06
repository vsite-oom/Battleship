using System;
using System.Collections.Generic;
using System.Linq;

namespace Vsite.Oom.Battleship.Model
{
    public class SurroundingSquareEliminator : ISquareEliminator
    {
        public SurroundingSquareEliminator(int rows, int columns)
        {
            this.rows    = rows;
            this.columns = columns;
        }

        public IEnumerable<Square> ToEliminate(IEnumerable<Square> squares)
        {
            int top = Math.Max(squares.Min(s => s.Row) - 1, 0);
            int left = Math.Max(squares.Min(s => s.Column) - 1, 0);
            int bottom = Math.Min(squares.Max(s => s.Row) + 2, rows);
            int right = Math.Min(squares.Max(s => s.Column) + 2, columns);

            var toEliminate = new List<Square>();

            for (int r = top; r < bottom; ++r)
            {
                for (int c = left; c < right; ++c)
                {
                    toEliminate.Add(new Square(r, c));
                }
            }

            return toEliminate;
        }

        private readonly int rows;
        private readonly int columns;
    }
}
