using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public interface ISquareEliminator
    {
        IEnumerable<Square> ToEliminate(IEnumerable<Square> shipSquares);
    }
}
