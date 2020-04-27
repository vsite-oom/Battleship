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
        int labelSize = 50;
        public Form1()
        {
            InitializeComponent();
            FillTableWithPanels(tableLayoutPanel1);
            AddRowColumnIds();
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
        private void AddRowColumnIds() {
            

            for (int i = 0; i < 10; i++) {

                //vertical 
                Label rowId = new Label();
                rowId.Text = (i+1).ToString();
                rowId.Width = 50;
                rowId.Height = 50;
                rowId.Location = new Point(20, 20 + 51 * (i + 1));
                this.Controls.Add(rowId);

                //horizontal
                Label columnId = new Label();
                columnId.Text = Convert.ToString((char)('A' + i));
                columnId.Width = 50;
                columnId.Height = 50;
                columnId.Location = new Point(15 + 55 * (i + 1),20);
                this.Controls.Add(columnId);
            }
        }
 
        private void resetbutton_Click(object sender, EventArgs e)
        {
            //reset map
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                control.BackColor = Color.White;
            }

            sw = new Shipwright(10, 10);

            try
            {
                Fleet fleet = sw.CreateFleet(new int[] { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 });

                //add ships to map
                foreach (var ship in fleet.Ships)
                {
                    foreach (var square in ship.squares)
                    {
                        tableLayoutPanel1.GetControlFromPosition(square.Column, square.Row).BackColor = Color.Black;
                    }
                }
            }
            catch (ArgumentOutOfRangeException) {
                MessageBox.Show("Neuspjelo slaganje brodova");
            }
            
        }
    }
}
