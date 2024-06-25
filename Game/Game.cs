using Vsite.Oom.Battleship.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

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

            var (squareSizePlayer, startLocationXPlayer, startLocationYPlayer) = CalculateSquareSize(panel_Player);
            InitializeBattleField("Igrač", panel_Player, squareSizePlayer, startLocationXPlayer, startLocationYPlayer, Color.Blue);

            var (squareSizeOpponent, startLocationXOpponent, startLocationYOpponent) = CalculateSquareSize(panel_Opponent);
            InitializeBattleField("Protivnik", panel_Opponent, squareSizeOpponent, startLocationXOpponent, startLocationYOpponent, Color.Red);
        }

        private (int, int, int) CalculateSquareSize(Panel panel)
        {
            int optimalSquare = panel.Size.Width;
            if (optimalSquare > panel.Size.Height)
            {
                optimalSquare = panel.Size.Height;
            }

            int squareSize = (int)((optimalSquare - 50) / gridColumn);  
            if (squareSize < 5)
            {
                squareSize = 5;
            }

            int totalGridWidth = squareSize * gridColumn;
            int totalGridHeight = squareSize * gridRow;

            int startLocationX = (panel.Size.Width - totalGridWidth) / 2;
            int startLocationY = (panel.Size.Height - totalGridHeight) / 2;

            return (squareSize, startLocationX, startLocationY);
        }

        private void InitializeBattleField(string fieldName, Panel panel, int squareSize, int startLocationX, int startLocationY, Color backgroundColor)
        {
            panel.BackColor = backgroundColor;

            for (var col = 0; col < gridColumn; ++col)
            {
                var label = new Label
                {
                    Text = columns[col].ToString(),
                    Font = new Font("Tahoma", 10, FontStyle.Bold),
                    Size = new Size(squareSize, squareSize),
                    Location = new Point(startLocationX + 5 + col * squareSize + 2, startLocationY - squareSize),
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = Color.Black,
                    BackColor = backgroundColor
                };
                panel.Controls.Add(label);
            }

            for (var row = 0; row < gridRow; ++row)
            {
                var label = new Label
                {
                    Text = (row + 1).ToString(),
                    Font = new Font("Tahoma", 10, FontStyle.Bold),
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
                button_StartStop.Text = "PREDAJ";
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
                DialogResult dialogResult = MessageBox.Show("Predajete igru?", "Prekid igre", MessageBoxButtons.YesNo);
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
            // Provjera je li igra u tijeku
            if (button_StartStop.Tag == null || Convert.ToInt32(button_StartStop.Tag) == 0)
            {
                MessageBox.Show("Morate izabrati novu igru", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var hitSquare = (Button)sender;
            hitSquare.Enabled = false;

            var (column, row) = ((int column, int row))hitSquare.Tag;
            var lastTarget = new Square(row, column);

            var hitResult = opponentFleet.Hit(lastTarget.Row, lastTarget.Column);
            switch (hitResult)
            {
                case HitResult.Missed:
                    hitSquare.BackColor = Color.Blue;
                    break;

                case HitResult.Hit:
                    hitSquare.BackColor = Color.Red;
                    opponentHitCounter--;
                    UpdateHitCounters();
                    break;

                case HitResult.Sunken:
                    hitSquare.BackColor = Color.Black;
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
                MessageBox.Show("POBJEDA", "Igra je gotova", MessageBoxButtons.OK);
                ResetBattleFields();
                button_StartStop.Tag = 0;
                button_StartStop.Text = "IGRA";
                button_StartStop.ForeColor = Color.ForestGreen;
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
                button_StartStop.Tag = 0;
                button_StartStop.Text = "IGRA";
                button_StartStop.ForeColor = Color.ForestGreen;
                return;
            }

            var hitResult = playerFleet.Hit(target.Row, target.Column);
            shipGunnery.ProcessHitResult(hitResult);

            var hitSquare = panel_Player.Controls.OfType<Button>().FirstOrDefault(b =>
            {
                var position = ((int column, int row))b.Tag;
                return position.row == target.Row && position.column == target.Column;
            });

            if (hitSquare == null) return;

            switch (hitResult)
            {
                case HitResult.Missed:
                    hitSquare.BackColor = Color.Blue;
                    break;

                case HitResult.Hit:
                    hitSquare.BackColor = Color.Red;
                    playerHitCounter--;
                    UpdateHitCounters();
                    hitSquares.Add(hitSquare);
                    break;

                case HitResult.Sunken:
                    hitSquare.BackColor = Color.Black;

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
                MessageBox.Show("PORAZ", "Igra je gotova", MessageBoxButtons.OK);
                ResetBattleFields();
                button_StartStop.Tag = 0;
                button_StartStop.Text = "IGRA";
                button_StartStop.ForeColor = Color.ForestGreen;
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
