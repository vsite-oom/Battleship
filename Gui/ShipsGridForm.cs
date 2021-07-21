using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Vsite.Oom.Battleship.Model;

namespace BattleshipGUI {
    public partial class ShipsGridForm : Form {

        private List<int> shipLengths;
        private readonly Stopwatch stop_watch = new Stopwatch();
        private Fleet my_ships, enemy_ships;

        private readonly Gunnery gunner = new Gunnery(rows, columns, new List<int> { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 });
        private readonly GridLabel[,] gridLabel = new GridLabel[rows + 1, columns + 1];
        private readonly Grid_Buttons[,] my_grid_draw = new Grid_Buttons[rows, columns];
        private readonly Grid_Buttons[,] enemy_grid_draw = new Grid_Buttons[rows, columns];

        private const int rows = 10;
        private const int columns = 10;
        private static int numbe_of_enemy_ships_alive = 10;
        private static int number_of_my_ships_alive = 10;
        private static int number_of_my_squares_left = 30;
        private static int number_of_enemy_squares_left = 30;

        public ShipsGridForm() {
            InitializeComponent();
            panel_draw(my_grid_draw, 50);
            panel_draw(enemy_grid_draw, 700);
        }

        private void PlaceFleetButton_Click(object sender, EventArgs e) {
            place_my_ships();
            place_enemy_ships();

            enable_buttons(enemy_grid_draw, true);

            stop_watch.Start();

            Start_Button.Enabled = false;
        }


