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

namespace BattleshipForm
{
    public partial class Form1 : Form
    {
        Shipwright shpWrt;
        Fleet flota;
        public Form1()
        {
            InitializeComponent();

            /*TableLayoutColumnStyleCollection ColumnStyles = BattleshipGrid.ColumnStyles;
            foreach(ColumnStyle style in ColumnStyles)
            {
                style.SizeType = SizeType.Percent;
                style.Width = 10;
            }


            TableLayoutRowStyleCollection RowStyles = BattleshipGrid.RowStyles;
            foreach (RowStyle style in RowStyles)
            {
                style.SizeType = SizeType.Percent;
                style.Height = 10;
            }

            shpWrt = new Shipwright(10, 10);
            List<int> shipLenghts = new List<int> { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 };
            flota = shpWrt.CreateFleet(shipLenghts);*/
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnSort_Click(object sender, EventArgs e)
        {
            NewGame();
        }
        private void NewGame()
        {
            try
            {
                shpWrt = new Shipwright(10, 10);
                List<int> shipLenghts = new List<int> { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 };
                flota = shpWrt.CreateFleet(shipLenghts);

                BattleshipGrid.Invalidate();
            }
            catch (ArgumentOutOfRangeException)
            {
                NewGame();
            }
        }

        private void GridFieldsPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            for (int i = 0; i < flota.Ships.Count(); ++i)
            {
                if (flota.Ships.ElementAt(i).Squares.Contains(new Square(e.Column, e.Row)))
                {
                    e.Graphics.FillRectangle(Brushes.Green, e.CellBounds);
                    break;
                }
                else
                    e.Graphics.FillRectangle(Brushes.Red, e.CellBounds);
            }
        }
    }
}
