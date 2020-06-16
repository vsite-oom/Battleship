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

        public void ClearGrid()
        {
            for (int r = 0; r < RulesSingleton.Instance.Rows; ++r)
            {
                for (int c = 0; c < RulesSingleton.Instance.Columns; ++c)
                {
                    buttons[r, c].BackColor = buttonColor;
                }
            }
        }

        Color buttonColor = SystemColors.ControlLight;
        Color shipColor = Color.Red;
    }
}
