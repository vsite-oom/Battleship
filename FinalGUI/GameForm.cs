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

        private void DeployFleets()
        {
            playersGrid.ClearGrid();
            computersGrid.ClearGrid();
            playersFleet = sw.CreateFleet(shipLengths);
            playersGrid.FillGrid(playersFleet);
            computersFleet = sw.CreateFleet(shipLengths);
        }

        private void buttonDeploy_Click(object sender, EventArgs e)
        {
            DeployFleets();
            if (!buttonStartGame.Enabled)
            {
                buttonStartGame.Enabled = true;
                buttonStartGame.Text = "Start Game";
            }
            buttonDeploy.Text = "Redeploy Fleets";
            RestorePlayerLabel();
            RestoreComputerLabel();
            RestoreStatusLabel();
        }

        private void buttonStartGame_Click(object sender, EventArgs e)
        {
            buttonStartGame.Enabled = false;
            buttonStartGame.Text = "Game Started";
            Game game = new Game(this, playersFleet, computersFleet, playersGrid, computersGrid);
            game.StartShooting();
        }

        internal void OnFirstShooterChosen(object source, Game.MessageArgs arg)
        {
            if (arg.Message == "You")
            {
                groupBoxPlayer.Text = arg.Message + " shoot first!";
                groupBoxPlayer.ForeColor = Color.Red;
                RestoreComputerLabel();
            }
            else
            {
                groupBoxComputer.Text = arg.Message + " shoots first!";
                groupBoxComputer.ForeColor = Color.Red;
                RestorePlayerLabel();
            }                
        }

        private void RestorePlayerLabel()
        {
            groupBoxPlayer.Text = "You";
            groupBoxPlayer.ForeColor = SystemColors.ControlText;
        }

        private void RestoreComputerLabel()
        {
            groupBoxComputer.Text = "Computer";
            groupBoxComputer.ForeColor = SystemColors.ControlText;
        }

        internal void OnChangeOfTurn(object source, Game.MessageArgs arg)
        {
            labelStatus.Text = arg.Message;
        }

        private void RestoreStatusLabel()
        {
            labelStatus.Text = "";
        }

        readonly Shipwright sw;
        internal readonly int rows = RulesSingleton.Instance.Rows;
        internal readonly int columns = RulesSingleton.Instance.Columns;
        internal readonly int[] shipLengths = RulesSingleton.Instance.ShipLengths;
        private Fleet playersFleet = new Fleet();
        private Fleet computersFleet = new Fleet();
    }
}
