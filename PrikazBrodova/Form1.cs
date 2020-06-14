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

namespace GUI
{
    public partial class Form1 : Form
    {
        Shipwright sw = new Shipwright(10, 10);
        Shipwright swIgrac;
        Fleet FleetAi;
        Fleet FleetIgrac;
        Gunner gunner;
        int shipCountAi;
        int shipCountIgrac;

        public Form1()
        {
            
            InitializeComponent();
            AddButtons();
            AddRowColumnIds();
        }

        private void AddButtons() {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {

                    Button poljeAi = new Button();
                    poljeAi.Enabled = false;
                    poljeAi.Height = 48;
                    poljeAi.Width = 48;
                    poljeAi.Location = new Point(10 + 50 * i, 10 + 50 * j);
                    poljeAi.BackColor = Color.White;
                    poljeAi.Tag = new Square(i, j);
                    poljeAi.FlatStyle = FlatStyle.Flat;
                    poljeAi.FlatAppearance.BorderColor = Color.Gray;
                    poljeAi.Click += new EventHandler(igracButton_Click);
                    aiPanel.Controls.Add(poljeAi);

                    Button poljeIgrac = new Button();
                    poljeIgrac.Enabled = false;
                    poljeIgrac.Height = 48;
                    poljeIgrac.Width = 48;
                    poljeIgrac.Location = new Point(10 + 50 * i, 10 + 50 * j);
                    poljeIgrac.BackColor = Color.White;
                    poljeIgrac.Tag = new Square(i, j);
                    poljeIgrac.FlatStyle = FlatStyle.Flat;
                    poljeIgrac.FlatAppearance.BorderColor = Color.Gray;
                    igracPanel.Controls.Add(poljeIgrac);

                }
            }
        }

        private void AddRowColumnIds()
        {

            for (int i = 0; i < 10; i++)
            {

                //vertical ids
                Label rowId = new Label
                {
                    Text = (i + 1).ToString(),
                    Width = 50,
                    Height = 50,
                    Location = new Point(30, 20 + 51 * (i + 1))
                };
                
                Label rowId2 = new Label
                {
                    Text = (i + 1).ToString(),
                    Width = 50,
                    Height = 50,
                    Location = new Point(640, 20 + 51 * (i + 1))
                };
                this.Controls.Add(rowId);
                this.Controls.Add(rowId2);

                //horizontal ids
                Label columnId = new Label
                {
                    Text = Convert.ToString((char)('A' + i)),
                    Width = 50,
                    Height = 50,
                    Location = new Point(20 + 51 * (i + 1), 20)
                };
                Label columnId2 = new Label
                {
                    Text = Convert.ToString((char)('A' + i)),
                    Width = 50,
                    Height = 50,
                    Location = new Point(620 +(20 + 51 * (i + 1)), 20)
                };
                this.Controls.Add(columnId);
                this.Controls.Add(columnId2);
            }
        }
        private void refreshSunken(Panel panel,Fleet fleet) {
            foreach (var ship in fleet.Ships)
            {
                foreach (var square in ship.squares)
                {
                    foreach (Button button in panel.Controls)
                    {
                        if (square.Equals(button.Tag))
                        {
                            if(square.SquareState == SquareState.Sunken)
                            button.BackColor = Color.DarkRed;
                        }
                    }
                }
            }
        }
        private void igracButton_Click(object sender, EventArgs e)
        {
            foreach (Button bu in aiPanel.Controls) {
                bu.FlatStyle = FlatStyle.Flat;
                bu.FlatAppearance.BorderColor = Color.Gray;
                bu.FlatAppearance.BorderSize = 1;

            }

            foreach (Button bu in igracPanel.Controls)
            {
                bu.FlatStyle = FlatStyle.Flat;
                bu.FlatAppearance.BorderColor = Color.Gray;
                bu.FlatAppearance.BorderSize = 1;
            }

            Button button = (Button)sender;
            HitResult hitResult = FleetAi.Hit((Square)button.Tag);
            switch (hitResult) {
                case HitResult.Hit:
                    button.BackColor = Color.Red;
                    break;
                case HitResult.Missed:
                    button.BackColor = Color.LightGray;
                    break;
                case HitResult.Sunken:
                    button.BackColor = Color.DarkRed;
                    shipCountAi--;
                    brodoviAi.Text = "Ships left (AI): " + shipCountAi;
                    refreshSunken(aiPanel,FleetAi);
                    
                    
                    
                    break;
            }
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderColor = Color.Blue;
            button.FlatAppearance.BorderSize = 2;
            button.Enabled = false;
            this.ActiveControl = null;
            
            //this.Enabled = false;
            //this.UseWaitCursor = true;
            //await Task.Delay(800);
            //this.Enabled = true;
            //this.UseWaitCursor = false;

            Square target = gunner.NextTarget();
            hitResult = FleetIgrac.Hit(target);
            gunner.ProcessHitResult(hitResult);
            Button b= new Button();
            foreach (Button but in igracPanel.Controls) {
                if (but.Tag.Equals(target)) {
                    b = but;
                }
                
            }

            switch (hitResult)
            {
                case HitResult.Hit:
                    b.BackColor = Color.Red;
                    break;
                case HitResult.Missed:
                    b.BackColor = Color.LightGray;
                    break;
                case HitResult.Sunken:
                    b.BackColor = Color.DarkRed;
                    shipCountIgrac--;
                    refreshSunken(igracPanel,FleetIgrac);
                    brodoviIgrac.Text = "Ships left (Igrac): " + shipCountIgrac.ToString();
                    
                    break;
            }
            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderColor = Color.Blue;
            b.FlatAppearance.BorderSize = 2;

            zadnjiPotezAi.Text = target.Row.ToString() + "  " + target.Column.ToString();

            if (FleetAi.AllShipsSunken())
            {
                MessageBox.Show("Pobjeda");

                foreach (Button but in aiPanel.Controls)
                {
                    but.Enabled = false;
                }
                return;
            }
            if (FleetIgrac.AllShipsSunken())
            {
                MessageBox.Show("Poraz");
                foreach (Button but in aiPanel.Controls)
                {
                    but.Enabled = false;
                }
                return;
            }
        }


        private void resetbutton_Click(object sender, EventArgs e)
        {
            //reset map
            foreach (Button button in aiPanel.Controls)
            {
                    button.BackColor = Color.White;
                    button.Enabled = true;
            }
            foreach (Button button in igracPanel.Controls)
            {
                    button.BackColor = Color.White;
            }

            

            try
            {
                swIgrac = new Shipwright(10, 10);
                FleetAi = sw.CreateFleet(new int[] { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 });
                FleetIgrac = swIgrac.CreateFleet(new int[] { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 });
                gunner = new Gunner(10, 10, new int[] { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 });
                shipCountAi = FleetAi.Ships.Count();
                shipCountIgrac = FleetIgrac.Ships.Count();
                brodoviAi.Text ="Ships left (AI): " +  shipCountAi;
                brodoviIgrac.Text = "Ships left (Igrac): " + shipCountIgrac;
                //add ships to map
                foreach (var ship in FleetIgrac.Ships)
                {
                    foreach (var square in ship.squares)
                    {
                        foreach(Button button in igracPanel.Controls)
                        {
                            if (square.Equals(button.Tag))
                            {
                                button.BackColor = Color.Black;
                            }
                        }
                    }
                }
            }
            //random adding of ships has failed
            catch (ArgumentOutOfRangeException) {
                MessageBox.Show("Neuspješno slaganje brodova");
            }
            
        }

      
    }
}
