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
    public partial class Battlefield : Form
    {
        public Battlefield()
        {
            InitializeComponent();

            for (int i = 0; i < numberOfRows; ++i)
            {
                for (int j = 0; j < numberOfColumns; ++j)
                {
                    CheckBox cb = new CheckBox();
                    cb.Appearance = Appearance.Button;
                    cb.Location = new System.Drawing.Point(20 + j * 50, 20 + i * 50);
                    cb.Size = new System.Drawing.Size(50, 50);
                    cb.Enabled = false;
                    this.Controls.Add(cb);
                    BattleGrid.Add(cb);
                }
            }
        }

        private void arrange_Click(object sender, EventArgs e)
        {
            Shipwright sw = new Shipwright(10, 10);
            fleet = sw.CreateFleet(new int[] { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 });

            for (int i = 0; i < numberOfRows; ++i)
            {
                for (int j = 0; j < numberOfColumns; ++j)
                {
                    for (int k = 0; k < fleet.Ships.Count(); ++k)
                    {
                        if (fleet.Ships.ElementAt(k).Squares.Contains(new Square(i, j)))
                        {
                            BattleGrid[i * 10 + j].BackColor = System.Drawing.Color.Blue;
                            break;
                        }
                        else
                            BattleGrid[i * 10 + j].BackColor = System.Drawing.Color.White;
                    }
                }
            }
        }

        Fleet fleet = new Fleet();
        List<CheckBox> BattleGrid = new List<CheckBox>();
        int numberOfRows = 10;
        int numberOfColumns = 10;
    }
}
