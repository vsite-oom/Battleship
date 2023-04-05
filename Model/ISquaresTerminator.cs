using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsie.Oom.Battleship.Model
{
    using Sequences = IEnumerable<IEnumerable<Square>>;
    public interface ISquaresTerminator
    {
        SquareSequence ToEliminat(SquareSequence shipSquares);   
    }
}
