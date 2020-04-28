using Vsite.Oom.Battleship.Model;
using System;
using System.Windows.Forms;

namespace FleetArangementGUI
{
    public partial class BattleshipGUI : Form
    {
        public BattleshipGUI()
        {
            InitializeComponent();
            fleetsGrid.DefineGrid(rows, columns);
        }


        private void ArrangeFleet(object sender, EventArgs e)
        {
            int[] shipLenghts = new int[] { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 };
            Shipwright sw = new Shipwright(rows, columns);
            var fleet = sw.CreateFleet(shipLenghts);
            fleetsGrid.SetupFleet(fleet);
        }

        private void Quit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        int rows = 10;
        int columns = 10;
    }
}
