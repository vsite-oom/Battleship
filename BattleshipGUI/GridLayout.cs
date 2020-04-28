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
            //using Try Catch so when he fails 3 times to gen a fleet, that no crash happens
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
            float x = 0;
            float y = 0;
            float xspace = panel1.Height / lines;
            float yspace = panel1.Width / lines - 1;


            foreach(var ship in fleet.Ships) { 
                foreach(var square in ship.Squares) { 
                    var rect = new Rectangle((int)((square.Column)*xspace)+1, (int)((square.Row)*yspace)+1, (int)xspace-1,(int)yspace-1);
                    x += xspace;
                    graphics.FillRectangle(Brushes.BlueViolet, rect);
                }
                y += yspace;
            }           
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            drawGrid(e.Graphics);

            if (fleet != null)
                drawShips(e.Graphics);
        }
    }
}
