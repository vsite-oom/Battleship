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
            int left = shipSquares.First().Column;
            int right = shipSquares.Last().Column + 1;

            int top = shipSquares.First().Row;
            int bottom = shipSquares.Last().Row + 1;

            if (left > 0)
                --left;

            if (right < columns)
                ++right;

            if (top > 0)
                --top;

            if (bottom < rows)
                ++bottom;
            
            List<Square> eliminated = new List<Square>();

            for (int i = top; i < bottom; i++)
            {
                for (int j = left; j < right; j++)
                {
                    eliminated.Add(new Square(i, j));
                }
            }

            return eliminated;
        }

        private readonly int rows;
        private readonly int columns;
    }
}