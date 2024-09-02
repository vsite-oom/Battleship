using System.Media;
using Vsite.Oom.Battleship.Model;

namespace Vsite.Oom.Battleship.Game
{
    public partial class BattleGame : Form
    {
        private Gameplay gameplay;
        private SoundPlayer soundPlayer;
        private List<int> ShipLengths = new List<int> { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 };
        private Fleet playerFleet;
        private Fleet enemyFleet;
        private Gunnery shipGunnery;
        private SquareEliminator squareEliminator;
        private List<Button> hitSquares = new List<Button>();
        private List<int> shipsToShoot;
        private int playerHitCounter;
        private int opponentHitCounter;
        private int scoreBooster = 5;
        private int totalTime = 0;
        private const int gridRow = 10;
        private const int gridColumn = 10;

        public BattleGame()
        {
            InitializeComponent();
            InitializeStartSound();

            gameplay = new Gameplay(panel_Player, panel_Opponent, GridButton_Click);
        }

        private void InitializeStartSound()
        {
            string soundFilePath = GetSoundPathFrom(StringFile.baseAudioPath, StringFile.singleGunshot);
            soundPlayer = new SoundPlayer(soundFilePath);
        }

        private string GetSoundPathFrom(string path1, string path2) 
        {
            return Path.Combine(path1, path2);
        }

        private void ResetBattleGrid()
        {
            button_StartStop.Tag = 0;
            button_StartStop.Text = StringFile.startText;
            button_StartStop.ForeColor = Color.ForestGreen;

            playerFleet = null;
            enemyFleet = null;
            shipGunnery = null;
            squareEliminator = null;
            hitSquares.Clear();
            shipsToShoot = null;
            playerHitCounter = 0;
            opponentHitCounter = 0;

            foreach (Button button in panel_Player.Controls.OfType<Button>())
            {
                button.BackColor = Color.White;
                button.Text = "";
                button.Enabled = false;
            }

            foreach (Button button in panel_Opponent.Controls.OfType<Button>())
            {
                button.BackColor = Color.White;
                button.Text = "";
                button.Enabled = true;
            }

            playerHitsLabel.Text = StringFile.mojiBodoviText;
            opponentHitsLabel.Text =StringFile.aiBodoviText;
        }

        private void button_StartStop_Click(object sender, EventArgs e)
        {
            PlaySoundFromPathAndName(StringFile.windowsAudioPath, StringFile.startReset);

            ResetAndStartTimer();
            ResetBattleGrid();

            // Update the start/stop button to indicate the game is in progress
            button_StartStop.Tag = 0;
            button_StartStop.Text = StringFile.restartText;
            button_StartStop.ForeColor = Color.Red;

            // Initialize the list of ships to shoot based on predefined ship lengths
            shipsToShoot = new List<int>(ShipLengths);

            // Initialize the square eliminator for managing grid squares
            squareEliminator = new SquareEliminator();

            // Calculate the initial hit counters for both player and opponent
            playerHitCounter = scoreBooster * ShipLengths.Sum();
            opponentHitCounter = scoreBooster * ShipLengths.Sum();

            // Update the hit counters displayed on the UI
            UpdateHitCounters();

            playerFleet = CreateFleetFromPredefinedLayout();

            // Render the player's fleet on the player's grid
            RenderFleetOnGrid(playerFleet, panel_Player);

            enemyFleet = CreateFleetFromPredefinedLayout();

            // Render the enemy's fleet on the opponent's grid, hiding the ships
            RenderFleetOnGrid(enemyFleet, panel_Opponent, hideShips: true);

            // Initialize the gunnery system for the opponent's shooting logic
            shipGunnery = new Gunnery(gridRow, gridColumn, ShipLengths);

            // Enable the player's shooting functionality
            EnablePlayerShooting();
        }

        private Fleet CreateFleetFromPredefinedLayout()
        {
            var random = new Random();
            var fleet = new Fleet();

            // Select a random predefined layout from the available layouts
            var selectedLayout = PredefinedLayouts.Layouts[random.Next(PredefinedLayouts.Layouts.Count)];

            // Initialize a grid to keep track of the fleet's positions
            var fleetGrid = new FleetGrid(gridRow, gridColumn);

            // Initialize a square eliminator to manage grid squares around ships
            var eliminator = new SquareEliminator();

            // Iterate through each ship position in the selected layout
            foreach (var shipPosition in selectedLayout)
            {
                // Create a list to hold the squares occupied by the current ship
                var shipSquares = new List<Square>();

                // Add squares to the ship based on its length and orientation
                for (int i = 0; i < shipPosition.Length; i++)
                {
                    if (shipPosition.IsHorizontal)
                    {
                        // If the ship is horizontal, add squares to the right
                        shipSquares.Add(new Square(shipPosition.Row, shipPosition.Column + i));
                    }
                    else
                    {
                        // If the ship is vertical, add squares downwards
                        shipSquares.Add(new Square(shipPosition.Row + i, shipPosition.Column));
                    }
                }

                // Create the ship in the fleet using the calculated squares
                fleet.CreateShip(shipSquares);

                // Eliminate squares around the placed ship to prevent overlap
                var toEliminate = eliminator.ToEliminate(shipSquares, fleetGrid.Rows, fleetGrid.Columns);
                foreach (var coordinate in toEliminate)
                {
                    fleetGrid.EliminateSquare(coordinate.Row, coordinate.Column);
                }
            }

            // Return the created fleet
            return fleet;
        }

