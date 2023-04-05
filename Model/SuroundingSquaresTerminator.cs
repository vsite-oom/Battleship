using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class SuroundingSquaresTerminator : ISquareTerminator
    {
        public SuroundingSquaresTerminator(int gridRows, int gridColumns)
        {
            this.gridRows = gridRows;
            this.gridColumns = gridColumns;
        }

        public readonly int gridRows;
        public readonly int gridColumns;

        public IEnumerable<Square> ToEliminate(IEnumerable<Square> shipSquares)
        {
            throw new NotImplementedException();
        }
    }
}
