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

                    Panel p = new Panel
                    {
                        BackColor = Color.White,
                        Margin = new Padding(0)
                    };
                    grid.Controls.Add(p, c, r);

                } 
            }
        }
        private void AddRowColumnIds() {
            
            for (int i = 0; i < 10; i++) {

                //vertical ids
                Label rowId = new Label
                {
                    Text = (i + 1).ToString(),
                    Width = 50,
                    Height = 50,
                    Location = new Point(20, 20 + 51 * (i + 1))
                };
                this.Controls.Add(rowId);

                //horizontal ids
                Label columnId = new Label
                {
                    Text = Convert.ToString((char)('A' + i)),
                    Width = 50,
                    Height = 50,
                    Location = new Point(15 + 55 * (i + 1), 20)
                };
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

            Shipwright sw = new Shipwright(10, 10);

            
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
            //random adding of ships has failed
            catch (ArgumentOutOfRangeException) {
                MessageBox.Show("Neuspješno slaganje brodova");
            }
            
        }
    }
}
