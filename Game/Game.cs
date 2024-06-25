using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Vsite.Oom.Battleship.Model;

namespace Vsite.Oom.Battleship.Game
{
    public partial class MainForm : Form
    {
        private List<int> ShipLengths = new List<int> { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 };
        private Fleet playerFleet;
        private Fleet opponentFleet;
        private Gunnery shipGunnery;
        private SquareEliminator squareEliminator;
        private List<Button> hitSquares = new List<Button>();
        private List<int> shipsToShoot;
        private int playerHitCounter;
        private int opponentHitCounter;

        private const int gridRow = 10;
        private const int gridColumn = 10;
        private readonly char[] columns = "ABCDEFGHIJ".ToCharArray();

        public MainForm()
        {
            InitializeComponent();

            var (squareSize, startLocationX, startLocationY) = CalculateSquareSize();
            InitializeBattleField("Igrač", panel_Player, squareSize, startLocationX, startLocationY, Color.Blue);
            InitializeBattleField("Protivnik", panel_Opponent, squareSize, startLocationX, startLocationY, Color.Red);
        }

        private (int, int, int) CalculateSquareSize()
        {
            int startLocationX = 27; 
            int startLocationY = 27; 

            var optimalSquare = panel_Player.Size.Width;
            if (optimalSquare > panel_Player.Size.Height)
            {
                optimalSquare = panel_Player.Size.Height;
            }

            int squareSize = (int)((optimalSquare - 2 * startLocationX - 18) / gridColumn);
            if (squareSize < 5)
            {
                squareSize = 5;
            }

            return (squareSize, startLocationX, startLocationY);
        }

        private void InitializeBattleField(string fieldName, Panel panel, int squareSize, int startLocationX, int startLocationY, Color backgroundColor)
        {
            panel.BackColor = backgroundColor;

            // Add column labels
            for (var col = 0; col < gridColumn; ++col)
            {
                var label = new Label
                {
                    Text = columns[col].ToString(),
                    Font = new Font("Tahoma", 8, FontStyle.Bold),
                    Size = new Size(squareSize, squareSize),
                    Location = new Point(startLocationX + 5 + col * squareSize + 2, startLocationY - squareSize),
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = Color.Black, 
                    BackColor = backgroundColor 
                };
                panel.Controls.Add(label);
            }

            // Add row labels and buttons
            for (var row = 0; row < gridRow; ++row)
            {
                var label = new Label
                {
                    Text = (row + 1).ToString(),
                    Font = new Font("Tahoma", 8, FontStyle.Bold),
                    Size = new Size(squareSize, squareSize),
                    Location = new Point(startLocationX - squareSize, startLocationY + 5 + row * squareSize + 2),
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = Color.Black,
                    BackColor = backgroundColor
                };
                panel.Controls.Add(label);

                for (var col = 0; col < gridColumn; ++col)
                {
                    panel.Controls.Add(InitializeSquareButton(fieldName, row, col, squareSize, startLocationX + 5 + col * squareSize + 2, startLocationY + row * squareSize + 2));
                }
            }
        }

        private Button InitializeSquareButton(string fieldName, int row, int column, int size, int position_X, int position_Y)
        {
            var button = new Button
            {
                BackColor = Color.White,
                ForeColor = Color.ForestGreen,
                Location = new Point(position_X, position_Y),
                Margin = new Padding(2),
                Name = $"button_Battle_{column}_{row}",
                Size = new Size(size, size),
                Tag = (column, row),
                Text = "",
                UseVisualStyleBackColor = false,
                BackgroundImageLayout = ImageLayout.Stretch,
                Enabled = fieldName != "Igrač"
            };
            button.Click += new EventHandler(GridButton_Click);
            return button;
        }

        private void ResetBattleFields()
        {
            button_StartStop.Tag = 0;
            button_StartStop.Text = "IGRA";
            button_StartStop.ForeColor = Color.ForestGreen;

            playerFleet = null;
            opponentFleet = null;
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

            playerHitsLabel.Text = "obrambeni bodovi: ";
            opponentHitsLabel.Text = "napadački bodovi:";
        }

        private void button_StartStop_Click(object sender, EventArgs e)
        {
            var buttonTag = Convert.ToInt32(button_StartStop.Tag);
            if (buttonTag == 0)
            {
                button_StartStop.Tag = 1;
                button_StartStop.Text = "Predaj";
                button_StartStop.ForeColor = Color.Red;

                shipsToShoot = new List<int>(ShipLengths);
                squareEliminator = new SquareEliminator();

                playerHitCounter = ShipLengths.Sum();
                opponentHitCounter = ShipLengths.Sum();
                UpdateHitCounters();

                try
                {
                    playerFleet = CreateFleet(gridRow, gridColumn, ShipLengths);
                    PlaceShipsOnGrid(playerFleet, panel_Player);

                    opponentFleet = CreateFleet(gridRow, gridColumn, ShipLengths);
                    PlaceShipsOnGrid(opponentFleet, panel_Opponent, hideShips: true);

                    shipGunnery = new Gunnery(gridRow, gridColumn, ShipLengths);

                    PlayerIsShooting();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Greška prilikom kreiranja flote: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ResetBattleFields();
                }
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Da li želite predati igru?", "Prekid igre", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.No)
                {
                    return;
                }

                ResetBattleFields();
            }
        }

