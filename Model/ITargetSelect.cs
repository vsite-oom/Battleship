using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.oom.Battleship.Model
{
    public interface ITargetSelect
    {

        Square NextTarget();
    }
}