        private void RenderFleetOnGrid(Fleet fleet, Panel panel, bool hideShips = false)
        {
            // Iterate through each ship in the fleet
            foreach (var ship in fleet.Ships)
            {
                // Iterate through each square occupied by the current ship
                foreach (var sq in ship.Squares)
                {
                    // Find the button corresponding to the current square on the grid
                    var button = panel.Controls.OfType<Button>().FirstOrDefault(b =>
                    {
                        var position = ((int column, int row))b.Tag;
                        return position.row == sq.Row && position.column == sq.Column;
                    });

                    // If the button is found, update its appearance
                    if (button != null)
                    {
                        // Set the button's background color based on whether ships should be hidden
                        button.BackColor = hideShips ? Color.White : Color.Blue;
                    }
                }
            }
        }

        private void EnablePlayerShooting()
        {
            panel_Opponent.Enabled = true;
        }

        private void GridButton_Click(object sender, EventArgs e)
        {
            // Cast the sender to a Button and disable it to prevent further clicks
            var clickedButton = (Button)sender;
            clickedButton.Enabled = false;

            // Extract the column and row from the button's Tag property
            var (col, row) = ((int col, int row))clickedButton.Tag;
            var targetSquare = new Square(row, col);

            // Check if the enemy fleet is initialized
            if (enemyFleet == null)
            {
                // Show a message if the enemy fleet is not initialized
                MessageBox.Show(StringFile.neprijateljskaFlotaMsg);
                return;
            }

            // Attempt to hit the enemy fleet at the target square
            var attackResult = enemyFleet.Hit(targetSquare.Row, targetSquare.Column);
            switch (attackResult)
            {
                case HitResult.Missed:
                    // Play miss sound
                    PlaySoundFromPathAndName(StringFile.baseAudioPath, StringFile.singleGunshot);

                    // Update the button's background color to indicate a miss
                    clickedButton.BackColor = Color.Black;
                    break;

                case HitResult.Hit:
                    // Play hit sound
                    PlaySoundFromPathAndName(StringFile.baseAudioPath, StringFile.singleHit);

                    // Update the button's background color to indicate a hit
                    clickedButton.BackColor = Color.Red;

                    // Decrease the opponent's hit counter and update the UI
                    opponentHitCounter -= scoreBooster;
                    UpdateHitCounters();
                    break;

                case HitResult.Sunken:
                    // Play hit sound
                    PlaySoundFromPathAndName(StringFile.baseAudioPath, StringFile.singleHit);

                    // Update the button's background color to indicate a sunken ship
                    clickedButton.BackColor = Color.Black;

                    // Find the sunken ship in the enemy fleet
                    var sunkenShip = enemyFleet.Ships.First(s => s.Squares.Any(sq => sq.Row == targetSquare.Row && sq.Column == targetSquare.Column));
                    foreach (var sq in sunkenShip.Squares)
                    {
                        // Update the background color of all squares occupied by the sunken ship
                        var button = panel_Opponent.Controls.OfType<Button>().FirstOrDefault(b =>
                        {
                            var position = ((int column, int row))b.Tag;
                            return position.row == sq.Row && position.column == sq.Column;
                        });

                        if (button != null)
                        {
                            button.BackColor = Color.Black;
                            button.Enabled = false;
                        }
                    }

                    // Eliminate squares around the sunken ship to prevent further hits
                    var toEliminate = squareEliminator.ToEliminate(sunkenShip.Squares, gridRow, gridColumn);
                    foreach (var sq in toEliminate)
                    {
                        var button = panel_Opponent.Controls.OfType<Button>().FirstOrDefault(b =>
                        {
                            var position = ((int column, int row))b.Tag;
                            return position.row == sq.Row && position.column == sq.Column;
                        });

                        if (button != null)
                        {
                            button.BackColor = Color.Gray;
                            button.Enabled = false;
                        }
                    }

                    // Remove the sunken ship's length from the list of ships to shoot
                    shipsToShoot.Remove(sunkenShip.Squares.Count());

                    // Decrease the opponent's hit counter and update the UI
                    opponentHitCounter -= scoreBooster;
                    UpdateHitCounters();
                    break;
            }

            // Check if the opponent has lost all their hit points
            if (opponentHitCounter <= 0)
            {
                // Stop the timer and display a win message
                StopTimer();
                MessageBox.Show(StringFile.win, StringFile.gameOver, MessageBoxButtons.OK);

                // Reset the battle grid and update the start button
                ResetBattleGrid();
                button_StartStop.Tag = 0;
                button_StartStop.Text = StringFile.startText;
                button_StartStop.ForeColor = Color.ForestGreen;
                return;
            }
            // Trigger the opponent's turn to shoot
            OpponentIsShooting();
        }

