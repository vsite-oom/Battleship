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
        private Gunnery opponentGunnery;
        private SquareEliminator squareEliminator;
        private List<Button> playerHitButtons = new List<Button>();
        private List<int> shipsToShoot;
        private int playerHitCounter;
        private int opponentHitCounter;

        private const int gridRow = 10;
        private const int gridColumn = 10;

        public MainForm()
        {
            InitializeComponent();

            var (buttonSize, startLocationX, startLocationY) = CalculateButtonSize();
            InitializeBattleField("Host", panel_Host, buttonSize, startLocationX, startLocationY);
            InitializeBattleField("Enemy", panel_Enemy, buttonSize, startLocationX, startLocationY);
        }

        private (int, int, int) CalculateButtonSize()
        {
            int startLocationX = 5;
            int startLocationY = 5;

            var optimalSquare = panel_Host.Size.Width;
            if (optimalSquare > panel_Host.Size.Height)
            {
                optimalSquare = panel_Host.Size.Height;
            }

            int buttonSize = (int)((optimalSquare - 2 * startLocationX - 18) / gridColumn);
            if (buttonSize < 5)
            {
                buttonSize = 5;
            }

            return (buttonSize, startLocationX, startLocationY);
        }

        private void InitializeBattleField(string fieldName, Panel panel, int buttonSize, int startLocationX, int startLocationY)
        {
            for (var row = 0; row < gridRow; ++row)
            {
                for (var col = 0; col < gridColumn; ++col)
                {
                    panel.Controls.Add(CreateButton(fieldName, row, col, buttonSize, startLocationX + 5 + row * buttonSize + 2, startLocationY + col * buttonSize + 2));
                }
            }
        }

        private Button CreateButton(string fieldName, int row, int column, int size, int position_X, int position_Y)
        {
            var button = new Button
            {
                BackColor = Color.White,
                Font = new Font("Calibri", 8F, FontStyle.Regular, GraphicsUnit.Point),
                ForeColor = Color.ForestGreen,
                Location = new Point(position_X, position_Y),
                Margin = new Padding(2),
                Name = $"button_Battle_{column}_{row}",
                Size = new Size(size, size),
                Tag = (column, row),
                Text = "",
                UseVisualStyleBackColor = false,
                BackgroundImageLayout = ImageLayout.Stretch,
                Enabled = fieldName != "Host"
            };
            button.Click += new EventHandler(GridButton_Click);
            return button;
        }

        private void ResetBattleFields()
        {
            button_StartStop.Tag = 0;
            button_StartStop.Text = "Start";
            button_StartStop.ForeColor = Color.ForestGreen;

            // Clear player fleet, opponent fleet, and other variables
            playerFleet = null;
            opponentFleet = null;
            opponentGunnery = null;
            squareEliminator = null;
            playerHitButtons.Clear();
            shipsToShoot = null;
            playerHitCounter = 0;
            opponentHitCounter = 0;

            // Reset player and opponent grid buttons
            foreach (Button button in panel_Host.Controls.OfType<Button>())
            {

                button.BackColor = Color.White;
                button.Text = "";
                button.Enabled = false; // Disable the host buttons initially
            }

            foreach (Button button in panel_Enemy.Controls.OfType<Button>())
            {

                button.BackColor = Color.White;
                button.Text = "";
                button.Enabled = true; // Enable the enemy buttons
            }

            // Update hit counters
            playerHitsLabel.Text = "Obrambeni bodovi: 0";
            opponentHitsLabel.Text = "napadački bodovi: 0";
        }

        private void button_StartStop_Click(object sender, EventArgs e)
        {
            var buttonTag = Convert.ToInt32(button_StartStop.Tag);
            if (buttonTag == 0)
            {
                button_StartStop.Tag = 1;
                button_StartStop.Text = "Stop";
                button_StartStop.ForeColor = Color.Red;

                shipsToShoot = new List<int>(ShipLengths);
                squareEliminator = new SquareEliminator();

                playerHitCounter = ShipLengths.Sum();
                opponentHitCounter = ShipLengths.Sum();
                UpdateHitCounters();

                try
                {
                    playerFleet = CreateFleet(gridRow, gridColumn, ShipLengths);
                    PlaceShipsOnGrid(playerFleet, panel_Host);

                    opponentFleet = CreateFleet(gridRow, gridColumn, ShipLengths);
                    PlaceShipsOnGrid(opponentFleet, panel_Enemy, hideShips: true);

                    opponentGunnery = new Gunnery(gridRow, gridColumn, ShipLengths);

                    HostIsShooting();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error creating fleet: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ResetBattleFields();
                }
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure that you want to quit the game?", "In the middle of the war...", MessageBoxButtons.YesNo);
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
                    throw new InvalidOperationException($"No available placements for ship length {length}");
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

        private void HostIsShooting()
        {
            panel_Enemy.Enabled = true;

            Refresh();
            System.Threading.Thread.Sleep(100);
            Refresh();
            System.Threading.Thread.Sleep(100);
            Refresh();
            System.Threading.Thread.Sleep(100);
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
                        var button = panel_Enemy.Controls.OfType<Button>().FirstOrDefault(b =>
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
                        var button = panel_Enemy.Controls.OfType<Button>().FirstOrDefault(b =>
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

            EnemyIsShooting();
        }

        private void EnemyIsShooting()
        {
            panel_Enemy.Enabled = false;
            System.Threading.Thread.Sleep(100);
            Refresh();
            System.Threading.Thread.Sleep(100);
            Refresh();
            System.Threading.Thread.Sleep(100);

            Square target;
            try
            {
                target = opponentGunnery.Next();
            }
            catch (InvalidOperationException)
            {
                ResetBattleFields();
                return;
            }

            var hitResult = playerFleet.Hit(target.Row, target.Column);
            opponentGunnery.ProcessHitResult(hitResult);

            var hitButton = panel_Host.Controls.OfType<Button>().FirstOrDefault(b =>
            {
                var position = ((int column, int row))b.Tag;
                return position.row == target.Row && position.column == target.Column;
            });

            if (hitButton == null) return; // Safety check

            switch (hitResult)
            {
                case HitResult.Missed:
                    hitButton.BackColor = Color.Blue;
                    break;

                case HitResult.Hit:
                    hitButton.BackColor = Color.Red;
                    playerHitCounter--;
                    UpdateHitCounters();
                    playerHitButtons.Add(hitButton);
                    break;

                case HitResult.Sunken:
                    hitButton.BackColor = Color.Black;

                    foreach (var bt in playerHitButtons)
                    {
                        bt.BackColor = Color.Black;
                    }

                    playerHitButtons.Clear();
                    playerHitCounter--;
                    UpdateHitCounters();

                    // Eliminisanje okolnih polja
                    var sunkenShip = playerFleet.Ships.First(s => s.Squares.Any(sq => sq.Row == target.Row && sq.Column == target.Column));
                    var toEliminate = squareEliminator.ToEliminate(sunkenShip.Squares, gridRow, gridColumn);
                    foreach (var sq in toEliminate)
                    {
                        var button = panel_Host.Controls.OfType<Button>().FirstOrDefault(b =>
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

            HostIsShooting();
        }

        private void UpdateHitCounters()
        {
            playerHitsLabel.Text = $"obrambeni bodovi: {playerHitCounter}";
            opponentHitsLabel.Text = $"napadački bodovi: {opponentHitCounter}";
        }
    }
}
