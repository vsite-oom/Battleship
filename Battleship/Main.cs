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

namespace Battleship
{
    public partial class Main : Form
    {
        readonly int[] shipDefinitions = new int[] { 2, 2, 2, 2, 3, 3, 3, 4, 4, 5 };
        public Main()
        {
            InitializeComponent();            
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            var shipBuilder = new Shipwright(10,10);
            //using Try Catch so when he fails 3 times to gen a fleet, that no crash happens
            try
            {
                opponentPanel.fleetToDraw = shipBuilder.CreateFleet(shipDefinitions);
            }
            catch
            {
                opponentPanel.fleetToDraw = shipBuilder.CreateFleet(shipDefinitions); 
            }            
            opponentPanel.Invalidate();

            // so I dont have same fleets every time
            System.Threading.Thread.Sleep(150);

            shipBuilder = new Shipwright(10, 10);
            try
            {
                playerPanel.fleetToDraw = shipBuilder.CreateFleet(shipDefinitions);
            }
            catch
            {
                playerPanel.fleetToDraw = shipBuilder.CreateFleet(shipDefinitions);
            }
            playerPanel.Invalidate();
        }
      
    }
}
