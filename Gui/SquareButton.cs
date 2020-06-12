using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Windows.Forms.VisualStyles;
using Vsite.Oom.Battleship.Model;

namespace Vsite.Oom.Battleship.Gui
{
    class SquareButton : Button
    {
        public SquareButton(int w, int h, Square square)
        {
            this.square = square;
            Width = w;
            Height = h;
            TabStop = false;
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 1;
            FlatAppearance.BorderColor = Color.White;
        }

        public Square GetSquare() 
        {
            return square; 
        }

        private Square square;
    }
}
