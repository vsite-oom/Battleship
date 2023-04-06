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
        public SurroundingSquaresTerminator(int gridRows,int gridColumns)
        {
            this.gridRows = gridRows;
            this.gridColumns = gridColumns;
        }
        public IEnumerable<Square> ToEliminate(IEnumerable<Square> squareSequance)
        {
            throw new NotImplementedException();
        }
    }
}
