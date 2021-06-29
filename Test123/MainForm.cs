using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Vsite.Oom.Battleship.Model;

namespace BattleshipGUI
{
    public partial class MainForm : Form
    {
        private const int nRows = 10;
        private const int nColumns = 10;
        private static int enemyShipsAlive = 10;
        private static int myShipsAlive = 10;
        private static int mySquaresLeft = 30;
        private static int enemySquaresLeft = 30;
        private List<int> shipLengths;
        private Fleet myFleet, enemyFleet;
        private readonly Stopwatch stopWatch = new Stopwatch();
        private readonly Gunnery gunner = new Gunnery(nRows, nColumns, new List<int> { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 });         
        private readonly GridLabel[,] gridLabel = new GridLabel[nRows + 1, nColumns + 1];
        private readonly GridButton[,] myGridDraw = new GridButton[nRows, nColumns];
        private readonly GridButton[,] enemyGridDraw = new GridButton[nRows, nColumns];        

        public MainForm()
        {
            InitializeComponent();
            DrawPanel(myGridDraw, 50);
            DrawPanel(enemyGridDraw, 700);           
        }

        // PLACE FLEET BUTTON
        private void PlaceFleetButton_Click(object sender, EventArgs e)
        {
            PlaceMyFleet();
            PlaceEnemyFleet();
            EnableButtons(enemyGridDraw, true);            
            MessageBox.Show("You can start. Good Luck!", "Play", MessageBoxButtons.OK, MessageBoxIcon.Information);
            stopWatch.Start();
            PlaceFleetButton.Enabled = false;
        }

        // GRID DRAW
        private void DrawPanel(GridButton[,] gridDraw, int startLeft)
        {
            for (int i = 0; i < nRows; i++)
            {
                for (int j = 0; j < nColumns; j++)
                {
                    gridDraw[i, j] = new GridButton(nRows, nColumns)
                    {
                        Row = i,
                        Column = j,
                        BackColor = Color.GhostWhite,
                        Location = new Point(startLeft + i * 40, 60 + j * 40),
                        Size = new Size(40, 40),
                        Enabled = false,
                        TabStop = false,
                        FlatStyle = FlatStyle.Flat
                    };
                    gridDraw[i, j].FlatAppearance.BorderSize = 1;
                    gridDraw[i, j].FlatAppearance.BorderColor = Color.Black;                    
                    gridDraw[i, j].Click += ButtonClick;                   
                    Controls.Add(gridDraw[i, j]);
                }
            }                  

            // LABELS DRAW
            char columnLetter = 'A';
            for (int i = 0; i < nRows + 1; i++)
            {
                for (int j = 0; j < nColumns + 1; j++)
                {
                    gridLabel[i, j] = new GridLabel(nRows, nColumns);

                    if (i != 0) // i >= 1
                    {
                        gridLabel[i, j].Text = columnLetter.ToString();                        
                    }

                    if (j != 0) // i >= 1
                    {
                        gridLabel[i, j].Text = j.ToString();                        
                    }

                    gridLabel[i, j].Location = new Point(startLeft + i * 40 - 25, 70 + j * 40 - 35);
                    gridLabel[i, j].Size = new Size(20, 20);
                    gridLabel[i, j].Font = new Font("Times New Roman", 8);
                    Controls.Add(gridLabel[i, j]);
                }

                if (i != 0) // i >= 1
                {
                    columnLetter++;
                }
            }
            myFleetGroupBox.SendToBack();
            enemyFleetGroupBox.SendToBack();
        }

        // ENABLE BUTTONS
        private void EnableButtons(GridButton[,] gridDraw, bool enable)
        {
            for (int i = 0; i < nRows; i++)
            {
                for (int j = 0; j < nColumns; j++)
                {
                    gridDraw[i, j].Enabled = enable;
                }
            }
        }

        // PAINTING SHIP SQUARE (MY FLEET)
        private void FleetButtonFillColorForMyFleet(int nRows, int nColumn, Color c)
        {
            myGridDraw[nRows, nColumn].BackColor = c;                 
        }

        // PAINTING SHIP SQUARE (ENEMY FLEET)
        private void FleetButtonFillColorForEnemyFleet(int nRows, int nColumn, Color c)
        {
            enemyGridDraw[nRows, nColumn].BackColor = c;
        }

        // ANIMATION SUNKEN SHIP (MY FLEET)
        private void AnimateColorForMyFleet(int nRows, int nColumn)
        {
            myGridDraw[nRows, nColumn].AnimateButtonColor(Color.DarkRed);
        }

        // ANIMATION SUNKEN SHIP (ENEMY FLEET)
        private void AnimateColorForEnemyFleet(int nRows, int nColumn)
        {
            enemyGridDraw[nRows, nColumn].AnimateButtonColor(Color.DarkRed);
        }

        // STOPWATCH
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            stopWatchLabel.Text = stopWatch.Elapsed.ToString("mm\\:ss\\.ff");
        }

