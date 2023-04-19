using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class SurroundingSquaresTerminator : ISquaresTerminator
    {
        private readonly int gridRows;
        private readonly int gridColumns;
        public SurroundingSquaresTerminator(int gridRows, int gridColumns)
        {
            this.gridRows = gridRows;
            this.gridColumns = gridColumns;
        }
        public IEnumerable<Square> ToEliminate(IEnumerable<Square> squareSequance)
        {
            int topmostRow = Math.Max(0, squareSequance.Min(s => s.row) -1);
            int bottonmostRow = Math.Min(gridRows, squareSequance.Max(s => s.row) + 2);
            int leftmostColumn = Math.Max(0, squareSequance.Min(s => s.column) - 1);
            int rightmostColumn = Math.Min(gridColumns, squareSequance.Max(s => s.column) + 2);

            var toEliminate = new List<Square>();
            for (int r = topmostRow; r < bottonmostRow; ++r)
            {
                for (int c = leftmostColumn; c < rightmostColumn; ++c)
                {
                    toEliminate.Add(new Square(r, c));
                }
            }
            return toEliminate;
        }
    }
}
