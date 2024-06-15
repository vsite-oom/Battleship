using Vsite.Oom.Battleship.Model;

namespace UI_Battleship
{
    public partial class NewGameForm : Form
    {
        private FleetBuilder? pcBuilder;
        private Fleet? fleet;
        private const int GridSize = 10;
        private Button[,]? buttonList;

        public NewGameForm()
        {
            InitializeComponent();
            InitializeGameGrid();
            StartNewGame();
        }

        private void InitializeGameGrid()
        {
            buttonList = new Button[GridSize, GridSize];
            int buttonSize = 30;
            
            for (int row = 0; row < GridSize; row++)
            {
                for (int col = 0; col < GridSize; col++)
                {
                    Button button = new Button
                    {
                        Size = new Size(buttonSize, buttonSize),
                        Location = new Point(col * buttonSize, row * buttonSize),
                        BackColor = Color.LightBlue,
                        Tag = new Point(row, col)
                    };
                    button.Click += Button_Click;
                    buttonList[row, col] = button;
                    this.Controls.Add(button);
                }
            }


        }

        private void StartNewGame()
        {
            int[] shipLengths = { 5, 4, 3, 3, 2 }; // Example ship lengths
            pcBuilder = new FleetBuilder(GridSize, GridSize, shipLengths);
            fleet = pcBuilder.CreateFleet();
        }

        private void Button_Click(object? sender, EventArgs e)
        {
            Button button = sender as Button;
            Point point = (Point)button.Tag;
            int row = point.X;
            int col = point.Y;

            // Simulate a hit
            HitResult result = fleet.Hit(row, col);

            // Update button color based on hit result
            if (result == HitResult.Hit)
            {
                button.BackColor = Color.Red;
            }
            else if (result == HitResult.Sunken)
            {
                button.BackColor = Color.DarkRed;
            }
            else
            {
                button.BackColor = Color.Gray;
            }

            button.Enabled = false; // Disable button after it's been clicked
        }
    }
}
