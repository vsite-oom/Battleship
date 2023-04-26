using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    internal class SurroundingSquaresTerminator : ISquaresTerminator
    {
        public SurroundingSquaresTerminator(int gridRows, int gridColumns)
        {
            this.gridRows = gridRows;
            this.gridColumns = gridColumns;
        }

        private readonly int gridRows;
        private readonly int gridColumns;

      

        public IEnumerable<Square> ToEliminate(IEnumerable<Square> shipSquares)
        {
            int topmostRow = Math.Max(0, shipSquares.Min(x => x.Row) - 1);
            int bottommostRow = Math.Min(gridRows, shipSquares.Max(x => x.Row) + 2);
            int leftmostColumn = Math.Max(0, shipSquares.Min(x => x.Column) - 1);
            int rightmostColumn = Math.Min(gridColumns, shipSquares.Max(x => x.Column) + 2);

            var toEliminate = new List<Square>();
            for (int r = topmostRow; r < bottommostRow; ++r)
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
