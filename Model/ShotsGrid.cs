using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vsite.Oom.Battleship.Model;

namespace vsite.oom.battleship.model
{


    public class ShotsGrid : Grid
    {
        public ShotsGrid(int rows, int columns) : base(rows, columns) 
        {

        }

        protected override bool isSquareAvailable(int row, int column)
        {
            return squares[row, column]?.SquareState == SquareState.Intact;
        }
        public Square GetSquare(int row, int column)
        {
            return squares[row, column]!;
        }
    }
}
