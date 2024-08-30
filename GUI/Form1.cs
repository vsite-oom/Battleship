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

        public Form1()
        {
            InitializeComponent();
            //CreateFleetBuilders();  // Warning otherwise
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

        //private void CreateFleetBuilders()  // Warning otherwise
        //{
        //    playerFleetBuilder = new FleetBuilder(gridSize, gridSize, new int[] { 2, 2, 2, 2, 3, 3, 3, 4, 4, 5 });
        //    computerFleetBuilder = new FleetBuilder(gridSize, gridSize, new int[] { 2, 2, 2, 2, 3, 3, 3, 4, 4, 5 });
        //}

        //
        // Helper methods
        //
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

        //
        // Event handlers
        //
        private void btnPlaceFleet_Click(object sender, EventArgs e)
        {
            // Clear player grid (color grid to LightGray)
            int i = 0;
            int j = 0;
            for (i = 1; i < (gridSize + 1); i++)
            {
                for (j = 1; j < (gridSize + 1); j++)
                {
                    var button = playerButtons[i - 1, j - 1];
                    //var button = Controls.Find("button" + i + j, true).FirstOrDefault();
                    button.BackColor = Color.LightGray;
                }
            }

            // Create player fleet
            playerFleet = playerFleetBuilder.CreateFleet();

            // Place player fleet on the grid (color grid to Gray)
            foreach (var ship in playerFleet.Ships)
            {
                foreach (var square in ship.Squares)
                {
                    var button = playerButtons[square.Row, square.Column];
                    button.BackColor = Color.Gray;
                }
            }
        }
    }
}
