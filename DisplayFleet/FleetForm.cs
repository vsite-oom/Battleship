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




namespace DisplayFleet
{
    public partial class FleetForm : Form
    {
        public FleetForm()
        {
            InitializeComponent();
            DrawBoard(player, 45);
            DrawBoard(enemy, 450);

        }

        private void displayFleet(object sender, EventArgs e)
        {
            wipeBoard(player);
            wipeBoard(enemy);
            int[] shipSizes = new int[] { 5,4,4,3,3,3,2,2,2,2 };
            Shipwright b = new Shipwright(Rows, Columns);
            // Creates non displayable 
            var flota = b.CreateFleet(shipSizes);
            var enemyFlota = b.CreateFleet(shipSizes);
            playerFleet = flota;
            enemyFleet = enemyFlota;
            //
            //Displays player fleet, enemy fleet exists but only in memory
            foreach (Ship ship in flota.Ships)
            {
                foreach (Square p in ship.Squares)
                {
                    player[p.Row, p.Column].BackColor = System.Drawing.SystemColors.ControlDarkDark;
                }
            }

        }
        private void DrawBoard(PanelButton[,] p, int pos)
        {

            for (int i = 0; i<Rows;i++)
            {
                for(int j = 0; j<Columns;j++)
                {
                    p[i, j] = new PanelButton();
                    p[i, j].i = i;
                    p[i, j].j = j;
                    p[i, j].BackColor = System.Drawing.SystemColors.ControlLightLight;
                    p[i, j].Location = new System.Drawing.Point(pos + i * 35, 45 + j * 35);
                    p[i, j].Size = new System.Drawing.Size(35, 35);
                    p[i, j].TabStop = false;
                    p[i, j].Click += HitSquare;
                    this.Controls.Add(p[i, j]);
                }
            }
        }

        private void wipeBoard(Button[,] p)
        {
            for(int i = 0; i<Rows;i++)
            {
                for(int j = 0; j<Columns; j++)
                {
                    p[i, j].BackColor = System.Drawing.SystemColors.GradientActiveCaption;
                }
            }
        }

        private void HitSquare(object sender, EventArgs e)
        {
            PanelButton button = sender as PanelButton;
            Square clicked = new Square(button.i, button.j);
            HitResult result = enemyFleet.Hit(clicked);
            switch(result)
            {
                case HitResult.Hit:
                    {
                        button.BackColor = Color.FromArgb(255, 0, 0);
                        break;
                    }
                case HitResult.Missed:
                    {
                        button.BackColor = Color.FromArgb(211, 211, 211);
                        break;
                    }
                case HitResult.Sunken:
                    {
                        foreach (var sunken in enemyFleet.Ships.Where(s => s.Squares.Contains(clicked)).SelectMany(s => s.Squares))
                            enemy[sunken.Row, sunken.Column].BackColor = Color.FromArgb(255, 255, 255);
                        break;
                    }

            }
        }



        Fleet playerFleet;
        Fleet enemyFleet;
        int Rows = 10;
        int Columns = 10;
        PanelButton[,] player = new PanelButton[10, 10];
        PanelButton[,] enemy = new PanelButton[10, 10];

        private void quitGame(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            align.Enabled = false;
            startGame.Enabled = false;
        }
    }
}
