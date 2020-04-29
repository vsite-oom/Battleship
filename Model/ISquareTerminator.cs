using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.BattleShip.Model
{
    public interface ISquareTerminator
    {
        IEnumerable<Square> toEliminate(IEnumerable<Square> shipSquares);
    }
}
