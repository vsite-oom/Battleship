using System.Collections.Generic;

namespace Vsite.Oom.Battleship.Model
{
    class OnlyShipSquaresEliminator : ISquareEliminator
    {
        public IEnumerable<Square> ToEliminate(IEnumerable<Square> shipSquares)
        {
            return shipSquares;
        }
    }
}
