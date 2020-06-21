using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vsite.Oom.Battleship.Model;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.Timers;

namespace FinalGUI
{
    public partial class PlayersGrid : GameGrid
    {
        public PlayersGrid()
        {
            InitializeComponent();
            
        }

        public void FillGrid(Fleet fleet)
        {
            List<Ship> ships = new List<Ship>(fleet.Ships);
            for (int i = 0; i < ships.Count(); ++i)
            {
                foreach (var square in ships[i].Squares)
                {
                    buttons[square.Row, square.Column].BackColor = shipColor;
                }
            }
        }

        internal void OnComputerShoots(object sender, Game.SquareShotArgs arg)
        {
            Button button = buttons[arg.SquareShot.Row, arg.SquareShot.Column];
            Square square = arg.SquareShot;
            switch (square.SquareState)
            {
                case SquareState.Missed:
                    button.BackColor = missedColor;
                    button.ForeColor = shipColor;
                    break;
                case SquareState.Hit:
                    button.BackColor = hitColor;
                    button.Text = "Hit";
                    button.ForeColor = missedColor;
                    break;
                case SquareState.Sunken:
                    button.BackColor = sunkenColor;
                    button.Text = "Sunk";
                    button.ForeColor = missedColor;
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }
        }
    }
}