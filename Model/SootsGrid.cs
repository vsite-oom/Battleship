using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class ShotsGrid : Grid
    {
        public ShotsGrid(int rows, int columns) : base(rows, columns) { }

        protected override bool IsSquareAvailable(int row, int column)
        {
            return squares[row, column]?.SquareState == SquareState.Intact;
        }

       /* public Square GetSquare(int row, int column)
        {
            return squares[row, column]!;
        }*/

        public void ChangeSquareState(int row, int column, SquareState newState)
        {
            squares[row, column]!.ChangeState(newState);
        }
    }
}