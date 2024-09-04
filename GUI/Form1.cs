using Microsoft.VisualBasic.ApplicationServices;
using System.Globalization;
using System.Media;
using System.Reflection;
using System.Resources;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Vsite.Oom.Battleship.Model;

namespace GUI
{
    public partial class Form1 : Form
    {
        // ************************************************************
        // *                   CONSTANTS AND FIELDS                   *
        // ************************************************************

        private const int gridSize = 10;
        private const int gridSquareSize = 50;
        private const int playerGridLeftMargin = 100;
        private const int computerGridLeftMargin = 750;
        private const int topMargin = 50;

        private CustomButton[,] playerGridButtons = new CustomButton[gridSize, gridSize];  // Player's fleet and computer's shots
        private CustomButton[,] computerGridButtons = new CustomButton[gridSize, gridSize];  // Computer's fleet and player's shots

        private int[] shipLengths = new int[] { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 };

        private FleetBuilder? playerFleetBuilder;
        private FleetBuilder? computerFleetBuilder;
        private Fleet? playerFleet;
        private Fleet? computerFleet;

        private bool playerTurn = true;
        private bool gameStarted = false;

        private int playerShipsSunk = 0;
        private int computerShipsSunk = 0;

        private Gunnery? playerGunnery;
        private Gunnery? computerGunnery;

        private CustomButton? lastComputerHitButton;
        private CustomButton? lastPlayerHitButton;
        private String lastHitMarker = "X";

        private Color colorMissed = Color.DodgerBlue;
        private Color colorHit = Color.DarkOrange;
        private Color colorSunken = Color.DarkRed;
        private Color colorFogOfWar = Color.LightGray;
        private Color colorFleet = Color.Gray;

        // ResourceManager is used to load audio files from the resources
        private ResourceManager resourceManager = new ResourceManager("GUI.Form1", Assembly.GetExecutingAssembly());

        // SoundPlayer is used to play audio files
        private SoundPlayer soundMissed;
        private SoundPlayer soundHit;
        private SoundPlayer soundSunken;


        // ************************************************************
        // *                       CONSTRUCTOR                        *
        // ************************************************************

        public Form1()
        {
            InitializeComponent();
            btnStartReset.Enabled = false;
            CreateGrids(playerGridLeftMargin, computerGridLeftMargin, topMargin);

            // Load the audio files from the resources
            soundMissed = LoadSound("soundMissed");
            soundHit = LoadSound("soundHit");
            soundSunken = LoadSound("soundSunken");
        }