        private void panel_draw(Grid_Buttons[,] draw_grid, int startLeft) {
            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < columns; j++) {
                    draw_grid[i, j] = new Grid_Buttons(rows, columns) {
                        grid_button_row = i,
                        grid_button_column = j,

                        BackColor = Color.White,

                        Location = new Point(startLeft + i * 40, 60 + j * 40),
                        Size = new Size(40, 40),

                        Enabled = false,
                        TabStop = false,

                        FlatStyle = FlatStyle.Flat
                    };
                    draw_grid[i, j].FlatAppearance.BorderSize = 1;
                    draw_grid[i, j].FlatAppearance.BorderColor = Color.Black;
                    draw_grid[i, j].Click += ButtonClick;
                    Controls.Add(draw_grid[i, j]);
                }
            }
            my_ships_GroupBox.SendToBack();
            enemy_ships_GroupBox.SendToBack();
        }

        private void enable_buttons(Grid_Buttons[,] gridDraw, bool enable) {
            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < columns; j++) {
                    gridDraw[i, j].Enabled = enable;
                }
            }
        }

        private void ships_button_fill_color_for_my_ships(int nRows, int nColumn, Color c) {
            my_grid_draw[nRows, nColumn].BackColor = c;
        }

        private void ships_button_fill_color_for_enemy_ships(int nRows, int nColumn, Color c) {
            enemy_grid_draw[nRows, nColumn].BackColor = c;
        }
        private void animate_my_ships(int nRows, int nColumn) {
            my_grid_draw[nRows, nColumn].animate_button(Color.DarkRed);
            my_grid_draw[nRows, nColumn].Text = "X";
            my_grid_draw[nRows, nColumn].Font = new Font(Text, 20);
        }

        private void animate_enemy_ships(int nRows, int nColumn) {
            enemy_grid_draw[nRows, nColumn].animate_button(Color.DarkRed);
            enemy_grid_draw[nRows, nColumn].Text = "X";
            enemy_grid_draw[nRows, nColumn].Font = new Font(Text, 20);
        }

        private void GameTimer_Tick(object sender, EventArgs e) {
            stop_watch_Label.Text = stop_watch.Elapsed.ToString("mm\\:ss\\.ff");
        }



        private void I_WonOrLostDisplay(bool iWon) {
            string lostOrWon = "You lost!";
            if (iWon)
                lostOrWon = "You won!";

            DialogResult msgBoxResult = msgBoxResult = MessageBox.Show(lostOrWon + Environment.NewLine + Environment.NewLine
             + "Time in game: " + stop_watch.Elapsed.ToString("mm\\:ss\\.ff")
             + Environment.NewLine + Environment.NewLine + "Press OK to play again",
               "Game over", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (msgBoxResult == DialogResult.OK) {
                Application.Restart();
            } else if (msgBoxResult == DialogResult.Cancel) {
                Application.Exit();
            }
        }

        private void place_my_ships() {
            Color my_ships_color = Color.White;
            shipLengths = new List<int> { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 };
            Shipwright sw = new Shipwright(10, 10, shipLengths);
            my_ships = sw.CreateShips(new List<int> { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 });

            my_ships_color = Color.DeepSkyBlue;


            foreach (Ship ship in my_ships.Ships) {
                foreach (Square sq in ship.Squares) {
                    ships_button_fill_color_for_my_ships(sq.row, sq.column, my_ships_color);
                }
            }
        }

        private void place_enemy_ships() {
            Color enemyFleetColor = Color.White;
            shipLengths = new List<int> { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 };
            Shipwright sw = new Shipwright(10, 10, shipLengths);
            enemy_ships = sw.CreateShips(new List<int> { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 });

            foreach (Ship ship in enemy_ships.Ships) {
                foreach (Square sq in ship.Squares) {
                    ships_button_fill_color_for_enemy_ships(sq.row, sq.column, enemyFleetColor);
                }
            }
        }

        private void ButtonClick(object sender, EventArgs e) {
            Grid_Buttons btn = sender as Grid_Buttons;
            Square sqClicked = new Square(btn.grid_button_row, btn.grid_button_column);
            HitResult result = enemy_ships.Hit(sqClicked);

            switch (result) {
                case HitResult.Missed:
                    btn.BackColor = Color.DarkGray;
                    btn.Enabled = false;
                    enemy_turn();
                    break;

                case HitResult.Hit:
                    btn.BackColor = Color.Red;
                    btn.Text = "X";
                    btn.Font = new Font(Text, 20);
                    btn.Enabled = false;
                    enemy_turn();
                    break;

                case HitResult.Sunken:
                    foreach (var sunkenSquare in enemy_ships.Ships.Where(s => s.Squares.Contains(sqClicked)).SelectMany(s => s.Squares)) {
                        animate_enemy_ships(sunkenSquare.row, sunkenSquare.column);
                    }

                    numbe_of_enemy_ships_alive--;
                    number_of_enemy_squares_left--;

                    if (numbe_of_enemy_ships_alive == 0) {
                        stop_watch.Stop();
                        I_WonOrLostDisplay(true);
                    }
                    enemy_turn();
                    break;

                default:
                    break;
            }
        }

        private void enemyFleetGroupBox_Enter(object sender, EventArgs e) {

        }
        private void MainForm_Load(object sender, EventArgs e) {

        }

        private void my_fleet_GroupBox_Enter(object sender, EventArgs e) {

        }

        private void time_TextLabel_Click(object sender, EventArgs e) {

        }

        private void enemy_turn() {
            Square field = gunner.NextTarget();
            HitResult result = my_ships.Hit(field);
            gunner.RecordShootingResult(result);

            switch (result) {
                case HitResult.Missed:
                    my_grid_draw[field.row, field.column].BackColor = Color.Gray;
                    break;

                case HitResult.Hit:
                    number_of_my_squares_left--;
                    my_grid_draw[field.row, field.column].Text = "X";
                    my_grid_draw[field.row, field.column].BackColor = Color.Red;
                    my_grid_draw[field.row, field.column].Font = new Font(Text, 20);
                    break;

                case HitResult.Sunken:
                    foreach (var sunkenSquare in my_ships.Ships.Where(s => s.Squares.Contains(field)).SelectMany(s => s.Squares)) {
                        animate_my_ships(sunkenSquare.row, sunkenSquare.column);

                    }

                    number_of_my_ships_alive--;
                    number_of_my_squares_left--;

                    if (number_of_my_ships_alive == 0) {
                        stop_watch.Stop();
                        I_WonOrLostDisplay(false);
                    }
                    break;

                default:
                    break;
            }
        }
    }
}