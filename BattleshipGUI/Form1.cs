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
    public partial class Form1 : Form
    {
        Shipwright sw;
        Fleet flt;
        public Form1()

        {
            InitializeComponent();
            
            TableLayoutColumnStyleCollection ColumnStyles = BrodGUI.ColumnStyles;
            foreach (ColumnStyle style in ColumnStyles)
            {
                style.SizeType = SizeType.Percent;
                style.Width = 10;
            }

            TableLayoutRowStyleCollection RowStyles = BrodGUI.RowStyles;
            foreach (RowStyle style in RowStyles)
            {
                style.SizeType = SizeType.Percent;
                style.Height = 10;
            }

            sw = new Shipwright(10, 10);
            List<int> shipLengths = new List<int> { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 };
            flt = sw.CreateFleet(shipLengths);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sw = new Shipwright(10, 10);
            List<int> shipLengths = new List<int> { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 };
            flt = sw.CreateFleet(shipLengths);

            BrodGUI.Invalidate();
        }
        private void BS_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            foreach (var ship in flt.Ships)
            {
                foreach (var item in ship.Squares)
                {
                    if (e.Row == item.Row && e.Column == item.Column)
                        e.Graphics.FillRectangle(Brushes.Blue, e.CellBounds);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
