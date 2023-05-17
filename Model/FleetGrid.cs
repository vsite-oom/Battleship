using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    using SquareSequence = IEnumerable<Square>;
    public class FleetGrid : Grid
    {
        public FleetGrid(int rows, int columns) : base(rows, columns)
        {

        }



        public void RemoveSquare(int row, int column)
        {
            squares[row, column] = null;
        }

        public void RemoveSquares(SquareSequence squaresToRemove)
        {
            foreach (var square in squaresToRemove)
            {
                RemoveSquare(square.Row, square.Column);
            }
        }



        protected override bool IsAvailable(Square square)
        {
            return square != null;
        }


    }
}
