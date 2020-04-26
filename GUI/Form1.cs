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
    public partial class BattleshipWindow : Form
    {
        private Panel[,] _battleshipPanels;
        public BattleshipWindow()
        {
            InitializeComponent();
        }

        private void BattleshipWindow_Load(object sender, EventArgs e)
        {
            const int tileSize = 45;
            const int gridSize = 10;

            //incijalizacija ploče
            _battleshipPanels = new Panel[gridSize, gridSize];

            //dvije for petlje za prolazak kroz sve redove i kolumne
            for (var n=0;n< gridSize; n++)
            {
                for(var m = 0; m < gridSize; m++)
                {
                    var newPanel = new Panel
                    {
                        Size = new Size(tileSize, tileSize),
                        Location = new Point(10 + tileSize * n, 10 + tileSize * m)
                        
                    };
                    newPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
                    newPanel.BackColor = Color.White;

                    //dodajemo form kontrolama
                    Controls.Add(newPanel);

                    //dodajemo u array panela za daljnju uporabu
                    _battleshipPanels[n, m] = newPanel;
                }
            }

        }

        private void ShipPlacingButton_Click(object sender, EventArgs e)
        {
            Shipwright sw = new Shipwright(10, 10);
            var fleet = sw.CreateFleet(new int[] { 5, 4, 4, 3, 3, 2, 2, 2, 2 });

            foreach (var square in fleet.Ships.SelectMany(x => x.Squares))
                _battleshipPanels[square.Row, square.Column].BackColor = Color.CornflowerBlue;

            ShipPlacingButton.BackColor = Color.Gray;
            ShipPlacingButton.Enabled = false;
            ResetShipPlacingButton.Visible = true;
        }

        private void ResetShipPlacingButton_Click(object sender, EventArgs e)
        {
            ResetShipPlacingButton.Visible = false;
            ShipPlacingButton.BackColor = Color.CornflowerBlue;
            ShipPlacingButton.Enabled = true;

            foreach (var panel in _battleshipPanels)
                panel.BackColor = Color.White;

        }
    }
}
