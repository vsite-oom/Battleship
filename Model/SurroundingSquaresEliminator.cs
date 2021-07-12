using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class SurroundingSquaresEliminator : ISquareEliminator
    {
        public SurroundingSquaresEliminator(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;
        }

        public IEnumerable<Square> ToEliminate(IEnumerable<Square> shipSquares)
        {
            if (shipSquares.Count() == 0)
            {
                return null;
            }

            int top = shipSquares.First().Row;
            int bottom = shipSquares.Last().Row + 1;

            int left = shipSquares.First().Column;
            int right = shipSquares.Last().Column + 1;

            if (top > 0)
            {
                --top;
            }

            if (bottom < rows)
            {
                ++bottom;
            }

            if (left > 0)
            {
                --left;
            }

            if (right < columns)
            {
                ++right;
            }

            var eliminatedSquares = new List<Square>();

            for (int r = top; r < bottom; ++r)
            {
                for (int c = left; c < right; ++c)
                {
                    eliminatedSquares.Add(new Square(r, c));
                }
            }
            
            return eliminatedSquares;
        }

        private readonly int rows;
        private readonly int columns;
    }
}
