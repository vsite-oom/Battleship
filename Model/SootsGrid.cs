using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class SootsGrid: Grid
    {   
        public SootsGrid(int rows, int columns) : base(rows, columns)
        {
            
        }

        protected override bool IsSquareAvailble(int row, int column)
        {
            return squares[row, column]?.SquareState == SquareState.Intact;
        }

        
        public Square GetSquare(int row, int column)
        {
            return squares[row, column]!
        }

    }
}
