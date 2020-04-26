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

namespace GUI
{
    public partial class Form1 : Form
    {
        Shipwright sw;
        public Form1()
        {
            InitializeComponent();
            FillTableWithPanels(tableLayoutPanel1);
        }

        private void FillTableWithPanels(TableLayoutPanel grid) {
            for (int r = 0; r < grid.RowCount;r++) {

                for (int c = 0; c < grid.ColumnCount; c++)
                {
                    Panel p = new Panel();
                    p.BackColor = Color.White;           
                    p.Margin = new Padding(0);
                    grid.Controls.Add(p, c, r);
                } 
            }
        }

        private void ClearGrid() {
            foreach (Control control in tableLayoutPanel1.Controls) {
                control.BackColor = Color.White;
            }
        }

        private void resetbutton_Click(object sender, EventArgs e)
        {
            //reset map
            ClearGrid();

            sw = new Shipwright(10, 10);

            try
            {
                Fleet fleet = sw.CreateFleet(new int[] { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 });

                foreach (var ship in fleet.Ships)
                {
                    foreach (var square in ship.squares)
                    {
                        var controlToColor = tableLayoutPanel1.GetControlFromPosition(square.Column, square.Row);
                        controlToColor.BackColor = Color.Red;

                    }
                }
            }
            catch (ArgumentOutOfRangeException) {
                MessageBox.Show("Neuspjelo slaganje brodova");
            }
            
        }
    }
}
