using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Vsite.Oom.Battleship.Model;

namespace Battlefields
{
    public partial class Form1 : Form
    {
        private const int gridSize = 10;
        private Button[,] playerButtons = new Button[gridSize, gridSize];
        private Button[,] computerButtons = new Button[gridSize, gridSize];
        private Label[] playerTopLabels = new Label[gridSize];
        private Label[] playerLeftLabels = new Label[gridSize];
        private Label[] computerTopLabels = new Label[gridSize];
        private Label[] computerLeftLabels = new Label[gridSize];

        private FleetBuilder playerFleetBuilder;
        private FleetBuilder computerFleetBuilder;
        private Fleet playerFleet; // Flota igrača
        private Fleet computerFleet; // Flota računala
        private Gunnery computerGunnery;
        private bool isGameStarted = false; // Zastavica za praćenje stanja igre
        private int sunkShipsCount = 0; // Brojač potopljenih brodova
        private int playerSunkShipsCount = 0; // Brojač potopljenih brodova igrača
        private bool computerTurnInProgress = false; // Zastavica za praćenje poteza računala

        public Form1()
        {
            InitializeComponent();
            InitializeFleetBuilders();
            InitializeGrids();
            this.Resize += new EventHandler(Form1_Resize);
        }

        private void InitializeFleetBuilders()
        {
            playerFleetBuilder = new FleetBuilder(gridSize, gridSize, new int[] { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 });
            computerFleetBuilder = new FleetBuilder(gridSize, gridSize, new int[] { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            button.Enabled = false;
            try
            {
                ResetGame();
                isGameStarted = true;
                EnableGrids(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error resetting game: {ex.Message}");
            }
            finally
            {
                button.Enabled = true;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Kod za label1_Click
        }

        private void InitializeGrids()
        {
            CreateGrid(0, 0, null, playerButtons, playerTopLabels, playerLeftLabels, panel1);
            CreateGrid(0, 0, Button_Click_Player, computerButtons, computerTopLabels, computerLeftLabels, panel2);
            EnableGrids(false);
        }

        private void CreateGrid(int startRow, int startCol, EventHandler buttonClickHandler, Button[,] buttonArray, Label[] topLabels, Label[] leftLabels, Panel panel)
        {
            int buttonSize = Math.Min(panel.Width / (gridSize + 1), panel.Height / (gridSize + 1));

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
                        Label lbl = new Label
                        {
                            Text = j.ToString(),
                            Size = new Size(buttonSize, buttonSize),
                            TextAlign = ContentAlignment.MiddleCenter,
                            Location = new Point((startCol + j) * buttonSize, startRow * buttonSize)
                        };
                        topLabels[j - 1] = lbl;
                        panel.Controls.Add(lbl);
                    }
                    else if (j == 0)
                    {
                        Label lbl = new Label
                        {
                            Text = ((char)('A' + i - 1)).ToString(),
                            Size = new Size(buttonSize, buttonSize),
                            TextAlign = ContentAlignment.MiddleCenter,
                            Location = new Point(startCol * buttonSize, (startRow + i) * buttonSize)
                        };
                        leftLabels[i - 1] = lbl;
                        panel.Controls.Add(lbl);
                    }
                    else
                    {
                        Button btn = new Button
                        {
                            Size = new Size(buttonSize, buttonSize),
                            Location = new Point((startCol + j) * buttonSize, (startRow + i) * buttonSize),
                            BackColor = Color.White
                        };
                        if (buttonClickHandler != null)
                        {
                            btn.Click += buttonClickHandler;
                        }
                        btn.Tag = new Point(i - 1, j - 1);
                        buttonArray[i - 1, j - 1] = btn;
                        panel.Controls.Add(btn);
                    }
                }
            }
        }

        private void Button_Click_Computer(object sender, EventArgs e)
        {
            if (!isGameStarted || computerTurnInProgress) return;

            computerTurnInProgress = true;

            try
            {
                var target = computerGunnery.Next();
                var result = playerFleet.Hit(target.Row, target.Column);

                var btn = playerButtons[target.Row, target.Column];
                btn.Enabled = false; // Onemogućavanje gumba nakon gađanja

                if (result == HitResult.Hit || result == HitResult.Sunken)
                {
                    btn.BackColor = Color.Red; // Pogodak
                    if (result == HitResult.Sunken)
                    {
                        MarkSunkShip(playerFleet, playerButtons);
                        playerSunkShipsCount++;
                        if (playerSunkShipsCount >= 10)
                        {
                            score.Text = "Computer Won!";
                            score.ForeColor = Color.Red;
                            EnableGrids(false);
                            return; //Kraj igre ako računalo pobijedi
                        }
                    }
                }
                else
                {
                    btn.BackColor = Color.LightBlue; // Promašaj
                }

                computerGunnery.ProcessHitResult(result);
                score.Text = $"{ConvertToPosition(new Point(target.Row, target.Column))} - {result}!";
            }
            catch (InvalidOperationException)
            {
                score.Text = "Computer has no valid targets!";
                score.ForeColor = Color.Red;
                EnableGrids(false);
            }
            finally
            {
                computerTurnInProgress = false;
            }
        }

        private void Button_Click_Player(object sender, EventArgs e)
        {
            if (!isGameStarted) return;

            Button btn = sender as Button;
            Point gridPosition = (Point)btn.Tag;
            btn.Enabled = false; // Onemogućavanje gumba nakon gađanja

            var result = computerFleet.Hit(gridPosition.X, gridPosition.Y);

            if (result == HitResult.Hit || result == HitResult.Sunken)
            {
                btn.BackColor = Color.Red; // Pogodak
                if (result == HitResult.Sunken)
                {
                    MarkSunkShip(computerFleet, computerButtons);
                    sunkShipsCount++;
                    if (sunkShipsCount >= 10)
                    {
                        score.Text = "You Won!";
                        score.ForeColor = Color.Red; // Postavljanje boje teksta na crvenu
                        EnableGrids(false);
                        return; //Kraj ako igrač pobijedi
                    }
                }
            }
            else
            {
                btn.BackColor = Color.LightBlue; // Promašaj
            }

            Button_Click_Computer(null, null); // Pozivanje poteza kompjutera
        }

        private string ConvertToPosition(Point gridPosition)
        {
            char row = (char)('A' + gridPosition.X);
            int col = gridPosition.Y + 1;
            return $"{row}{col}";
        }

        private bool IsShipPart(Fleet fleet, Point gridPosition)
        {
            foreach (Ship ship in fleet.Ships)
            {
                foreach (Square square in ship.Squares)
                {
                    if (square.Row == gridPosition.X && square.Column == gridPosition.Y)
                        return true;
                }
            }
            return false;
        }

        private void MarkSunkShip(Fleet fleet, Button[,] buttonArray)
        {
            foreach (Ship ship in fleet.Ships)
            {
                if (ship.Squares.All(square => square.IsHit))
                {
                    foreach (Square square in ship.Squares)
                    {
                        buttonArray[square.Row, square.Column].BackColor = Color.Black;
                    }
                }
            }
        }

        private void SetFleetOnBoard()
        {
            playerFleet = playerFleetBuilder.CreateFleet();
            foreach (Ship ship in playerFleet.Ships)
            {
                foreach (Square square in ship.Squares)
                {
                    playerButtons[square.Row, square.Column].BackColor = Color.DarkBlue;
                }
            }

            computerFleet = computerFleetBuilder.CreateFleet();
            foreach (Ship ship in computerFleet.Ships)
            {
                foreach (Square square in ship.Squares)
                {
                    computerButtons[square.Row, square.Column].BackColor = Color.White;
                }
            }
        }

        private void EnableGrids(bool enable)
        {
            EnableGrid(panel1, enable);
            EnableGrid(panel2, enable);
        }

        private void EnableGrid(Panel panel, bool enable)
        {
            foreach (Control control in panel.Controls)
            {
                if (control is Button btn)
                {
                    btn.Enabled = enable;
                }
            }
        }

        private void ResetGame()
        {
            ResetGridButtons(playerButtons);  // Resetira mrežu igrača
            ResetGridButtons(computerButtons); // Resetira mrežu kompjutera
            InitializeFleetBuilders();
            SetFleetOnBoard();

            computerGunnery = new Gunnery(gridSize, gridSize, new int[] { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 });
            playerSunkShipsCount = 0;
            sunkShipsCount = 0;

            // Resetiranje teksta u 'score' labeli
            score.Text = "Start shooting!";
            score.Font = new Font(score.Font.FontFamily, 14f, FontStyle.Bold);
            score.ForeColor = Color.Black;
        }

        private void ResetGridButtons(Button[,] buttons)
        {
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    if (buttons[i, j] != null)
                    {
                        buttons[i, j].BackColor = Color.White;
                        buttons[i, j].Enabled = true;
                    }
                }
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            ResizeGrid(playerButtons, playerTopLabels, playerLeftLabels, panel1); // Resize mreže igrača
            ResizeGrid(computerButtons, computerTopLabels, computerLeftLabels, panel2); // Resize mreže kompjutera
        }

        private void ResizeGrid(Button[,] buttons, Label[] topLabels, Label[] leftLabels, Panel panel)
        {
            int buttonSize = Math.Min(panel.Width / (gridSize + 1), panel.Height / (gridSize + 1));
            for (int i = 0; i < gridSize; i++)
            {
                if (topLabels[i] != null)
                {
                    topLabels[i].Size = new Size(buttonSize, buttonSize);
                    topLabels[i].Location = new Point((i + 1) * buttonSize, 0);
                }
                if (leftLabels[i] != null)
                {
                    leftLabels[i].Size = new Size(buttonSize, buttonSize);
                    leftLabels[i].Location = new Point(0, (i + 1) * buttonSize);
                }
                for (int j = 0; j < gridSize; j++)
                {
                    Button btn = buttons[i, j];
                    if (btn != null)
                    {
                        btn.Size = new Size(buttonSize, buttonSize);
                        btn.Location = new Point((j + 1) * buttonSize, (i + 1) * buttonSize);
                    }
                }
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            //Ovdje ide kod za klik na label3 
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
