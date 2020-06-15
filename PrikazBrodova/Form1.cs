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

        private void AddButtons()
        {
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
                    Location = new Point(620 + (20 + 51 * (i + 1)), 20)
                };
                this.Controls.Add(columnId);
                this.Controls.Add(columnId2);
            }
        }
        private void refreshSunken(Panel panel, Fleet fleet)
        {
            foreach (var ship in fleet.Ships)
            {
                foreach (var square in ship.squares)
                {
                    foreach (Button button in panel.Controls)
                    {
                        if (square.Equals(button.Tag))
                        {
                            if (square.SquareState == SquareState.Sunken)
                                button.BackColor = Color.DarkRed;
                        }
                    }
                }
            }
        }

        private void resetButtonBorders()
        {
            foreach (Button bu in aiPanel.Controls)
            {
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
        }
        private void playerTurn(object sender)
        {
            Button button = (Button)sender;
            HitResult hitResult = FleetAi.Hit((Square)button.Tag);
            switch (hitResult)
            {
                case HitResult.Hit:
                    button.BackColor = Color.Red;
                    break;
                case HitResult.Missed:
                    button.BackColor = Color.LightGray;
                    break;
                case HitResult.Sunken:
                    button.BackColor = Color.DarkRed;
                    shipCountAi--;
                    refreshSunken(aiPanel, FleetAi);
                    brodoviAi.Text = "Ships left (AI): " + shipCountAi;
                    break;
            }

            //add blue border to last move played for player
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderColor = Color.Blue;
            button.FlatAppearance.BorderSize = 2;
            button.Enabled = false;

            //remove focus 
            this.ActiveControl = null;


        }

        private void aiTurn()
        {
            Square target = gunner.NextTarget();
            HitResult hitResult = FleetIgrac.Hit(target);
            gunner.ProcessHitResult(hitResult);
            Button button = new Button();

            //find button that represents target
            foreach (Button but in igracPanel.Controls)
            {
                if (but.Tag.Equals(target))
                {
                    button = but;
                }

            }

            switch (hitResult)
            {
                case HitResult.Hit:
                    button.BackColor = Color.Red;
                    break;
                case HitResult.Missed:
                    button.BackColor = Color.LightGray;
                    break;
                case HitResult.Sunken:
                    button.BackColor = Color.DarkRed;
                    shipCountIgrac--;
                    refreshSunken(igracPanel, FleetIgrac);
                    brodoviIgrac.Text = "Ships left (Igrac): " + shipCountIgrac.ToString();
                    break;
            }

            //add blue border to last move played for AI
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderColor = Color.Blue;
            button.FlatAppearance.BorderSize = 2;

            //display last move for AI
            zadnjiPotezAi.Text = (target.Column + 1).ToString() + "-" + ((char)('A' + target.Row)).ToString() + " - " + hitResult.ToString();
        }


        private void igracButton_Click(object sender, EventArgs e)
        {
            resetButtonBorders();

            // Player turn to play
            playerTurn(sender);

            //this.Enabled = false;
            //await Task.Delay(800);
            //this.Enabled = true;


            // Ai turn to play
            aiTurn();

            //check for winning condition
            if (shipCountAi == 0)
            {
                MessageBox.Show("Win");

                foreach (Button button in aiPanel.Controls)
                {
                    button.Enabled = false;
                }
                return;
            }
            if (shipCountIgrac == 0)
            {
                MessageBox.Show("You lost");
                foreach (Button button in aiPanel.Controls)
                {
                    button.Enabled = false;
                }
                return;
            }
        }

        //new game button
        private void resetbutton_Click(object sender, EventArgs e)
        {
            //reset maps square color and border
            foreach (Button button in aiPanel.Controls)
            {
                button.BackColor = Color.White;
                button.Enabled = true;
                button.FlatAppearance.BorderColor = Color.Gray;
                button.FlatAppearance.BorderSize = 1;
            }
            foreach (Button button in igracPanel.Controls)
            {
                button.BackColor = Color.White;
                button.FlatAppearance.BorderColor = Color.Gray;
                button.FlatAppearance.BorderSize = 1;
            }
            zadnjiPotezAi.Text = "";

            try
            {
                //create fleets and gunner   
                FleetAi = sw.CreateFleet(new int[] { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 });
                FleetIgrac = sw.CreateFleet(new int[] { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 });
                gunner = new Gunner(10, 10, new int[] { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 });

                //store and display ship counts
                shipCountAi = FleetAi.Ships.Count();
                shipCountIgrac = FleetIgrac.Ships.Count();
                brodoviAi.Text = "Ships left (AI): " + shipCountAi;
                brodoviIgrac.Text = "Ships left (Igrac): " + shipCountIgrac;

                //add ships to map
                foreach (var ship in FleetIgrac.Ships)
                {
                    foreach (var square in ship.squares)
                    {
                        foreach (Button button in igracPanel.Controls)
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
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Neuspješno slaganje brodova");
            }

        }


    }
}