        private void PlaySoundFromPathAndName(string audioPath, string shotName)
        {
            soundPlayer = new SoundPlayer(GetSoundPathFrom(audioPath, shotName));
            try
            {
                soundPlayer.Stop();
                soundPlayer.Play();
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show(StringFile.soundFileMsg + ex.Message);
                return;
            }
        }

        private void OpponentIsShooting()
        {
            // Disable the opponent's panel to prevent user interaction during the opponent's turn
            panel_Opponent.Enabled = false;

            Square target;
            
            try
            {
                // Get the next target square from the ship gunnery system
                target = shipGunnery.Next();
            }
            catch (InvalidOperationException)
            {
                // If no valid target is found, reset the game and update the start button
                ResetBattleGrid();
                button_StartStop.Tag = 0;
                button_StartStop.Text = StringFile.startText;
                button_StartStop.ForeColor = Color.ForestGreen;
                return;
            }

            // Attempt to hit the player's fleet at the target square
            var hitResult = playerFleet.Hit(target.Row, target.Column);

            // Process the result of the hit to update the gunnery system's state
            shipGunnery.ProcessHitResult(hitResult);

            // Find the button corresponding to the target square on the player's grid
            var hitSquare = panel_Player.Controls.OfType<Button>().FirstOrDefault(b =>
            {
                var position = ((int column, int row))b.Tag;
                return position.row == target.Row && position.column == target.Column;
            });

            // If the button is not found, exit the method
            if (hitSquare == null) return;

            // Update the button's appearance based on the hit result
            switch (hitResult)
            {
                case HitResult.Missed:
                    // If the hit missed, color the button black
                    hitSquare.BackColor = Color.Black;
                    break;

                case HitResult.Hit:
                    // If the hit was successful, color the button red and update the player's hit counter
                    hitSquare.BackColor = Color.Red;
                    playerHitCounter -= scoreBooster;
                    UpdateHitCounters();

                    // Add the hit square to the list of hit squares
                    hitSquares.Add(hitSquare);
                    break;

                case HitResult.Sunken:
                    // If a ship was sunk, color the button black
                    hitSquare.BackColor = Color.Black;

                    // Color all previously hit squares of the sunken ship black
                    foreach (var bt in hitSquares)
                    {
                        bt.BackColor = Color.Black;
                    }

                    // Clear the list of hit squares
                    hitSquares.Clear();

                    // Update the player's hit counter
                    playerHitCounter -= scoreBooster;
                    UpdateHitCounters();

                    // Find the sunken ship and eliminate surrounding squares
                    var sunkenShip = playerFleet.Ships.First(s => s.Squares.Any(sq => sq.Row == target.Row && sq.Column == target.Column));
                    var toEliminate = squareEliminator.ToEliminate(sunkenShip.Squares, gridRow, gridColumn);
                    foreach (var sq in toEliminate)
                    {
                        var button = panel_Player.Controls.OfType<Button>().FirstOrDefault(b =>
                        {
                            var position = ((int column, int row))b.Tag;
                            return position.row == sq.Row && position.column == sq.Column;
                        });

                        if (button != null)
                        {
                            button.BackColor = Color.Gray;
                            button.Enabled = false;
                        }
                    }
                    break;
            }

            // Check if the player has lost all their hit points
            if (playerHitCounter <= 0)
            {
                // Stop the timer and display a game over message
                StopTimer();
                MessageBox.Show(StringFile.poraz, StringFile.gameOver, MessageBoxButtons.OK);
                
                // Reset the game and update the start button
                ResetBattleGrid();
                button_StartStop.Tag = 0;
                button_StartStop.Text = StringFile.startText;
                button_StartStop.ForeColor = Color.ForestGreen;
                return;
            }
            // Re-enable the opponent's panel to allow the player to take their turn
            EnablePlayerShooting();
        }

        private void UpdateHitCounters()
        {
            playerHitsLabel.Text = $"{StringFile.mojiBodoviText} {playerHitCounter}";
            opponentHitsLabel.Text = $"{StringFile.aiBodoviText} {opponentHitCounter}";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            totalTime++;
            label1.Text = totalTime.ToString();
        }

        private void ResetAndStartTimer()
        {
            totalTime = 0;
            label1.Text = totalTime.ToString();
            timer1.Start();
        }

        private void StopTimer() 
        {
            timer1.Stop();
        }
    }
}
