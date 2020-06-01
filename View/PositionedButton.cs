using System.Windows.Forms;

namespace Vsite.Oom.Battleship.Model.View
{
    public class PositionedButton : Button
    {
        public int Row { get; set; }

        public int Column { get; set; }
    }
}
