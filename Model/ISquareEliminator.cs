using System.Collections.Generic;

namespace Vsite.Oom.Battleship.Model
{
    public interface ISquareEliminator
    {
        IEnumerable<Square> ToEliminate(IEnumerable<Square> shipSquares);
    }
}