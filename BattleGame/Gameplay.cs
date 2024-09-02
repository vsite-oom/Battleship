namespace Vsite.Oom.Battleship.Game
{
    internal class Gameplay
    {
        private const int gridRow = 10;
        private const int gridColumn = 10;

        private int[] playerSquaresAndLocations;
        private int[] enemySquaresAndLocations;
        private EventHandler gridButtonClickHandler;

        public Gameplay(Panel player, Panel enemy, EventHandler gridButtonClickHandler)
        {
            // Store the event handler for grid button clicks
            this.gridButtonClickHandler = gridButtonClickHandler;

            // Initialize the arrays to store square dimensions and start locations for player and enemy grids
            playerSquaresAndLocations = new int[3];
            enemySquaresAndLocations = new int[3];

            // Determine the square dimensions and start locations for the player's grid
            DetermineSquareDimensions(playerSquaresAndLocations, player);

            // Determine the square dimensions and start locations for the enemy's grid
            DetermineSquareDimensions(enemySquaresAndLocations, enemy);

            // Initialize the player's battle grid with the calculated dimensions and start locations
            InitializeBattleGrid(StringFile.igrac, player, playerSquaresAndLocations[0], playerSquaresAndLocations[1], playerSquaresAndLocations[2], Color.LavenderBlush);

            // Initialize the enemy's battle grid with the calculated dimensions and start locations
            InitializeBattleGrid(StringFile.neprijatelj, enemy, enemySquaresAndLocations[0], enemySquaresAndLocations[1], enemySquaresAndLocations[2], Color.LavenderBlush);
        }

        private void DetermineSquareDimensions(int[] squaresAndLocations, Panel panel)
        {
            int optimalSquare = panel.Size.Width;
            if (optimalSquare > panel.Size.Height)
            {
                optimalSquare = panel.Size.Height;
            }

            int squareSize = (int)((optimalSquare - 115) / gridColumn);
            if (squareSize < 5)
            {
                squareSize = 5;
            }

            int totalGridWidth = squareSize * gridColumn;
            int totalGridHeight = squareSize * gridRow;

            int startLocationX = (panel.Size.Width - totalGridWidth) / 2;
            int startLocationY = (panel.Size.Height - totalGridHeight) / 2;

            // Update the elements of the original array
            squaresAndLocations[0] = squareSize;
            squaresAndLocations[1] = startLocationX;
            squaresAndLocations[2] = startLocationY;
        }

        private void InitializeBattleGrid(string fieldName, Panel panel, int squareSize, int startLocationX, int startLocationY, Color backgroundColor)
        {
            // Set the background color of the panel
            panel.BackColor = backgroundColor;

            // Add column labels (A, B, C, ...) to the top of the grid
            for (var col = 0; col < gridColumn; ++col)
            {
                var label = new Label
                {
                    Text = StringFile.gridColumnLetters[col].ToString(),
                    Font = new Font("Segoe", 8, FontStyle.Bold),
                    Size = new Size(squareSize, squareSize),
                    Location = new Point(startLocationX + 5 + col * squareSize + 2, startLocationY - squareSize),
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = Color.Black,
                    BackColor = backgroundColor
                };
                panel.Controls.Add(label);
            }

            // Add row labels (1, 2, 3, ...) to the left side of the grid
            for (var row = 0; row < gridRow; ++row)
            {
                var label = new Label
                {
                    Text = (row + 1).ToString(),
                    Font = new Font("Segoe", 8, FontStyle.Bold),
                    Size = new Size(squareSize, squareSize),
                    Location = new Point(startLocationX - (int)(0.8 * squareSize), startLocationY + 5 + row * squareSize + 2),
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = Color.Black,
                    BackColor = backgroundColor
                };
                panel.Controls.Add(label);

                // Add buttons to the grid for each cell
                for (var col = 0; col < gridColumn; ++col)
                {
                    panel.Controls.Add(CreateGridButton(fieldName, row, col, squareSize, startLocationX + 5 + col * squareSize + 2, startLocationY + row * squareSize + 2));
                }
            }
        }

        private Button CreateGridButton(string fieldName, int row, int column, int size, int position_X, int position_Y)
        {
            // Create a new button
            var button = new Button
            {
                BackColor = Color.White,
                ForeColor = Color.ForestGreen,
                Location = new Point(position_X, position_Y),
                Name = $"{StringFile.combatBtn}{column}:{row}",
                Size = new Size(size, size),
                Tag = (column, row),
                Text = "",
                UseVisualStyleBackColor = false,
                BackgroundImageLayout = ImageLayout.Stretch,
                Enabled = fieldName != StringFile.igrac
            };

            // Attach the provided event handler to the button's Click event
            button.Click += new EventHandler(gridButtonClickHandler);

            // Return the created button
            return button;
        }
    }
}
