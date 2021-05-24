using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsApp1
{
    public partial class SquareButton : System.Windows.Forms.Button
    {
        public SquareButton(int x, int y, Color color) : base()
        {
            X = x;
            Y = y;
            this.color = color;
        }

        public int X, Y;
        public Color color = Color.Gray;
    }
}
