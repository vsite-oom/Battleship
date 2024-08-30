using Vsite.Oom.Battleship.Model;

namespace GUI
{
    public partial class Form1 : Form
    {
        private const int GridSize = 10;
        private FleetBuilder playerFleetBuilder;
        private FleetBuilder computerFleetBuilder;

        public Form1()
        {
            InitializeComponent();

            playerFleetBuilder = new FleetBuilder(GridSize, GridSize, new int[] { 2, 2, 2, 2, 3, 3, 3, 4, 4, 5 });
            computerFleetBuilder = new FleetBuilder(GridSize, GridSize, new int[] { 2, 2, 2, 2, 3, 3, 3, 4, 4, 5 });

            PlacePlayerGrids();
        }

        private void PlacePlayerGrids()
        {
            for (int i = 0; i < (GridSize + 1); i++)
            {
                for (int j = 0; j < (GridSize + 1); j++)
                {
                    if (i == 0 && j == 0)
                    {
                        continue;
                    }
                    else if (i == 0)
                    {
                        Controls.Add(CreateLetterLabel(j, 150 + j * 50, 100));  // Fleet grid
                        Controls.Add(CreateLetterLabel(j, 800 + j * 50, 100));  // Shots grid
                    }
                    else if (j == 0)
                    {
                        Controls.Add(CreateNumberLabel(i, 150, 100 + i * 50));  // Fleet grid
                        Controls.Add(CreateNumberLabel(i, 800, 100 + i * 50));  // Shots grid
                    }
                    else
                    {
                        int FleetGridLeftMargin = 150;
                        int ShotsGridLeftMargin = 800;
                        int topMargin = 100;
                        Controls.Add(CreateButton(i, j, FleetGridLeftMargin, topMargin));  // Fleet grid
                        Controls.Add(CreateButton(i, j, ShotsGridLeftMargin, topMargin));  // Shots grid
                    }
                }
            }
        }

        private CustomButton CreateButton(int i, int j, int leftMargin, int topMargin)
        {
            CustomButton button = new CustomButton(i, j)
            {
                Location = new Point(leftMargin + i * 50, topMargin + j * 50),
                Name = "button" + i + j,
                Size = new Size(50, 50),
                TabIndex = (i * GridSize) + j,
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
                Size = new Size(50, 50),
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
                Size = new Size(50, 50),
                TabIndex = j,
                Text = ((char)(j + 64)).ToString(),
                Padding = new Padding(0, 0, 0, 10),
                Font = new Font("Regular", 12),
                TextAlign = ContentAlignment.BottomCenter
            };

            return label;
        }

        private void btnPlaceFleet_Click(object sender, EventArgs e)
        {
            // Clear player grid (color grid to LightGray)
            int i = 0;
            int j = 0;
            for (i = 1; i < (GridSize + 1); i++)
            {
                for (j = 1; j < (GridSize + 1); j++)
                {
                    var button = Controls.Find("button" + i + j, true).FirstOrDefault();
                    button.BackColor = Color.LightGray;  // Ne znam kako dobiti boju na koju je postavljen prilikom stvaranja pa sam ru?no postavio na LightGray.
                }
            }

            // Create player fleet
            var playerFleet = playerFleetBuilder.CreateFleet();

            // Place player fleet on the grid (color grid to Gray)
            foreach (var ship in playerFleet.Ships)
            {
                foreach (var square in ship.Squares)
                {
                    var button = Controls.Find("button" + (square.Row + 1) + (square.Column + 1), true).FirstOrDefault();
                    button.BackColor = Color.Gray;
                }
            }
        }
    }
}
