using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using vsite.oom.battleship.model;

namespace BattleshipGUI
{
    public partial class Form1 : Form
    {
        private const int gridSize = 30;
        private const int gridSpacing = 10;
        private const int gridRows = 10;
        private const int gridColumns = 10;
        private readonly int[] shipLengths = { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 };

        private FleetGrid fleetGrid;
        private ShotsGrid shotsGrid;
        private FleetBuilder fleetBuilder;
        private Fleet playerFleet;
        private Fleet enemyFleet;
        private Gunnery enemyGunnery;

        private Button[,] fleetGridButtons;
        private Button[,] shotsGridButtons;

        private Button[] selectedButtons;
        private int currentShipIndex;
        private int selectedCount;

        private List<Button> deployedShipButtons;
        private bool fleetDeploymentComplete;

        public Form1()
        {
            //initialization
            InitializeComponent();
            fleetGrid = new FleetGrid(gridRows, gridColumns);
            fleetGridButtons = InitializeGrid(fleetGrid, 50, 50);

            shotsGrid = new ShotsGrid(gridRows, gridColumns);
            shotsGridButtons = InitializeGrid(shotsGrid, 50 + (gridSize + gridSpacing) * gridColumns + 50, 50);

            int spaceBetweenGrids = 50;
            this.ClientSize = new Size(
                2 * 50 + 2 * gridColumns * (gridSize + gridSpacing),
                50 + gridRows * (gridSize + gridSpacing) + spaceBetweenGrids + 50
            );

            fleetBuilder = new FleetBuilder(gridRows, gridColumns, shipLengths);

            playerFleet = new Fleet();
            enemyFleet = fleetBuilder.CreateFleet();

            enemyGunnery = new Gunnery(gridRows, gridColumns, shipLengths);

            currentShipIndex = 0;
            selectedCount = 0;
            selectedButtons = new Button[shipLengths[currentShipIndex]];

            deployedShipButtons = new List<Button>();

            fleetDeploymentComplete = false;

            foreach (Button button in fleetGridButtons)
            {
                button.Click += FleetGridButton_Click;
            }

            foreach (Button button in shotsGridButtons)
            {
                button.Click += ShotsGridButton_Click;
                button.Enabled = false;
            }
        }

        //Initialize button grid
        private Button[,] InitializeGrid(Grid gridModel, int startX, int startY)
        {
            Button[,] gridButtons = new Button[gridRows, gridColumns];
            for (int row = 0; row < gridRows; row++)
            {
                for (int col = 0; col < gridColumns; col++)
                {
                    gridButtons[row, col] = new Button();
                    gridButtons[row, col].Size = new Size(gridSize, gridSize);
                    gridButtons[row, col].Location = new Point(startX + col * (gridSize + gridSpacing), startY + row * (gridSize + gridSpacing));
                    gridButtons[row, col].Tag = new Point(row, col);
                    this.Controls.Add(gridButtons[row, col]);
                }
            }
            return gridButtons;
        }

