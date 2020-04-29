using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.BattleShip.Model
{
    public class SquareTerminator
    {
        public SquareTerminator(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;
        }

        public IEnumerable<Square> toEliminate(IEnumerable<Square> shipSquares)
        {
            int left = shipSquares.First().column;
            if(left > 0)
            {
                --left;
            }

            int top = shipSquares.First().row;
            if (top > 0)
            {
                --top;
            }

            int right = shipSquares.Last().column + 1;
            if (right < columns)
            {
                ++right;
            }

            int bottom = shipSquares.Last().row + 1;
            if (bottom < rows)
            {
                ++bottom;
            }

            List<Square> toEliminate = new List<Square>();
            for(int r = top; r < bottom; ++r)
            {
                for(int c = left; c < right; ++c)
                {
                    toEliminate.Add(new Square(r, c));
                }
            }

            return toEliminate;
        }
        private readonly Grid grid;
        private readonly int rows;
        private readonly int columns;
    }
}
