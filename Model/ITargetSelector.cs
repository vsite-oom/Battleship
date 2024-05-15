using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vsite.Oom.Battleship.Model;

namespace vsite.oom.battleship.model
{
    public interface ITargetSelector
    {
        Square Next();
    }
}