        //Event handler for button click for fleet grid
        private void FleetGridButton_Click(object sender, EventArgs e)
        {
            if (fleetDeploymentComplete)
            {
                MessageBox.Show("All ships have been deployed.");
                return;
            }

            Button clickedButton = sender as Button;
            Point position = (Point)clickedButton.Tag;

            if (fleetGrid.IsSquareAvailable(position.X, position.Y) &&
                !deployedShipButtons.Contains(clickedButton) &&
                AreSurroundingSquaresEmpty(position.X, position.Y))
            {
                if (selectedButtons.Any(btn => btn == clickedButton))
                {
                    MessageBox.Show("Square already selected. Choose a different square.");
                    return;
                }

                selectedButtons[selectedCount++] = clickedButton;
                clickedButton.BackColor = Color.Yellow;

                if (selectedCount == shipLengths[currentShipIndex])
                {
                    List<Square> selectedSquares = new List<Square>();
                    List<Point> selectedPositions = new List<Point>();
                    for (int i = 0; i < selectedButtons.Length; i++)
                    {
                        Point selectedPosition = (Point)selectedButtons[i].Tag;
                        Square selectedSquare = fleetGrid.Squares.First(s => s.Row == selectedPosition.X && s.Column == selectedPosition.Y);
                        selectedSquares.Add(selectedSquare);
                        selectedPositions.Add(selectedPosition);
                    }

                    if (!AreSquaresInStraightLine(selectedPositions))
                    {
                        MessageBox.Show("Ships must be placed in a straight line.");
                        ResetSelectedButtons();
                        return;
                    }

                    if (!AreSquaresConnected(selectedPositions))
                    {
                        MessageBox.Show("Ships must be connected and not touch each other.");
                        ResetSelectedButtons();
                        return;
                    }

                    playerFleet.CreateShip(selectedSquares);

                    foreach (Button btn in selectedButtons)
                    {
                        btn.BackColor = Color.Blue;
                        btn.Enabled = false;
                        deployedShipButtons.Add(btn);
                    }

                    // Reset selectedButtons and selectedCount for the next ship
                    selectedCount = 0;
                    if (++currentShipIndex < shipLengths.Length)
                    {
                        selectedButtons = new Button[shipLengths[currentShipIndex]];
                    }
                    else
                    {
                        fleetDeploymentComplete = true;
                        MessageBox.Show("Deployment phase complete. Initiate combat phase.");

                        // Enable shots grid buttons
                        foreach (Button button in shotsGridButtons)
                        {
                            button.Enabled = true;
                        }

                        // Show deployed enemy ships on shotsGrid
                        foreach (var ship in enemyFleet.Ships)
                        {
                            foreach (var square in ship.Squares)
                            {
                                shotsGridButtons[square.Row, square.Column].BackColor = Color.Yellow;
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Invalid position for ship placement.");
            }
        }

        //Check if deployed ship is deployed in a straight line
        private bool AreSquaresInStraightLine(List<Point> positions)
        {
            var firstPosition = positions[0];
            var secondPosition = positions[1];

            if (firstPosition.X == secondPosition.X)
            {
                for (int i = 2; i < positions.Count; i++)
                {
                    if (positions[i].X != firstPosition.X)
                        return false;
                }
                return true;
            }
            else if (firstPosition.Y == secondPosition.Y)
            {
                for (int i = 2; i < positions.Count; i++)
                {
                    if (positions[i].Y != firstPosition.Y)
                        return false;
                }
                return true;
            }

            return false;
        }

        //Check method for deployed ships
        private bool AreSquaresConnected(List<Point> positions)
        {
            for (int i = 0; i < positions.Count - 1; i++)
            {
                var currentPos = positions[i];
                var nextPos = positions[i + 1];

                if (Math.Abs(currentPos.X - nextPos.X) > 1 || Math.Abs(currentPos.Y - nextPos.Y) > 1)
                {
                    return false;
                }

                for (int row = nextPos.X - 1; row <= nextPos.X + 1; row++)
                {
                    for (int col = nextPos.Y - 1; col <= nextPos.Y + 1; col++)
                    {
                        if (row >= 0 && row < gridRows && col >= 0 && col < gridColumns)
                        {
                            Button btn = fleetGridButtons[row, col];
                            if (deployedShipButtons.Contains(btn))
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }

        private bool AreSurroundingSquaresEmpty(int row, int col)
        {
            for (int i = row - 1; i <= row + 1; i++)
            {
                for (int j = col - 1; j <= col + 1; j++)
                {
                    if (i >= 0 && i < gridRows && j >= 0 && j < gridColumns)
                    {
                        Button btn = fleetGridButtons[i, j];
                        if (deployedShipButtons.Contains(btn))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        //Reset the selected buttons during ship deployment
        private void ResetSelectedButtons()
        {
            foreach (Button btn in selectedButtons)
            {
                if (btn != null)
                {
                    btn.BackColor = default(Color);
                }
            }
            selectedCount = 0;
            selectedButtons = new Button[shipLengths[currentShipIndex]];
        }

        //Event handler for button click for shots grid
        private void ShotsGridButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            Point position = (Point)clickedButton.Tag;

            HitResult hitResult = enemyFleet.Hit(position.X, position.Y);

            if (hitResult == HitResult.Hit)
            {
                clickedButton.BackColor = Color.Green;
            }
            else
            {
                clickedButton.BackColor = Color.Red;
            }

            clickedButton.Enabled = false;

            if (AllShipsSunken(enemyFleet))
            {
                MessageBox.Show("You won!");
                RestartGame();
                return;
            }

            // Enemy takes a shot after each player shot
            Square enemyTarget = enemyGunnery.Next();
            HitResult enemyHitResult = playerFleet.Hit(enemyTarget.Row, enemyTarget.Column);

            Button enemyTargetButton = fleetGridButtons[enemyTarget.Row, enemyTarget.Column];
            if (enemyHitResult == HitResult.Hit || enemyHitResult == HitResult.Sunken)
            {
                enemyTargetButton.BackColor = Color.Green;
            }
            else
            {
                enemyTargetButton.BackColor = Color.Gray;
            }

            enemyTargetButton.Enabled = false;

            enemyGunnery.ProcessHitResult(enemyHitResult);

            if (AllShipsSunken(playerFleet))
            {
                MessageBox.Show("You lost!");
                RestartGame();
            }
        }

        //Check if all ships are sunken
        private bool AllShipsSunken(Fleet fleet)
        {
            return fleet.Ships.All(ship => ship.Squares.All(square => square.IsHit));
        }

        //Restart game
        private void RestartGame()
        {
            if (MessageBox.Show("Do you want to play again?", "Restart Game", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Restart();
            }
            else
            {
                Application.Exit();
            }
        }
    }
}
