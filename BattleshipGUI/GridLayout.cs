using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vsite.Oom.Battleship.Model;

namespace BattleshipGUI
{
    public partial class GridLayout : Form
    {
        Fleet fleet;
        readonly int[] ships = new int[] { 2, 2, 2, 2, 3, 3, 3, 4, 4, 5 };
        public GridLayout()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var shipBuilder = new Shipwright(10, 10);
            try
            {
                fleet = shipBuilder.CreateFleet(ships);
            }
            catch
            {
                fleet = shipBuilder.CreateFleet(ships);
            }
            
            Invalidate();
        }

        private void drawGrid(Graphics graphics)
        {
            graphics = panel1.CreateGraphics();
            Pen mypen = new Pen(Brushes.Black, 1);
            Font myfont = new Font("Arial", 10);
            int lines = 10;
            float x = 0;
            float y = 0;
            float xspace = panel1.Height / lines;
            float yspace = panel1.Width / lines - 1;
            for (int i = 0; i <= lines; ++i)
            {
                graphics.DrawLine(mypen, x, y, x, lines * yspace);
                x += xspace;
            }
            x = 0;
            for (int i = 0; i <= lines; ++i)
            {
                graphics.DrawLine(mypen, x, y, lines * xspace, y);
                y += yspace;
            }
        }

        private void drawShips(Graphics graphics)
        {
            graphics = panel1.CreateGraphics();
            Pen mypen = new Pen(Brushes.Black, 1);
            Font myfont = new Font("Arial", 10);
            int lines = 10;
            float xspace = panel1.Height / lines;
            float yspace = panel1.Width / lines - 1;
            if (fleet != null)
            {
                List<Square> shipsquares = new List<Square>();
                foreach (var ship in fleet.Ships)
                {
                    foreach (var square in ship.Squares)
                    {
                        shipsquares.Add(square);
                    }
                }
                for (int i = 0; i < lines; ++i)
                {
                    for (int j = 0; j < lines; ++j)
                    {
                        var rect = new Rectangle((int)(i * xspace) + 1, (int)(j * yspace) + 1, (int)xspace - 1, (int)yspace - 1);
                        if (shipsquares.Contains(new Square(i, j)))
                        {
                            graphics.FillRectangle(Brushes.BlueViolet, rect);
                        }
                        else
                        {
                            graphics.FillRectangle(Brushes.WhiteSmoke, rect);
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < lines; ++i)
                {
                    for (int j = 0; j < lines; ++j)
                    {
                        var rect = new Rectangle((int)(i * xspace) + 1, (int)(j * yspace) + 1, (int)xspace - 1, (int)yspace - 1);
                        graphics.FillRectangle(Brushes.WhiteSmoke, rect);
                    }
                }
            } 
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            drawGrid(e.Graphics);
            drawShips(e.Graphics);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            fleet = null;
            Invalidate();
        }
    }
}
