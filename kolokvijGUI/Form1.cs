using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using Vsite.Oom.Battleship.Model;

namespace kolokvijGUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            showFleet();

            while (true)
            {
                button1.Enabled = false;

                DialogResult dialogResult = MessageBox.Show("Arrange again ?", "", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {

                    cleanGrid();
                    showFleet();
                   
                }
                else
                {
                    Close();
                }

            }
              
        }




        private void showFleet()
        {
            List<Button> buttonList = this.Controls.OfType<Button>().Concat(this.panel1.Controls.OfType<Button>()).ToList();


            Shipwright shipwright = new Shipwright(10, 10);

            Fleet fleet = shipwright.CreateFleet(new int[] { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 });


            var shipsList = fleet.Ships.ToList();

            var rowList = new List<int>();
            var colList = new List<int>();

         

            foreach (var x in shipsList)
            {
                var squares = x.Squares;
                foreach (var y in squares)
                {
                    var r = y.Row;
                    var c = y.Column;

                    for (int i = 0; i < 10; ++i)
                    {
                        for (int j = 0; j < 10; j++)
                        {
                            if (i == c && r == j)
                            {
                                string buttonName = "row" + i + "col" + j;
                                foreach (var buttton in buttonList)
                                {
                                    if (buttton.Name == buttonName)
                                    {
                                        buttton.BackColor = SystemColors.ControlDarkDark;


                                    }
                                }
                            }
                        }
                    }

                }
              
            }
        }
        private void cleanGrid()
        {
            List<Button> buttonList = this.Controls.OfType<Button>().Concat(this.panel1.Controls.OfType<Button>()).ToList();
            foreach (var buttton in buttonList)
            {
                buttton.BackColor = SystemColors.ControlLightLight;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

      
    }
}
