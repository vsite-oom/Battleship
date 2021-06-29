using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class Meni : Form
    {
        public Meni()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Jeste li sigurni da želite napusititi igru?", "Izlaz", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            using (var form = new Postavke())
            {
                var result = form.ShowDialog();
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            startGame();            
        }

        private void startGame()
        {
            using (var form = new Igra(
                Properties.Settings.Default.BrojRedaka, 
                Properties.Settings.Default.BrojStupaca, 
                Properties.Settings.Default.PostavljanjeFlote, 
                Properties.Settings.Default.ListaBrodova.Split(',').Select(int.Parse).ToList()))
            {
                var result = form.ShowDialog();
                if (result == DialogResult.Yes)
                    startGame();
            }
        }

        private void btnStats_Click(object sender, EventArgs e)
        {
            using (var form = new Statistika())
            {
                var result = form.ShowDialog();
            }
        }
    }
}
