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

        private CustomButton[,] playerButtons = new CustomButton[gridSize, gridSize];
        private CustomButton[,] computerButtons = new CustomButton[gridSize, gridSize];

        private FleetBuilder playerFleetBuilder = new FleetBuilder(gridSize, gridSize, new int[] { 2, 2, 2, 2, 3, 3, 3, 4, 4, 5 });
        private FleetBuilder computerFleetBuilder = new FleetBuilder(gridSize, gridSize, new int[] { 2, 2, 2, 2, 3, 3, 3, 4, 4, 5 });

        private Fleet? playerFleet;
        private Fleet? computerFleet;

        private bool playerTurn = true;
        private bool gameStarted = false;

        private int playerShipsSunk = 0;
        private int computerShipsSunk = 0;

        public Form1()
        {
            InitializeComponent();
            btnStartReset.Enabled = false;
            CreateGrids(PlayerGridLeftMargin, ComputerGridLeftMargin, topMargin);
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
                        CustomButton playerButton = CreateButton(i, j, PlayerGridLeftMargin, topMargin);
                        playerButtons[i - 1, j - 1] = playerButton;
                        Controls.Add(playerButton);

                        CustomButton computerButton = CreateButton(i, j, ComputerGridLeftMargin, topMargin);
                        computerButtons[i - 1, j - 1] = computerButton;
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
                Location = new Point(leftMargin + i * gridSquareSize, topMargin + j * gridSquareSize),
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

        // ************************************************************
        // *                    EVENT HANDLERS                        *
        // ************************************************************

        private void btnPlaceFleet_Click(object sender, EventArgs e)
        {
            // 1. Clear player grid (color grid to LightGray)
            ClearGrid(playerButtons);

            // 2. Create player fleet
            playerFleet = playerFleetBuilder.CreateFleet();

            // 3. Place player fleet on the grid (color grid to Gray)
            PlaceFleetOnGrid(playerFleet, playerButtons);

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
                ClearGrid(computerButtons);

                // 3. Create computer fleet
                computerFleet = computerFleetBuilder.CreateFleet();

                // 4. Determine who starts the game <----------------------- !
                Random random = new Random();
                playerTurn = random.Next(0, 2) != 0;

                // Debugging: Place computer fleet on the grid (color grid to Gray) for testing purposes.
                PlaceFleetOnGrid(computerFleet, computerButtons);

                gameStarted = true; // <------------------------------------ !
            }
            else
            {
                gameStarted = false;
                btnPlaceFleet.Enabled = true;
            }




        }
    }
}