        // ************************************************************
        // *                     GRID MANAGEMENT                      *
        // ************************************************************

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
                        // Player grid column letters
                        Controls.Add(CreateLetterLabel(j, playerGridLeftMargin + (j * gridSquareSize), topMargin));
                        // Computer grid column letters
                        Controls.Add(CreateLetterLabel(j, Form1.computerGridLeftMargin + (j * gridSquareSize), topMargin));
                    }
                    else if (j == 0)
                    {
                        // Player grid row numbers
                        Controls.Add(CreateNumberLabel(i, playerGridLeftMargin, topMargin + (i * gridSquareSize)));
                        // Computer grid row numbers
                        Controls.Add(CreateNumberLabel(i, computerGridLeftMargin, topMargin + (i * gridSquareSize)));
                    }
                    else  // Create squares buttons
                    {
                        CustomButton playerButton = CreateButton(i - 1, j - 1, Form1.playerGridLeftMargin, topMargin);  // i - 1 and j - 1 because Model.Grid is 0-based.
                        playerButton.Enabled = false;
                        playerGridButtons[i - 1, j - 1] = playerButton;
                        Controls.Add(playerButton);

                        CustomButton computerButton = CreateButton(i - 1, j - 1, Form1.computerGridLeftMargin, topMargin);
                        computerButton.Click += btnComputerGridButton_Click;
                        computerButton.Enabled = false;
                        computerGridButtons[i - 1, j - 1] = computerButton;
                        Controls.Add(computerButton);
                    }
                }
            }
        }

        private CustomButton CreateButton(int i, int j, int leftMargin, int topMargin)
        {
            CustomButton button = new CustomButton(i, j)
            {
                Location = new Point(leftMargin + ((j + 1) * gridSquareSize), topMargin + ((i + 1) * gridSquareSize)),  // +1 because of row and column labels.
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

        private void PrepareComputerGrid()
        {
            DisableGrid(computerGridButtons);
            for (int i = 1; i < (gridSize + 1); i++)
            {
                for (int j = 1; j < (gridSize + 1); j++)
                {
                    var button = computerGridButtons[i - 1, j - 1];
                    button.BackColor = colorFogOfWar;
                }
            }
            if (lastPlayerHitButton != null)
            {
                // Clear the last hit button (remove the lastHitMarker text)
                lastPlayerHitButton.Text = "";
            }
        }

        private void PreparePlayerGrid()
        {
            for (int i = 1; i < (gridSize + 1); i++)
            {
                for (int j = 1; j < (gridSize + 1); j++)
                {
                    var button = playerGridButtons[i - 1, j - 1];
                    button.BackColor = colorFogOfWar;
                }
            }
            if (lastComputerHitButton != null)
            {
                // Clear the last hit button (remove the lastHitMarker text)
                lastComputerHitButton.Text = "";
            }
        }

        private void PlaceFleetOnGrid(Fleet fleet, CustomButton[,] buttons)
        {
            foreach (var ship in fleet.Ships)
            {
                foreach (var square in ship.Squares)
                {
                    var button = buttons[square.Row, square.Column];
                    button.BackColor = colorFleet;
                }
            }
        }

        private void EnableGrid(CustomButton[,] buttons)
        {
            for (int i = 1; i <= gridSize; i++)
            {
                for (int j = 1; j <= gridSize; j++)
                {
                    // Enable only buttons that are not already hit
                    var button = buttons[i - 1, j - 1];
                    if (button.BackColor == colorFogOfWar || button.BackColor == colorFleet)  // button.BackColor == colorFleet is added for when computer's fleet is shown for testing purposes.
                    {
                        button.Enabled = true;
                    }
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


        // ************************************************************
        // *                        GAME LOGIC                        *
        // ************************************************************

        private void playerTurnLogic()
        {
            // Enable computer grid
            EnableGrid(computerGridButtons);
        }

        private async void computerTurnLogic()
        {
            // Disable computer grid
            DisableGrid(computerGridButtons);

            // Simulate thinking delay
            await Task.Delay(1000);

            // If the game was reset while the computer was "thinking", return so as not to continue the game
            if (gameStarted == false)
            {
                return;
            }

            var target = computerGunnery!.Next();
            HitResult hitResult = playerFleet!.Hit(target.Row, target.Column);  // Warning if '!' is omitted. Warning is false-positive because playerFleet is initialized in btnPlaceFleet_Click.
            computerGunnery.ProcessHitResult(hitResult);

            if (lastComputerHitButton != null)
            {
                lastComputerHitButton.Text = "";
            }

            lastComputerHitButton = playerGridButtons[target.Row, target.Column];
            lastComputerHitButton.Text = lastHitMarker;
            lastComputerHitButton.Font = new Font(lastComputerHitButton.Font, FontStyle.Bold);

            if (hitResult == HitResult.Missed)
            {
                soundMissed.Play();
                playerGridButtons[target.Row, target.Column].BackColor = colorMissed;
                playerTurn = true;
                playerTurnLogic();
            }
            else if (hitResult == HitResult.Hit)
            {
                soundHit.Play();
                playerGridButtons[target.Row, target.Column].BackColor = colorHit;
                playerTurn = true;
                playerTurnLogic();
            }
            else if (hitResult == HitResult.Sunken)
            {
                soundSunken.Play();
                foreach (var ship in playerFleet.Ships)
                {
                    foreach (var square in ship.Squares)
                    {
                        if (square.SquareState == SquareState.Sunken)
                        {
                            playerGridButtons[square.Row, square.Column].BackColor = colorSunken;
                        }
                    }
                }

                playerShipsSunk++;

                if (playerShipsSunk == playerFleet.Ships.Count())
                {
                    MessageBox.Show("Computer wins!");
                    gameReset();
                }
                else
                {
                    playerTurn = true;
                    playerTurnLogic();
                }
            }
        }

        private void gameReset()
        {
            gameStarted = false;
            btnPlaceFleet.Enabled = true;
            btnStartReset.Enabled = false;
            PreparePlayerGrid();
            PrepareComputerGrid();
            playerShipsSunk = 0;
            computerShipsSunk = 0;
        }


        // ************************************************************
        // *                    EVENT HANDLERS                        *
        // ************************************************************

        private void btnPlaceFleet_Click(object sender, EventArgs e)
        {
            // 1. Clear player grid (color grid to color used for empty grid)
            PreparePlayerGrid();

            // 2. Create player fleet
            playerFleetBuilder = new FleetBuilder(gridSize, gridSize, shipLengths);
            playerFleet = playerFleetBuilder.CreateFleet();

            // 3. Place player fleet on the grid (color corresponding grid buttons to color used for fleet)
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

                // 2. Clear computer grid (color grid to color used for empty grid)
                PrepareComputerGrid();

                // 3. Create computer fleet
                computerFleetBuilder = new FleetBuilder(gridSize, gridSize, shipLengths);
                computerFleet = computerFleetBuilder.CreateFleet();

                // 4. Create player and computer gunnery
                playerGunnery = new Gunnery(gridSize, gridSize, shipLengths);
                computerGunnery = new Gunnery(gridSize, gridSize, shipLengths);

                // 5. Determine who starts the game
                Random random = new Random();
                playerTurn = random.Next(0, 2) != 0;

#if DEBUG
                // Place computer fleet on the grid (color grid to color used for fleet) for testing purposes.
                PlaceFleetOnGrid(computerFleet, computerGridButtons);
#endif

                gameStarted = true;

                // Call the method to handle the game logic based on who starts the game
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
                PreparePlayerGrid();
                PrepareComputerGrid();
                btnPlaceFleet.Enabled = true;
                btnStartReset.Enabled = false;
            }
        }

        // Event handler for when a player clicks on a square on the computer grid
        private void btnComputerGridButton_Click(object? sender, EventArgs e)
        {
            if (sender is CustomButton customButton)  // Warning if omitted. Warning is false-positive because the sender is always a CustomButton.
            {
                customButton.Enabled = false;
                HitResult hitResult = computerFleet!.Hit(customButton.Row, customButton.Column);  // Warning if '!' is omitted. Warning is false-positive because computerFleet is initialized in btnStartReset_Click.

                if (lastPlayerHitButton != null)
                {
                    lastPlayerHitButton.Text = "";
                }

                lastPlayerHitButton = customButton;
                lastPlayerHitButton.Text = lastHitMarker;
                lastPlayerHitButton.Font = new Font(lastPlayerHitButton.Font, FontStyle.Bold);

                if (hitResult == HitResult.Missed)
                {
                    soundMissed.Play();
                    customButton.BackColor = colorMissed;
                    playerTurn = false;
                    computerTurnLogic();
                }
                else if (hitResult == HitResult.Hit)
                {
                    soundHit.Play();
                    customButton.BackColor = colorHit;
                    playerTurn = false;
                    computerTurnLogic();
                }
                else if (hitResult == HitResult.Sunken)
                {
                    soundSunken.Play();
                    foreach (var ship in computerFleet.Ships)
                    {
                        foreach (var square in ship.Squares)
                        {
                            if (square.SquareState == SquareState.Sunken)
                            {
                                computerGridButtons[square.Row, square.Column].BackColor = colorSunken;
                            }
                        }
                    }

                    computerShipsSunk++;

                    if (computerShipsSunk == computerFleet.Ships.Count())
                    {
                        MessageBox.Show("Player wins!");
                        gameReset();
                    }
                    else
                    {
                        playerTurn = false;
                        computerTurnLogic();
                    }
                }
            }
        }


        // ************************************************************
        // *                          AUDIO                           *
        // ************************************************************

        private SoundPlayer LoadSound(string resourceName)
        {
            var soundBytes = resourceManager.GetObject(resourceName) as byte[];  // 'as' casts the object to a byte array or returns null if the object is not a byte array.
            if (soundBytes == null)
            {
                throw new ArgumentException("Resource not found or is not a sound file.", nameof(resourceName));
            }

            MemoryStream soundStream = new MemoryStream(soundBytes);
            return new SoundPlayer(soundStream);
        }
    }
}
