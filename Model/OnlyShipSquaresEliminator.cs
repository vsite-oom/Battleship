using System.Collections.Generic;

namespace Vsite.Oom.Battleship.Model
{
    public class OnlyShipSquaresEliminator : ISquareEliminator
    {
        public IEnumerable<Square> ToEliminate(IEnumerable<Square> squares)
        {
            return squares;
        }
    }
}
