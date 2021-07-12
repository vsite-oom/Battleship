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

namespace GUI_Ship_Placement_Randomizer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void GridReset()
        {
            Pen pen = new Pen(Brushes.Black, 1);
            Brush gray = new SolidBrush(Color.Gray);
            Graphics gfx = gridPanel.CreateGraphics();

            for (int rowLine = 0; rowLine < 11; ++rowLine)
            {
                gfx.DrawLine(pen, 0, rowLine * (cellHeight + 1), 410, rowLine * (cellHeight + 1));
            }

            for (int columnLine = 0; columnLine < 11; ++columnLine)
            {
                gfx.DrawLine(pen, columnLine * (cellWidth + 1), 0, columnLine * (cellWidth + 1), 410);
            }

            for (int r = 0; r < 10; ++r)
            {
                for (int c = 0; c < 10; ++c)
                {
                    gridSquareXCoor[r, c] = (cellWidth + 1) * c + 1;
                    gridSquareYCoor[r, c] = (cellHeight + 1) * r + 1;
                    gfx.FillRectangle(gray, gridSquareXCoor[r, c], gridSquareYCoor[r, c], cellWidth, cellHeight);
                }
            }
        }

        private void gridPanel_Paint(object sender, PaintEventArgs e)
        {
            GridReset();
        }

        private void GenerateFleet()
        {
            GridReset();

            Brush yellow = new SolidBrush(Color.Yellow);
            Graphics gfx = gridPanel.CreateGraphics();

            Shipwright shipWright = new Shipwright(10, 10, new int[] { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 });
            Fleet fleet = new Fleet();

            try
            {
                fleet = shipWright.CreateFleet();
            }
            catch (Exception excep)
            {
                GenerateFleet();
            }

            for (int r = 0; r < 10; ++r)
            {
                for (int c = 0; c < 10; ++c)
                {
                    foreach (Ship ship in fleet.Ships)
                    {
                        if (ship.Squares.Contains(new Square(r, c)))
                        {
                            gfx.FillRectangle(yellow, gridSquareXCoor[r, c], gridSquareYCoor[r, c], cellWidth, cellHeight);
                        }
                    }
                }
            }
        }

        private int cellWidth = 40;
        private int cellHeight = 40;
        private int[,] gridSquareXCoor = new int[10, 10];
        private int[,] gridSquareYCoor = new int[10, 10];

        private void randomizeButton_Click(object sender, EventArgs e)
        {
            GenerateFleet();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
