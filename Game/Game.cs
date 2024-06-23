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
        Fleet playerFleet;
        Fleet opponentFleet;
        FleetBuilder fleetBuilder;
        Gunnery playerGunnery;
        Gunnery opponentGunnery;
        private List<Button> buttonsHostHit = new List<Button>();
        private List<int> shipsToShoot;

        public MainForm()
        {
            InitializeComponent();
            InitializeGrids();
        }

        void InitializeGrids()
        {
            InitializeGrid(panel_Host);
            InitializeGrid(panel_Enemy);
        }

        void InitializeGrid(Panel grid)
        {
            grid.Controls.Clear();
            int buttonSize = grid.Width / 10;
            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    Button btn = new Button
                    {
                        Size = new Size(buttonSize, buttonSize),
                        Location = new Point(col * buttonSize, row * buttonSize),
                        Tag = new ButtonInfo(row, col)
                    };
                    btn.Click += GridButton_Click;
                    grid.Controls.Add(btn);
                }
            }
        }

        void button_StartStop_Click(object sender, EventArgs e)
        {
            try
            {
                fleetBuilder = new FleetBuilder(10, 10, new[] { 5, 4, 4, 3, 3, 2, 2, 2, 2 });

                // Check if all ships can be placed before creating fleets
                var fleetGrid = fleetBuilder.GetType()
                    .GetField("fleetGrid", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                    ?.GetValue(fleetBuilder) as FleetGrid;

                if (fleetGrid == null)
                {
                    MessageBox.Show("Unable to initialize the fleet grid.");
                    return;
                }

                foreach (var length in new[] { 5, 4, 4, 3, 3, 2, 2, 2, 2 })
                {
                    var candidates = fleetGrid.GetAvailablePlacements(length).ToList();
                    if (!candidates.Any())
                    {
                        MessageBox.Show($"Not enough space to place a ship of length {length}. Try again.");
                        return;
                    }
                }

                playerFleet = fleetBuilder.CreateFleet();
                opponentFleet = fleetBuilder.CreateFleet();

                playerGunnery = new Gunnery(10, 10, new[] { 5, 4, 4, 3, 3, 2, 2, 2, 2 });
                opponentGunnery = new Gunnery(10, 10, new[] { 5, 4, 4, 3, 3, 2, 2, 2, 2 });

                RenderFleet(panel_Host, playerFleet);
                statusLabel.Text = "Status: Game started!";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        void GridButton_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null) return;

            var info = btn.Tag as ButtonInfo;
            if (info == null) return;

            int row = info.Row;
            int col = info.Col;

            var result = opponentFleet.Hit(row, col);
            playerGunnery.ProcessHitResult(result);
            UpdateGrids();
            if (result == HitResult.Sunken && opponentFleet.Ships.All(ship => ship.Squares.All(sq => sq.IsHit)))
            {
                MessageBox.Show("You won!");
                statusLabel.Text = "Status: You won!";
                rezultatLabel.Text = "Result: You are the winner!";
            }
            else
            {
                OpponentMove();
            }
        }

        void RenderFleet(Panel grid, Fleet fleet)
        {
            foreach (var ship in fleet.Ships)
            {
                foreach (var square in ship.Squares)
                {
                    var btn = grid.Controls.OfType<Button>().FirstOrDefault(b =>
                    {
                        var info = b.Tag as ButtonInfo;
                        return info.Row == square.Row && info.Col == square.Column;
                    });

                    if (btn != null)
                    {
                        btn.BackColor = Color.Gray;
                    }
                }
            }
        }

        void OpponentMove()
        {
            var target = opponentGunnery.Next();
            var result = playerFleet.Hit(target.Row, target.Column);
            opponentGunnery.ProcessHitResult(result);
            UpdateGrids();
            if (result == HitResult.Sunken && playerFleet.Ships.All(ship => ship.Squares.All(sq => sq.IsHit)))
            {
                MessageBox.Show("Opponent won!");
                statusLabel.Text = "Status: Opponent won!";
                rezultatLabel.Text = "Result: Opponent is the winner!";
            }
        }

        void UpdateGrids()
        {
            // Update the display of strategic and tactical grids based on the current game state
        }

        void panel_Host_SizeChanged(object sender, EventArgs e)
        {
            InitializeGrid(panel_Host);
        }

        void panel_Enemy_SizeChanged(object sender, EventArgs e)
        {
            InitializeGrid(panel_Enemy);
        }

        void MainForm_Resize(object sender, EventArgs e)
        {
            InitializeGrids();
        }

        private class ButtonInfo
        {
            public int Row { get; }
            public int Col { get; }

            public ButtonInfo(int row, int col)
            {
                Row = row;
                Col = col;
            }
        }
    }
}
