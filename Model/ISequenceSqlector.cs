using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    using SquareSequance = IEnumerable<Square>;
    using Sequances = IEnumerable<IEnumerable<Square>>;
    public interface ISequenceSqlector
    {
        SquareSequance Select(Sequances sequances);
    }
}
