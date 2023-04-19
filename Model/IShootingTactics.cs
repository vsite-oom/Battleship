using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vsite.Oom.Battleship.Model;

namespace Vsie.Oom.Battleship.Model
{
    public interface IShootingTactics
    {
        Square NextTarget();
    }
}
