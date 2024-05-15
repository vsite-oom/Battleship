using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vsite.oom.battleship.model
{
    public enum SquareState
    {
        Intact,
        Eliminated,
        Missed,
        Hit,
        Sunken
    }

    public class ShotsGrid : Grid
    {
        public ShotsGrid(int rows, int columns) : base(rows, columns) 
        {

        }

        protected override bool isSquareAvailable(int row, int column)
        {
            throw new NotImplementedException();
        }


    }
}
