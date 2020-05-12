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
        Fleet flota = new Fleet();
        public Form1()
        {
            InitializeComponent();
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

        private void BattleshipGrid_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            foreach (var s in flota.Ships)
            {
                if (s.Squares.Contains(new Square(e.Row, e.Column)))
                {
                    e.Graphics.FillRectangle(Brushes.Green, e.CellBounds);
                    break;
                }
            }
        }
    }
}
