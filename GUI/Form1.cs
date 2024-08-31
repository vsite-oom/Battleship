using System.Xml.Serialization;
using Vsite.Oom.Battleship.Model;

namespace GUI
{
    public partial class Form1 : Form
    {
        private const int gridSize = 10;
        private const int gridSquareSize = 50;
        private const int PlayerGridLeftMargin = 150;
        private const int ComputerGridLeftMargin = 800;
        private const int topMargin = 100;

        private CustomButton[,] playerGridButtons = new CustomButton[gridSize, gridSize];  // Buttons representing squares on the grid for placing player's fleet and recording computer's shots
        private CustomButton[,] computerGridButtons = new CustomButton[gridSize, gridSize];  // Buttons representing squares on the grid for placing computer's fleet and recording player's shots

        private int[] shipLengths = new int[] { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 };

        private FleetBuilder playerFleetBuilder;
        private FleetBuilder computerFleetBuilder;
        private Fleet? playerFleet;
        private Fleet? computerFleet;

        private bool playerTurn = true;
        private bool gameStarted = false;

        private int playerShipsSunk = 0;
        private int computerShipsSunk = 0;

        private Gunnery playerGunnery;
        private Gunnery computerGunnery;

        public Form1()
        {
            InitializeComponent();
            btnStartReset.Enabled = false;
            CreateGrids(PlayerGridLeftMargin, ComputerGridLeftMargin, topMargin);
            playerFleetBuilder = new FleetBuilder(gridSize, gridSize, shipLengths);
            computerFleetBuilder = new FleetBuilder(gridSize, gridSize, shipLengths);
            playerGunnery = new Gunnery(gridSize, gridSize, shipLengths);
            computerGunnery = new Gunnery(gridSize, gridSize, shipLengths);
        }

        private void CreateGrids(int playerGridLeftMargin, int computerGridLeftMargin, int topMargin)
        {
            for (int i = 0; i <= gridSize; i++)
            {
                for (int j = 0; j <= gridSize; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        continue;
                    }
                    else if (i == 0)
                    {
                        Controls.Add(CreateLetterLabel(j, 150 + j * gridSquareSize, 100));  // Player grid column letters
                        Controls.Add(CreateLetterLabel(j, 800 + j * gridSquareSize, 100));  // Computer grid column letters
                    }
                    else if (j == 0)
                    {
                        Controls.Add(CreateNumberLabel(i, 150, 100 + i * gridSquareSize));  // Player grid row numbers
                        Controls.Add(CreateNumberLabel(i, 800, 100 + i * gridSquareSize));  // Computer grid row numbers
                    }
                    else  // Create squares buttons
                    {
                        CustomButton playerButton = CreateButton(i - 1, j - 1, PlayerGridLeftMargin, topMargin);  // i - 1 and j - 1 because Model.Grid is 0-based
                        playerButton.Enabled = false;
                        playerGridButtons[i - 1, j - 1] = playerButton;
                        Controls.Add(playerButton);

                        CustomButton computerButton = CreateButton(i - 1, j - 1, ComputerGridLeftMargin, topMargin);
                        computerButton.Click += btnComputerGridButton_Click;
                        computerButton.Enabled = false;
                        computerGridButtons[i - 1, j - 1] = computerButton;
                        Controls.Add(computerButton);
                    }
                }
            }
        }

        // ************************************************************
        // *                    HELPER METHODS                        *
        // ************************************************************

        private CustomButton CreateButton(int i, int j, int leftMargin, int topMargin)
        {
            CustomButton button = new CustomButton(i, j)
            {
                Location = new Point(leftMargin + (j + 1) * gridSquareSize, topMargin + (i + 1) * gridSquareSize),  // +1 because of row and column labels
                Name = "button" + i + j,
                Size = new Size(gridSquareSize, gridSquareSize),
                TabIndex = (i * gridSize) + j,
                Text = "",
                UseVisualStyleBackColor = true
            };

            return button;
        }

        private Label CreateNumberLabel(int i, int leftMargin, int topMargin)
        {
            Label label = new Label
            {
                AutoSize = false,
                Location = new Point(leftMargin, topMargin),
                Name = "label" + i,
                Size = new Size(gridSquareSize, gridSquareSize),
                TabIndex = i,
                Text = i.ToString(),
                Padding = new Padding(0, 0, 10, 0),
                Font = new Font("Regular", 12),
                TextAlign = ContentAlignment.MiddleRight
            };

            return label;
        }

