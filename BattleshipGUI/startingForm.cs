using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleshipGUI
{
    public partial class startingForm : Form
    {
        public startingForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }



        private void button_WOC2_Click(object sender, EventArgs e)
        {
            this.Hide();
            var formToLoad = new playerVsComputer();
            formToLoad.Closed += (s, args) => Close();
            formToLoad.Show();
        }

        private void button_WOC1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var formToLoad = new randomFleetGenerator();
            formToLoad.Closed += (s, args) => Close();
            formToLoad.Show();
        }
    }
}
