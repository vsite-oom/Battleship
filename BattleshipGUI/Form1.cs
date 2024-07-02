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

        private void Form1_Load(object sender, EventArgs e)
        {

        }

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

        private void FleetGridButton_Click(object sender, EventArgs e)
        {

            Button clickedButton = sender as Button;
            Point position = (Point)clickedButton.Tag;

            if (fleetGrid.IsSquareAvailable(position.X, position.Y) &&
                !deployedShipButtons.Contains(clickedButton))
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
                    for (int i = 0; i < selectedButtons.Length; i++)
                    {
                        Point selectedPosition = (Point)selectedButtons[i].Tag;
                        Square selectedSquare = fleetGrid.Squares.First(s => s.Row == selectedPosition.X && s.Column == selectedPosition.Y);
                        selectedSquares.Add(selectedSquare);
                    }

                    if (AreSquaresAdjacentToDeployedShips(selectedSquares))
                    {
                        MessageBox.Show("Ships must have an empty space between them.");
                        foreach (Button btn in selectedButtons)
                        {
                            btn.BackColor = default(Color);
                        }
                        selectedCount = 0;
                        selectedButtons = new Button[shipLengths[currentShipIndex]];
                        return;
                    }

                    playerFleet.CreateShip(selectedSquares);

                    foreach (Button btn in selectedButtons)
                    {
                        btn.BackColor = Color.Blue;
                        btn.Enabled = false;
                        deployedShipButtons.Add(btn);
                    }

                    selectedCount = 0;
                    if (++currentShipIndex < shipLengths.Length)
                    {
                        selectedButtons = new Button[shipLengths[currentShipIndex]];
                    }
                    else
                    {
                        fleetDeploymentComplete = true;
                        MessageBox.Show("Deployment phase complete. Initiate combat phase..");

                        foreach (Button button in shotsGridButtons)
                        {
                            button.Enabled = true;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Invalid position for ship placement.");
            }
        }

        private bool AreSquaresAdjacentToDeployedShips(List<Square> selectedSquares)
        {
            foreach (var square in selectedSquares)
            {
                int row = square.Row;
                int column = square.Column;

                for (int r = row - 1; r <= row + 1; r++)
                {
                    for (int c = column - 1; c <= column + 1; c++)
                    {
                        if (r >= 0 && r < gridRows && c >= 0 && c < gridColumns)
                        {
                            Button btn = fleetGridButtons[r, c];
                            if (deployedShipButtons.Contains(btn))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        private void ShotsGridButton_Click(object sender, EventArgs e)
        {

            Button clickedButton = sender as Button;
            Point position = (Point)clickedButton.Tag;

            var hitResult = enemyFleet.Hit(position.X, position.Y);

            if (hitResult != HitResult.Missed)
            {
                clickedButton.BackColor = Color.Green;
            }
            else
            {
                clickedButton.BackColor = Color.Red;
            }

            clickedButton.Enabled = false;

            EnemyTakesShot();
        }

        private void EnemyTakesShot()
        {
            Square targetSquare = enemyGunnery.Next();
            var hitResult = playerFleet.Hit(targetSquare.Row, targetSquare.Column);
            enemyGunnery.ProcessHitResult(hitResult);

            Button targetButton = fleetGridButtons[targetSquare.Row, targetSquare.Column];
            if (hitResult != HitResult.Missed)
            {
                targetButton.BackColor = Color.Red;
            }
            else
            {
                targetButton.BackColor = Color.Gray;
            }

            targetButton.Enabled = false;
        }
    }
}
