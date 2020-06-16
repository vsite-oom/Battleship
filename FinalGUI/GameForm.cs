using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vsite.Oom.Battleship.Model;

namespace FinalGUI
{
    public partial class GameForm : Form
    {
        public GameForm()
        {
            InitializeComponent();
            sw = new Shipwright(rows, columns);
        }

        private void buttonDeploy_Click(object sender, EventArgs e)
        {
            playersGrid.ClearGrid();
            playersFleet = sw.CreateFleet(shipLengths);
            playersGrid.FillGrid(playersFleet);
            computersFleet = sw.CreateFleet(shipLengths);
            if (!buttonStartGame.Enabled)
                buttonStartGame.Enabled = true;
            buttonDeploy.Text = "Redeploy Fleets";
        }

        private void buttonStartGame_Click(object sender, EventArgs e)
        {
            buttonStartGame.Enabled = false;
        }

        readonly Shipwright sw;
        private readonly int rows = RulesSingleton.Instance.Rows;
        private readonly int columns = RulesSingleton.Instance.Columns;
        private readonly int[] shipLengths = RulesSingleton.Instance.ShipLengths;
        private Fleet playersFleet = new Fleet();
        private Fleet computersFleet = new Fleet();

       
    }
}
