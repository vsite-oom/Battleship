using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.oom.Battleship.Model
{

   
       
    
 public class SquareTerminator    {
        public SquareTerminator(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;
        }

        public SquareTerminator(Grid grid)
        {
            this.grid = grid;
        }

        public IEnumerable<Square> ToEliminate(IEnumerable<Square> shipSquares)
        {
            int left = shipSquares.First().Columns;
            if (left > 0)
                --left;
            int top = shipSquares.First().Rows;
            if (top > 0)
                --top;

            int right = shipSquares.Last().Columns + 1;
            if (right < columns)
                ++right;
            int bottom = shipSquares.Last().Rows + 1;
            if (bottom < rows)
                ++bottom;

            List<Square> toEliminate = new List<Square>();
            for (int r = top; r < bottom; ++r)
            {
                for (int c = left; c < right; ++c)
                    toEliminate.Add(new Square(r, c));
            }
            return toEliminate;
        }

        private readonly int rows;
        private readonly int columns;
        private Grid grid;
    }
}
