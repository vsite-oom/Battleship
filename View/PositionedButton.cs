using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vsite.Oom.Battleship.Model.View
{
    class PositionedButton : Button
    {
        public int Row { get; set; }
        public int Column { get; set; }
    }
}