        private Fleet CreateFleet(int gridRows, int gridColumns, List<int> shipLengths)
        {
            var fleet = new Fleet();
            var fleetGrid = new FleetGrid(gridRows, gridColumns);
            var eliminator = new SquareEliminator();
            var random = new Random();

            foreach (var length in shipLengths)
            {
                var candidates = fleetGrid.GetAvailablePlacements(length);
                if (!candidates.Any())
                {
                    throw new InvalidOperationException($"Nema slobodnih polja za smještaj brodova {length}");
                }
                var selectedIndex = random.Next(candidates.Count());
                var selected = candidates.ElementAt(selectedIndex);

                fleet.CreateShip(selected);

                var toEliminate = eliminator.ToEliminate(selected, fleetGrid.Rows, fleetGrid.Columns);
                foreach (var coordinate in toEliminate)
                {
                    fleetGrid.EliminateSquare(coordinate.Row, coordinate.Column);
                }
            }
            return fleet;
        }

        private void PlaceShipsOnGrid(Fleet fleet, Panel panel, bool hideShips = false)
        {
            foreach (var ship in fleet.Ships)
            {
                foreach (var sq in ship.Squares)
                {
                    var button = panel.Controls.OfType<Button>().FirstOrDefault(b =>
                    {
                        var position = ((int column, int row))b.Tag;
                        return position.row == sq.Row && position.column == sq.Column;
                    });
                    if (button != null)
                    {
                        button.BackColor = hideShips ? Color.White : Color.Green;
                    }
                }
            }
        }

        private void PlayerIsShooting()
        {
            panel_Opponent.Enabled = true;
        }

        private void GridButton_Click(object sender, EventArgs e)
        {
            var hitButton = (Button)sender;
            hitButton.Enabled = false;

            var (column, row) = ((int column, int row))hitButton.Tag;
            var lastTarget = new Square(row, column);

            var hitResult = opponentFleet.Hit(lastTarget.Row, lastTarget.Column);
            switch (hitResult)
            {
                case HitResult.Missed:
                    hitButton.BackColor = Color.Blue;
                    break;

                case HitResult.Hit:
                    hitButton.BackColor = Color.Red;
                    opponentHitCounter--;
                    UpdateHitCounters();
                    break;

                case HitResult.Sunken:
                    hitButton.BackColor = Color.Black;
                    var sunkenShip = opponentFleet.Ships.First(s => s.Squares.Any(sq => sq.Row == lastTarget.Row && sq.Column == lastTarget.Column));
                    foreach (var sq in sunkenShip.Squares)
                    {
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

                    shipsToShoot.Remove(sunkenShip.Squares.Count());
                    opponentHitCounter--;
                    UpdateHitCounters();
                    break;
            }

            if (opponentHitCounter <= 0)
            {
                MessageBox.Show("Igra je gotova", "Pobjeda", MessageBoxButtons.OK);
                ResetBattleFields();
                return;
            }

            OpponentIsShooting();
        }

        private void OpponentIsShooting()
        {
            panel_Opponent.Enabled = false;

            System.Threading.Thread.Sleep(100);
            Refresh();
            System.Threading.Thread.Sleep(100);
            Refresh();

            Square target;
            try
            {
                target = shipGunnery.Next();
            }
            catch (InvalidOperationException)
            {
                ResetBattleFields();
                return;
            }

            var hitResult = playerFleet.Hit(target.Row, target.Column);
            shipGunnery.ProcessHitResult(hitResult);

            var hitButton = panel_Player.Controls.OfType<Button>().FirstOrDefault(b =>
            {
                var position = ((int column, int row))b.Tag;
                return position.row == target.Row && position.column == target.Column;
            });

            if (hitButton == null) return;

            switch (hitResult)
            {
                case HitResult.Missed:
                    hitButton.BackColor = Color.Blue;
                    break;

                case HitResult.Hit:
                    hitButton.BackColor = Color.Red;
                    playerHitCounter--;
                    UpdateHitCounters();
                    hitSquares.Add(hitButton);
                    break;

                case HitResult.Sunken:
                    hitButton.BackColor = Color.Black;

                    foreach (var bt in hitSquares)
                    {
                        bt.BackColor = Color.Black;
                    }

                    hitSquares.Clear();
                    playerHitCounter--;
                    UpdateHitCounters();

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

            if (playerHitCounter <= 0)
            {
                MessageBox.Show("Igra je gotova", "Poraz", MessageBoxButtons.OK);
                ResetBattleFields();
                return;
            }

            PlayerIsShooting();
        }

        private void UpdateHitCounters()
        {
            playerHitsLabel.Text = $"obrambeni bodovi: {playerHitCounter}";
            opponentHitsLabel.Text = $"napadački bodovi: {opponentHitCounter}";
        }
    }
}