        // USER WON MESSAGE
        private void IWonDisplay()
        {
            DialogResult msgBoxResult = MessageBox.Show("YOU WON!" + Environment.NewLine + Environment.NewLine 
                + "Time: " + stopWatch.Elapsed.ToString("mm\\:ss\\.ff")
                + Environment.NewLine + Environment.NewLine 
                + "Press 'OK' if you want to play again or 'Cancel' to exit",
                  "Winner!", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (msgBoxResult == DialogResult.OK)
            {
                Application.Restart();
            }
            else if (msgBoxResult == DialogResult.Cancel)
            {
                Application.Exit();
            }
        }        

        // ENEMY WON MESSAGE
        private void EnemyWonDisplay()
        {
            DialogResult msgBoxResult = MessageBox.Show("ENEMY WON!" + Environment.NewLine + Environment.NewLine 
                + "Time: " + stopWatch.Elapsed.ToString("mm\\:ss\\.ff")
                + Environment.NewLine + Environment.NewLine 
                + "Press 'OK' if you want to play again or 'Cancel' to exit",
                  "Winner!", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (msgBoxResult == DialogResult.OK)
            {
                Application.Restart();
            }
            else if (msgBoxResult == DialogResult.Cancel)
            {
                Application.Exit();
            }
        }

        // PLACE MY FLEET
        private void PlaceMyFleet()
        {
            Color myFleetColor = Color.GhostWhite;
            shipLengths = new List<int> { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 };
            Shipwright sw = new Shipwright(10, 10, shipLengths);
            myFleet = sw.CreateFleet(new List<int> { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 });

            try
            {
                if (myFleetColorDialog.ShowDialog() == DialogResult.OK)
                {
                    myFleetColor = myFleetColorDialog.Color;
                    myShipsAliveLabel.Text = myShipsAlive.ToString();
                    mySquaresLeftLabel.Text = mySquaresLeft.ToString();
                    enemyShipsAliveLabel.Text = enemyShipsAlive.ToString();
                    enemySquaresLeftLabel.Text = enemySquaresLeft.ToString();
                }
                else
                {
                    MessageBox.Show("You have to choose color for your fleet!", "Choose color", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    if (myFleetColorDialog.ShowDialog() == DialogResult.OK)
                    {
                        myFleetColor = myFleetColorDialog.Color;
                        myShipsAliveLabel.Text = myShipsAlive.ToString();
                        mySquaresLeftLabel.Text = mySquaresLeft.ToString();
                        enemyShipsAliveLabel.Text = enemyShipsAlive.ToString();
                        enemySquaresLeftLabel.Text = enemySquaresLeft.ToString();
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("You didn't choose the color, if you want to play start the appliacation again!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                Application.Exit();
            }            

            foreach (Ship ship in myFleet.Ships)
            {
                foreach (Square sq in ship.Squares)
                {
                    FleetButtonFillColorForMyFleet(sq.row, sq.column, myFleetColor);
                }
            }
        }

        // PLACE ENEMY FLEET
        private void PlaceEnemyFleet()
        {
            Color enemyFleetColor = Color.GhostWhite;
            shipLengths = new List<int> { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 };
            Shipwright sw = new Shipwright(10, 10, shipLengths);
            enemyFleet = sw.CreateFleet(new List<int> { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 });

            foreach (Ship ship in enemyFleet.Ships)
            {
                foreach (Square sq in ship.Squares)
                {
                    FleetButtonFillColorForEnemyFleet(sq.row, sq.column, enemyFleetColor);
                }
            }
        }

        // GAME LOGIC (MY TURN)
        private void ButtonClick(object sender, EventArgs e)
        {
            GridButton btn = sender as GridButton;
            Square sqClicked = new Square(btn.Row, btn.Column);
            HitResult result = enemyFleet.Hit(sqClicked);           

            switch (result)
            {
                case HitResult.Missed:
                    btn.BackColor = Color.DarkGray;
                    btn.Text = "X";                    
                    btn.Font = new Font(Text, 20);
                    btn.Enabled = false;
                    EnemyTurn();
                    break;

                case HitResult.Hit:
                    btn.BackColor = Color.Red;                    
                    btn.Enabled = false;
                    enemySquaresLeft--;
                    enemySquaresLeftLabel.Text = enemySquaresLeft.ToString();
                    EnemyTurn();
                    break;

                case HitResult.Sunken:
                    foreach (var sunkenSquare in enemyFleet.Ships.Where(s => s.Squares.Contains(sqClicked)).SelectMany(s => s.Squares))
                    {
                        AnimateColorForEnemyFleet(sunkenSquare.row, sunkenSquare.column);
                        btn.Enabled = false;
                    }

                    enemyShipsAlive--;
                    enemySquaresLeft--;
                    enemyShipsAliveLabel.Text = enemyShipsAlive.ToString();
                    enemySquaresLeftLabel.Text = enemySquaresLeft.ToString();

                    if (enemyShipsAlive == 0)
                    {
                        stopWatch.Stop();
                        IWonDisplay();
                    }
                    EnemyTurn();
                    break;

                default:
                    break;
            }           
        }

        // GAME LOGIC (ENEMY TURN)
        private void EnemyTurn()
        {
            Square field = gunner.NextTarget();
            HitResult result = myFleet.Hit(field);
            gunner.RecordShootingResult(result);            

            switch (result)
            {
                case HitResult.Missed:
                    myGridDraw[field.row, field.column].BackColor = Color.DarkGray;
                    myGridDraw[field.row, field.column].Text = "X";
                    myGridDraw[field.row, field.column].Font = new Font(Text, 20);
                    break;

                case HitResult.Hit:
                    mySquaresLeft--;
                    mySquaresLeftLabel.Text = mySquaresLeft.ToString();
                    myGridDraw[field.row, field.column].BackColor = Color.Red;
                    break;

                case HitResult.Sunken:
                    foreach (var sunkenSquare in myFleet.Ships.Where(s => s.Squares.Contains(field)).SelectMany(s => s.Squares))
                    {
                        AnimateColorForMyFleet(sunkenSquare.row, sunkenSquare.column);
                    }

                    myShipsAlive--;
                    mySquaresLeft--;
                    myShipsAliveLabel.Text = myShipsAlive.ToString();
                    mySquaresLeftLabel.Text = mySquaresLeft.ToString();

                    if (myShipsAlive == 0)
                    {
                        stopWatch.Stop();
                        EnemyWonDisplay();                        
                    }
                    break;

                default:
                    break;
            }            
        }
    }
}