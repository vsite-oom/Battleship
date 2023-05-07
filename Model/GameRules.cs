using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class GameRules
    {
        public readonly int gridRows = 10;
        public readonly int gridColumns = 10;
        public readonly IEnumerable<int> shipLenghts = new List<int>{ 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 };
        public readonly ISquareTerminator terminator;

        public GameRules() {
            terminator = new SurroundingSquaresTerminator(gridRows, gridColumns);
        }
    }
}
