using System.Collections.Generic;
using System.Linq;

namespace Vsite.Oom.Battleship.Model
{
    public class SquareTerminator
    {
        private readonly int rows;
        private readonly int columns;

        public SquareTerminator(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;
        }

        public IEnumerable<Square> ToEliminate(IEnumerable<Square> squares)
        {
            int left = squares.First().Column;
            if (left > 0)
                --left;

            int top = squares.First().Row;
            if (top > 0)
                --top;

            int right = squares.Last().Column + 1;
            if (right < columns)
                ++right;

            int bottom = squares.Last().Row + 1;
            if (bottom < rows)
                ++bottom;

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
    }
}
