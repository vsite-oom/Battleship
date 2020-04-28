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

namespace Vsite.Oom.Battleship.Gui
{
    public partial class GuiForm : Form
    {
        public GuiForm()
        {
            InitializeComponent();
        }

        public void InitGrid()
        {
            //Testing drawing grid
            int rows = 10;
            int cols = 10;

            Shipwright sw = new Shipwright(rows, cols);
            IEnumerable<int> ships = new int[] { 2, 2, 2, 2, 3, 3, 3, 4, 4, 5 };
            Fleet fleet = new Fleet();
            fleet = sw.CreateFleet(ships);

            guiPanel1.SetSize(rows, cols, ref fleet);
          //--------------------------------------------------------------------------
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InitGrid();
        }
    }
}
