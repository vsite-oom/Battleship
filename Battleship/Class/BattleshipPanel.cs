using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vsite.Oom.Battleship.Model;

namespace Battleship.Class
{
    class BattleshipPanel : Panel
    {
        Rectangle[,] shipGrid = new Rectangle[11, 11];
        internal Fleet fleetToDraw;


        public void CreateAndDrawShipGrid(Graphics graphics)
        {
            int x = 0;
            int y = 0;
            int xOffset = (Width - 1) / 11;
            int yOffset = (Height - 1) / 11;
            for (int row = 0; row < 11; ++row)
            {
                for (int col = 0; col < 11; ++col)
                {
                    var rect = new Rectangle(x, y, xOffset, yOffset);
                    graphics.DrawRectangle(Pens.Black, rect);

                    if (row == 0 && col > 0)
                    {
                        using (var font = new Font("Arial", 15))
                            graphics.DrawString(col.ToString(), font, Brushes.Black, rect.X, rect.Y);
                    }
                    if (col == 0 && row > 0)
                    {
                        using (var font = new Font("Arial", 15))
                        {
                            char[] c = { '0', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };
                            graphics.DrawString(c[row].ToString(), font, Brushes.Black, rect.X, rect.Y);
                        }
                    }

                    shipGrid[row, col] = rect;
                    x += xOffset;
                }
                x = 0;
                y += yOffset;
            }
        }

        public void FillFleetToShipGrid(Graphics graphics)
        {
            foreach (var ship in fleetToDraw.Ships)
            {
                foreach(var square in ship.Squares)
                {
                    var tmpRect = shipGrid[square.Column + 1, square.Row + 1];
                    //graphics.FillRectangle(Brushes.Transparent, tmpRect);
                    graphics.FillRectangle(Brushes.Navy, tmpRect);
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            CreateAndDrawShipGrid(e.Graphics);

            if (fleetToDraw != null)
                FillFleetToShipGrid(e.Graphics);
        }

        public BattleshipPanel()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
        }

    }
}
