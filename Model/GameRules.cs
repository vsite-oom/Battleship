using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class GameRules
    {
        public GameRules() 
        {
            Terminator = new SurroundingSquaresTerminator(GridRows, GridColumns);
        }

        public readonly int GridRows = 10;
        public readonly int GridColumns = 10;
        public readonly IEnumerable<int> ShipLenghts = new List<int> { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 };
        public readonly ISquaresTerminator Terminator;
    }
}
