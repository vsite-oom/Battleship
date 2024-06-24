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
        private List<int> ShipLengths = new List<int> { 5, 4, 4, 3, 3, 2, 2, 2, 2 };
        private Fleet playerFleet;
        private Fleet opponentFleet;
        private Gunnery playerGunnery;
        private Gunnery opponentGunnery;
        private SquareEliminator squareEliminator;
        private List<Button> playerHitButtons = new List<Button>();
        private List<int> shipsToShoot;

        private const int gridRow = 10;
        private const int gridColumn = 10;

        public MainForm()
        {
            InitializeComponent();
            this.MainForm_Resize(this, null);

            var (buttonSize, startLocationX, startLocationY) = this.CalculateButtonSize();
            this.InitializeBattleField("Host", this.panel_Host, buttonSize, startLocationX, startLocationY);
            this.InitializeBattleField("Enemy", this.panel_Enemy, buttonSize, startLocationX, startLocationY);
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
                    panel.Controls.Add(this.CreateButton(fieldName, row, col, buttonSize, startLocationX + 5 + row * buttonSize + 2, startLocationY + col * buttonSize + 2));
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
                Name = "button_Battle_" + column + "_" + row,
                Size = new Size(size, size),
                Tag = new ButtonInfo(column, row),
                Text = "",
                UseVisualStyleBackColor = false,
                BackgroundImageLayout = ImageLayout.Stretch,
                Enabled = fieldName != "Host"
            };
            button.Click += new EventHandler(this.GridButton_Click);
            return button;
        }

        private void ResetBattleFields()
        {
            this.button_StartStop.Tag = 0;
            this.button_StartStop.Text = "Start";
            this.button_StartStop.ForeColor = Color.ForestGreen;

            this.textBox_HostShoot.BackColor = Color.Red;
            foreach (Button button in this.panel_Host.Controls.OfType<Button>())
            {
                var buttonInfo = (ButtonInfo)button.Tag;
                buttonInfo.state = null;
                button.Tag = buttonInfo;
                button.BackColor = Color.White;
                button.Text = "";
            }

            this.textBox_EnemyShoot.BackColor = Color.Red;
            foreach (Button button in this.panel_Enemy.Controls.OfType<Button>())
            {
                var buttonInfo = (ButtonInfo)button.Tag;
                buttonInfo.state = null;
                button.Tag = buttonInfo;
                button.BackColor = Color.White;
                button.Enabled = true;
                button.Text = "";
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            this.label_Host.MinimumSize = new Size((int)((this.Width - 16) * 0.5), this.label_Host.MinimumSize.Height);
            this.label_Enemy.MinimumSize = new Size(this.label_Host.MinimumSize.Width, this.label_Host.MinimumSize.Height);
            this.panel_Host.VerticalScroll.Value = 0;
            this.panel_Host.HorizontalScroll.Value = 0;
            this.panel_Host.Size = new Size((int)((this.Width - 16 - this.panel_Split.Width) * 0.5), this.panel_Host.Height);
            this.panel_Enemy.VerticalScroll.Value = 0;
            this.panel_Enemy.HorizontalScroll.Visible = false;
            this.panel_ShootEnemy.Size = new Size((int)((this.Width - 16 - this.panel_Split.Width) * 0.5), this.panel_ShootEnemy.Height);
            this.Refresh();
        }

        private void panel_Host_SizeChanged(object sender, EventArgs e)
        {
            this.ResizeButtons(this.panel_Host.Controls.OfType<Button>());
        }

        private void panel_Enemy_SizeChanged(object sender, EventArgs e)
        {
            this.ResizeButtons(this.panel_Enemy.Controls.OfType<Button>());
        }

        private void ResizeButtons(IEnumerable<Button> buttons)
        {
            this.Refresh();
            var (buttonSize, startLocationX, startLocationY) = this.CalculateButtonSize();
            foreach (Button button in buttons)
            {
                var buttonLocation = (ButtonInfo)button.Tag;
                button.Location = new Point(startLocationX + buttonLocation.row * buttonSize + 2, startLocationY + buttonLocation.column * buttonSize + 2);
                button.Size = new Size(buttonSize, buttonSize);
            }
        }

        private void button_StartStop_Click(object sender, EventArgs e)
        {
            var buttonTag = Convert.ToInt32(this.button_StartStop.Tag);
            if (buttonTag == 0)
            {
                this.button_StartStop.Tag = 1;
                this.button_StartStop.Text = "Stop";
                this.button_StartStop.ForeColor = Color.Red;

                this.shipsToShoot = new List<int>(ShipLengths);
                this.squareEliminator = new SquareEliminator();
                var fleetBuilder = new FleetBuilder(gridRow, gridColumn, ShipLengths.ToArray());

                try
                {
                    this.playerFleet = CreateFleetWithRetry(fleetBuilder, "Player");
                    PlaceShipsOnGrid(this.playerFleet, this.panel_Host);

                    this.opponentFleet = CreateFleetWithRetry(fleetBuilder, "Opponent");
                    PlaceShipsOnGrid(this.opponentFleet, this.panel_Enemy, hideShips: true);

                    this.playerGunnery = new Gunnery(gridRow, gridColumn, ShipLengths);
                    this.opponentGunnery = new Gunnery(gridRow, gridColumn, ShipLengths);

                    this.HostIsShooting();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error creating fleet: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.ResetBattleFields();
                }
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure that you want to quit the game?", "In the middle of the war...", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.No)
                {
                    return;
                }

                this.ResetBattleFields();
            }
        }

        private Fleet CreateFleetWithRetry(FleetBuilder fleetBuilder, string fleetType, int maxRetries = 3)
        {
            for (int i = 0; i < maxRetries; i++)
            {
                try
                {
                    var fleet = fleetBuilder.CreateFleet();
                    Console.WriteLine($"{fleetType} fleet created successfully on attempt {i + 1}.");
                    return fleet;
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine($"Attempt {i + 1} to create {fleetType} fleet failed: {ex.Message}");

                    // Log details about the failed attempt
                    LogAvailablePlacements(fleetBuilder, fleetType);
                }
            }
            throw new InvalidOperationException($"Failed to create {fleetType} fleet after {maxRetries} attempts.");
        }

        private void LogAvailablePlacements(FleetBuilder fleetBuilder, string fleetType)
        {
            Console.WriteLine($"Logging available placements for {fleetType} fleet debugging...");
            foreach (var length in ShipLengths)
            {
                var candidates = GetAvailablePlacements(fleetBuilder, length);
                Console.WriteLine($"Available placements for ship length {length}: {candidates.Count()}");
                foreach (var placement in candidates)
                {
                    Console.WriteLine($"Placement: {string.Join(", ", placement.Select(sq => $"({sq.Row},{sq.Column})"))}");
                }
            }
        }

        private IEnumerable<IEnumerable<Square>> GetAvailablePlacements(FleetBuilder fleetBuilder, int shipLength)
        {
            // Use reflection to get the private fleetGrid field
            var fleetGridField = typeof(FleetBuilder).GetField("fleetGrid", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var fleetGrid = (FleetGrid)fleetGridField.GetValue(fleetBuilder);

            // Get available placements from the fleetGrid
            return fleetGrid.GetAvailablePlacements(shipLength);
        }

        private void PlaceShipsOnGrid(Fleet fleet, Panel panel, bool hideShips = false)
        {
            foreach (var ship in fleet.Ships)
            {
                foreach (var sq in ship.Squares)
                {
                    var button = panel.Controls.OfType<Button>().FirstOrDefault(b =>
                    {
                        var position = (ButtonInfo)b.Tag;
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
            this.panel_Enemy.Enabled = true;
            this.textBox_EnemyShoot.BackColor = Color.Red;
            this.textBox_HostShoot.BackColor = Color.LightGreen;
            this.Refresh();
            System.Threading.Thread.Sleep(100);
            this.textBox_HostShoot.BackColor = Color.GreenYellow;
            this.Refresh();
            System.Threading.Thread.Sleep(100);
            this.textBox_HostShoot.BackColor = Color.LightGreen;
            this.Refresh();
            System.Threading.Thread.Sleep(100);
        }

        private void GridButton_Click(object sender, EventArgs e)
        {
            var hitButton = (Button)sender;
            hitButton.Enabled = false;

            var hitButtonInfo = (ButtonInfo)hitButton.Tag;
            var lastTarget = new Square(hitButtonInfo.row, hitButtonInfo.column);

            var hitResult = this.opponentFleet.Hit(lastTarget.Row, lastTarget.Column);
            switch (hitResult)
            {
                case HitResult.Missed:
                    hitButton.BackColor = Color.Blue;
                    hitButtonInfo.state = HitResult.Missed;
                    break;

                case HitResult.Hit:
                    hitButton.BackColor = Color.Red;
                    hitButtonInfo.state = HitResult.Hit;
                    break;

                case HitResult.Sunken:
                    hitButton.BackColor = Color.Black;
                    hitButtonInfo.state = HitResult.Sunken;

                    var toEliminate = this.squareEliminator.ToEliminate(this.opponentFleet.Ships.First(s => s.Squares.Any(sq => sq.Row == lastTarget.Row && sq.Column == lastTarget.Column)).Squares, gridRow, gridColumn);
                    foreach (var sq in toEliminate)
                    {
                        var button = this.panel_Enemy.Controls.OfType<Button>().FirstOrDefault(b =>
                        {
                            var position = (ButtonInfo)b.Tag;
                            return position.row == sq.Row && position.column == sq.Column;
                        });

                        if (button != null)
                        {
                            button.BackColor = Color.Gray;
                            button.Enabled = false;
                        }
                    }

                    this.shipsToShoot.Remove(this.opponentFleet.Ships.First(s => s.Squares.Any(sq => sq.Row == lastTarget.Row && sq.Column == lastTarget.Column)).Squares.Count());
                    break;
            }

            hitButton.Tag = hitButtonInfo;

            if (!this.shipsToShoot.Any())
            {
                MessageBox.Show("You won the game!", "WINNER !!!", MessageBoxButtons.OK);
                this.ResetBattleFields();
                return;
            }

            this.EnemyIsShooting();
        }

        private void EnemyIsShooting()
        {
            this.panel_Enemy.Enabled = false;
            this.textBox_HostShoot.BackColor = Color.Red;
            this.textBox_EnemyShoot.BackColor = Color.LightGreen;
            System.Threading.Thread.Sleep(100);
            this.textBox_EnemyShoot.BackColor = Color.GreenYellow;
            this.Refresh();
            System.Threading.Thread.Sleep(100);
            this.textBox_EnemyShoot.BackColor = Color.LightGreen;
            this.Refresh();
            System.Threading.Thread.Sleep(100);

            var target = this.opponentGunnery.Next();
            var hitResult = this.playerFleet.Hit(target.Row, target.Column);

            var hitButton = this.panel_Host.Controls.OfType<Button>().FirstOrDefault(b =>
            {
                var position = (ButtonInfo)b.Tag;
                return position.row == target.Row && position.column == target.Column;
            });

            var buttonInfo = (ButtonInfo)hitButton.Tag;
            switch (hitResult)
            {
                case HitResult.Missed:
                    hitButton.BackColor = Color.Blue;
                    buttonInfo.state = HitResult.Missed;
                    break;

                case HitResult.Hit:
                    hitButton.BackColor = Color.Red;
                    buttonInfo.state = HitResult.Hit;
                    this.playerHitButtons.Add(hitButton);
                    break;

                case HitResult.Sunken:
                    hitButton.BackColor = Color.Black;
                    buttonInfo.state = HitResult.Sunken;

                    foreach (var bt in this.playerHitButtons)
                    {
                        bt.BackColor = Color.Black;
                    }

                    this.playerHitButtons.Clear();
                    break;
            }

            hitButton.Tag = buttonInfo;

            this.opponentGunnery.ProcessHitResult(hitResult);

            if (this.playerFleet.Ships.All(s => s.Squares.All(sq => sq.SquareState == SquareState.Sunken)))
            {
                MessageBox.Show("Sorry, you lost the game!", "LOSER !!!", MessageBoxButtons.OK);
                this.ResetBattleFields();
                return;
            }

            this.HostIsShooting();
        }

        private class ButtonInfo
        {
            public int row;
            public int column;
            public HitResult? state;

            public ButtonInfo(int row, int column)
            {
                this.row = row;
                this.column = column;
                this.state = null;
            }
        }
    }
}
