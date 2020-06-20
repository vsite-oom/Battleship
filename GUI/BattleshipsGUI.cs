using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vsite.Oom.Battleship.Model;

namespace Vsite.Oom.Battleship.GUI
{
    public partial class BattleshipsGUI : Form
    {
        public BattleshipsGUI()
        {
            InitializeComponent();
        }

        private void newGameButton_Click(object sender, EventArgs e)
        {
            playerFleetGrid.Controls.Clear();
            enemyFleetGrid.Controls.Clear();
            playerFleetGrid.Init();
            enemyFleetGrid.Init();
        }
    }
}