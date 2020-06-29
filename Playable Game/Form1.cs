using System;
using System.Windows.Forms;
using Vsite.Oom.Battleship.Model;
using System.Drawing;
using System.Linq;

namespace Playable_Game
{
    public partial class Battleships : Form
    {
        public Battleships()
        {
            InitializeComponent();
            buttonField(playerField, 60, 120);
            buttonField(enemyField, 560, 120);
            ableToShoot = false;
        }

        class ButtonField : Button
        {
            public int x;
            public int y;
        }

        private void buttonField(ButtonField[,] grid, int locationX, int locationY)
        {

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    grid[i, j] = new ButtonField();
                    grid[i, j].x = i;
                    grid[i, j].y = j;
                    grid[i, j].Size = new Size(40, 40);
                    grid[i, j].Location = new Point(locationX + i * grid[i,j].Size.Width, locationY + j * grid[i, j].Size.Height);
                    grid[i, j].BackColor = Color.FromArgb(255, 255, 255);
                    grid[i, j].Click += OnClickSquares;
                    Controls.Add(grid[i, j]);
                }
            }
        }

        private void OnClickSquares(object sender, EventArgs e)
        {
            if (ableToShoot == false)
            {
                return;
            }

            ButtonField button = sender as ButtonField;
            Square point = new Square(button.x, button.y);
            HitResult hitResult = enemyFleet.Hit(point);

            switch (hitResult)
            {
                case HitResult.Hit:
                    {
                        button.BackColor = Color.FromArgb(177, 91, 255);
                        break;
                    }
                case HitResult.Missed:
                    {
                        button.BackColor = Color.FromArgb(170, 170, 170);
                        NextTurn();
                        break;
                    }
                case HitResult.Sunken:
                    {
                        foreach (var sunken in enemyFleet.Ships.Where(s => s.Squares.Contains(point)).SelectMany(s => s.Squares))
                        {
                            enemyField[sunken.Row, sunken.Column].BackColor = Color.FromArgb(84, 0, 159);
                        }

                        --enemyShipsRemaining;
                        if (enemyShipsRemaining == 0)
                        {
                            MessageBox.Show("You Win!", "Battleships");
                            Close();
                        }
                        break;
                    }
            }
        }

        private void NextTurn()
        {
            Square point = gunner.NextTarget();
            HitResult hitResult = playerFleet.Hit(point);
            gunner.ProcessHitResult(hitResult);

            switch (hitResult)
            {
                case HitResult.Missed:
                    {
                        playerField[point.Row, point.Column].BackColor = Color.FromArgb(170, 170, 170);
                        break;
                    }
                case HitResult.Hit:
                    {
                        playerField[point.Row, point.Column].BackColor = Color.FromArgb(125, 78, 0);
                        NextTurn();
                        break;
                    }  
                case HitResult.Sunken:
                    {
                        foreach (var sunken in playerFleet.Ships.Where(s => s.Squares.Contains(point)).SelectMany(s => s.Squares))
                        {
                            playerField[sunken.Row, sunken.Column].BackColor = Color.FromArgb(60, 40, 0);
                        }

                        --playerShipsRemaining;
                        if (playerShipsRemaining == 0)
                        {
                            MessageBox.Show("You Lose!", "Battleships");
                            Close();
                            break;
                        }

                        NextTurn();
                        break;
                    }
            }
        }

        private void ClearFleet(Button[,] fleet)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    fleet[i, j].BackColor = Color.FromArgb(255, 255, 255);
                }
            }
        }

        private void fleet_setup_Click(object sender, EventArgs e)
        {
            ClearFleet(playerField);
            ClearFleet(enemyField);
            play.Enabled = true;
            Shipwright ship = new Shipwright(rows, columns);
            var player_fleet = ship.CreateFleet(shipSize);
            var enemy_fleet = ship.CreateFleet(shipSize);
            playerFleet = player_fleet;
            enemyFleet = enemy_fleet;
            gunner = new Gunner(rows, columns, shipSize);

            foreach (Ship ships in player_fleet.Ships)
            {
                foreach (Square fleet in ships.Squares)
                {
                    playerField[fleet.Row, fleet.Column].BackColor = Color.FromArgb(240, 150, 20);
                }
            }
        }
        
        private void play_Click(object sender, EventArgs e)
        {
            fleet_setup.Enabled = false;
            ableToShoot = true;
        }

        private void quit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private int rows = 10;
        private int columns = 10;
        private int[] shipSize = new int[] { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 };
        private int playerShipsRemaining = 10;
        private int enemyShipsRemaining = 10;
        private bool ableToShoot = false;
        private ButtonField[,] playerField = new ButtonField[10, 10];
        private ButtonField[,] enemyField = new ButtonField[10, 10];
        private Fleet playerFleet;
        private Fleet enemyFleet;
        private Gunner gunner;
    }
}