        private Label CreateLetterLabel(int j, int leftMargin, int topMargin)
        {
            Label label = new Label
            {
                AutoSize = false,
                Location = new Point(leftMargin, topMargin),
                Name = "label" + j,
                Size = new Size(gridSquareSize, gridSquareSize),
                TabIndex = j,
                Text = ((char)(j + 64)).ToString(),  // 65 is ASCII code for 'A'. 1 + 64 = 'A', 2 + 64 = 'B', etc.
                Padding = new Padding(0, 0, 0, 10),
                Font = new Font("Regular", 12),
                TextAlign = ContentAlignment.BottomCenter
            };

            return label;
        }

        private void ClearGrid(CustomButton[,] buttons)
        {
            for (int i = 1; i < (gridSize + 1); i++)
            {
                for (int j = 1; j < (gridSize + 1); j++)
                {
                    var button = buttons[i - 1, j - 1];
                    button.BackColor = Color.LightGray;
                }
            }
        }

        private void PlaceFleetOnGrid(Fleet fleet, CustomButton[,] buttons)
        {
            foreach (var ship in fleet.Ships)
            {
                foreach (var square in ship.Squares)
                {
                    var button = buttons[square.Row, square.Column];
                    button.BackColor = Color.Gray;
                }
            }
        }

        private void EnableGrid(CustomButton[,] buttons)
        {
            for (int i = 1; i < (gridSize + 1); i++)
            {
                for (int j = 1; j < (gridSize + 1); j++)
                {
                    var button = buttons[i - 1, j - 1];
                    button.Enabled = true;
                }
            }
        }

        private void DisableGrid(CustomButton[,] buttons)
        {
            for (int i = 1; i < (gridSize + 1); i++)
            {
                for (int j = 1; j < (gridSize + 1); j++)
                {
                    var button = buttons[i - 1, j - 1];
                    button.Enabled = false;
                }
            }
        }

        private void playerTurnLogic()
        {
            // Enable computer grid
            EnableGrid(computerGridButtons);
        }

        private void computerTurnLogic()
        {
            // Disable computer grid
            DisableGrid(computerGridButtons);
            playerTurnLogic();
            //MessageBox.Show("computer logic!");
        }

        // ************************************************************
        // *                    EVENT HANDLERS                        *
        // ************************************************************

        private void btnPlaceFleet_Click(object sender, EventArgs e)
        {
            // 1. Clear player grid (color grid to LightGray)
            ClearGrid(playerGridButtons);

            // 2. Create player fleet
            playerFleet = playerFleetBuilder.CreateFleet();

            // 3. Place player fleet on the grid (color grid to Gray)
            PlaceFleetOnGrid(playerFleet, playerGridButtons);

            // 4. Enable Start/Reset button
            btnStartReset.Enabled = true;
        }

        private void btnStartReset_Click(object sender, EventArgs e)
        {
            if (gameStarted == false)
            {
                // 1. Disable Place Fleet button
                btnPlaceFleet.Enabled = false;

                // 2. Clear computer grid (color grid to LightGray)
                ClearGrid(computerGridButtons);

                // 3. Create computer fleet
                computerFleet = computerFleetBuilder.CreateFleet();

                // 4. Determine who starts the game <----------------------- !
                Random random = new Random();
                playerTurn = random.Next(0, 2) != 0;

                // Debugging: Place computer fleet on the grid (color grid to Gray) for testing purposes.
                PlaceFleetOnGrid(computerFleet, computerGridButtons);

                gameStarted = true; // <------------------------------------ !

                // 5. Call the method to handle the game logic
                if (playerTurn)
                {
                    playerTurnLogic();
                }
                else
                {
                    computerTurnLogic();
                }
            }
            else if (gameStarted == true)
            {
                gameStarted = false;
                btnPlaceFleet.Enabled = true;
            }
        }

        // Event handler for when a player clicks on a square on the computer grid
        private void btnComputerGridButton_Click(object sender, EventArgs e)
        {
            CustomButton customButton = (CustomButton)sender;
            customButton.Enabled = false;
            playerGunnery.Next();
            HitResult hitResult = computerFleet.Hit(customButton.Row, customButton.Column);

            if (hitResult == HitResult.Missed)
            {
                customButton.BackColor = Color.Blue;
                playerTurn = false;
                computerTurnLogic();
            }
            else if (hitResult == HitResult.Hit)
            {
                customButton.BackColor = Color.Orange;
                playerTurn = false;
                computerTurnLogic();
            }
            else if (hitResult == HitResult.Sunken)
            {
                foreach (var ship in computerFleet.Ships)
                {
                    foreach (var square in ship.Squares)
                    {
                        if (square.SquareState == SquareState.Sunken)
                        {
                            computerGridButtons[square.Row, square.Column].BackColor = Color.Red;
                        }
                    }
                }

                computerShipsSunk++;

                if (computerShipsSunk == computerFleet.Ships.Count())
                {
                    MessageBox.Show("Player wins!");
                    gameStarted = false;
                    btnPlaceFleet.Enabled = true;
                }
            }
        }
    }
}
