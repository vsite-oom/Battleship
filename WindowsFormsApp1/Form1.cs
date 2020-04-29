
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

namespace WindowsFormsApp1
{
   
    public partial class Form1 : Form
    {
        Fleet fleet = new Fleet();
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Shipwright sw = new Shipwright(10, 10);
            fleet = sw.CreateFleet(new int[] { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 });
            tableLayoutPanel1.Invalidate();
            button1.Enabled=false;
        }

        private void tableLayoutPanel1_CellPaint_1(object sender, TableLayoutCellPaintEventArgs e)
        {
            for (int i = 0; i < fleet.Ships.Count(); ++i)
            {
                if (fleet.Ships.ElementAt(i).Squares.Contains(new Square(e.Column, e.Row)))
                {
                    e.Graphics.FillRectangle(Brushes.Black, e.CellBounds);
                    break;
                }
                else
                    e.Graphics.FillRectangle(Brushes.White, e.CellBounds);
            }
        }
    }
}
