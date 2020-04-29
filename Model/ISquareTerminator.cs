using System.Collections.Generic;

namespace Vsite.Oom.Battleship.Model
{
    public interface ISquareTerminator
    {
        IEnumerable<Square> ToEliminate(IEnumerable<Square> squares);
    }
}
