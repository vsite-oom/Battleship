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
            startGame.Enabled = true;
            wipeBoard(player);
            wipeBoard(enemy);

            int[] shipSizes = new int[] { 5,4,4,3,3,3,2,2,2,2 };
            Shipwright b = new Shipwright(Rows, Columns);
            // Creates non displayable 
            var flota = b.CreateFleet(shipSizes);
            var enemyFlota = b.CreateFleet(shipSizes);
            playerFleet = flota;
            enemyFleet = enemyFlota;
            gunner = new Gunner(Rows, Columns, shipSizes);
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
                    p[i, j].Location = new System.Drawing.Point(pos + i * 35, 50 + j * 35);
                    p[i, j].Size = new System.Drawing.Size(35, 35);
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
            if (gameOn == false)
                return;
            PanelButton button = sender as PanelButton;
            Square clicked = new Square(button.i, button.j);
            HitResult result = enemyFleet.Hit(clicked);
            switch(result)
            {
                case HitResult.Hit:
                    {
                        button.BackColor = Color.FromArgb(255, 225, 0);
                        break;
                    }
                case HitResult.Missed:
                    {
                        button.BackColor = Color.FromArgb(211, 211, 211);
                        EnemyTurn();
                        break;
                    }
                case HitResult.Sunken:
                    {
                        foreach (var sunken in enemyFleet.Ships.Where(s => s.Squares.Contains(clicked)).SelectMany(s => s.Squares))
                            enemy[sunken.Row, sunken.Column].BackColor = Color.FromArgb(255, 0, 0);
                        userScore++;
                        userPoint.Text = userScore + "/ 10";
                        if(userScore == 10)
                        {
                            foreach (var sunken in enemyFleet.Ships.Where(s => s.Squares.Contains(clicked)).SelectMany(s => s.Squares))
                                enemy[sunken.Row, sunken.Column].BackColor = Color.FromArgb(255, 0, 0);
                            string message = "YOU WIN, Yes to close, No to restart";
                            string title = "YOU WIN";
                            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                            DialogResult score = MessageBox.Show(message, title, buttons);
                            if (score == DialogResult.Yes)
                            {
                                this.Close();
                            }
                            else
                            {
                                Application.Restart();
                            }

                        }
                        break;
                    }

            }
        }

        private void EnemyTurn()
        {
            Square clicked = gunner.NextTarget();
            HitResult result = playerFleet.Hit(clicked);
            gunner.ProcessHitResult(result);
            switch(result)
            {
                case HitResult.Hit:
                    {
                        player[clicked.Row, clicked.Column].BackColor = Color.FromArgb(255,233, 0);
                        System.Threading.Thread.Sleep(500);
                        EnemyTurn();
                        break;
                    }
                case HitResult.Missed:
                    {
                        player[clicked.Row, clicked.Column].BackColor = Color.FromArgb(255, 255, 255);
                        break;
                    }
                case HitResult.Sunken:
                    {
                        foreach (var sunken in playerFleet.Ships.Where(s => s.Squares.Contains(clicked)).SelectMany(s => s.Squares))
                            player[sunken.Row, sunken.Column].BackColor = Color.FromArgb(0, 0, 225);
                        enemyScore++;
                        enemyPoint.Text = enemyScore + "/ 10";
                        if (enemyScore == 10)
                        {
                            foreach (var sunken in playerFleet.Ships.Where(s => s.Squares.Contains(clicked)).SelectMany(s => s.Squares))
                                player[sunken.Row, sunken.Column].BackColor = Color.FromArgb(0, 0, 225);
                            string message = "YOU Lose, Yes to close, No to restart";
                            string title = "YOU Lose";
                            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                            DialogResult score = MessageBox.Show(message, title, buttons);
                            if (score == DialogResult.Yes)
                            {
                                this.Close();
                            }
                            else
                            {
                                Application.Restart();
                            }
                            break;

                        }
                        EnemyTurn();
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
        Gunner gunner;
        bool gameOn = false;
        int userScore, enemyScore = 0;
        

        private void quitGame(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            align.Enabled = false;
            startGame.Enabled = false;
            gameOn = true;
        }

        private void nameButton_Click(object sender, EventArgs e)
        {
            align.Visible = true;
            startGame.Visible = true;
            button1.Visible = true;
            startGame.Enabled = false;
            nameLabel.Text = "Admiral " + textNameUser.Text;
            enemyLabel.Text = "Admiral " + textNameEnemy.Text;
            userPoint.Text = userScore + "/ 10";
            enemyPoint.Text = enemyScore + "/ 10";
            textNameUser.Visible = false;
            textNameEnemy.Visible = false;
            nameButton.Visible = false;
        }
    }
}
