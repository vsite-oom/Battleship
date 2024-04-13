using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.OOM.Battleship.Model
{
    public struct SquareCoordinate
    {
        public readonly int row;
        public readonly int col;
        public SquareCoordinate(int row, int col)
        {
            this.row = row;
            this.col = col;
        }
    }
}
