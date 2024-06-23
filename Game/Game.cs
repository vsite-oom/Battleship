using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        Gunnery playerGunnery;
        Gunnery opponentGunnery;

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
                // Create player fleet on a fresh grid
                var playerFleetGrid = new FleetGrid(10, 10);
                playerFleet = CreateFleet(playerFleetGrid);

                // Create opponent fleet on a fresh grid
                var opponentFleetGrid = new FleetGrid(10, 10);
                opponentFleet = CreateFleet(opponentFleetGrid);

                playerGunnery = new Gunnery(10, 10, new[] { 5, 4, 4, 3, 3, 2, 2, 2, 2 });
                opponentGunnery = new Gunnery(10, 10, new[] { 5, 4, 4, 3, 3, 2, 2, 2, 2 });

                RenderFleet(panel_Host, playerFleet);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        Fleet CreateFleet(FleetGrid fleetGrid)
        {
            var fleet = new Fleet();
            var shipLengths = new[] { 5, 4, 4, 3, 3, 2, 2, 2, 2 };
            var random = new Random();
            var eliminator = new SquareEliminator();

            for (int i = 0; i < shipLengths.Length; ++i)
            {
                var candidates = fleetGrid.GetAvailablePlacements(shipLengths[i]).ToList();
                Debug.WriteLine($"Ship length: {shipLengths[i]}, Candidates count: {candidates.Count}");

                if (candidates.Count == 0)
                {
                    throw new InvalidOperationException($"No available placements for ship of length {shipLengths[i]}");
                }

                var selectedIndex = random.Next(candidates.Count);
                Debug.Assert(selectedIndex >= 0 && selectedIndex < candidates.Count, "Selected index out of range");
                var selected = candidates[selectedIndex];
                Debug.WriteLine($"Selected index: {selectedIndex}, Selected candidate start: Row={selected.First().Row}, Column={selected.First().Column}");

                fleet.CreateShip(selected);

                var toEliminate = eliminator.ToEliminate(selected, fleetGrid.Rows, fleetGrid.Columns);
                Debug.WriteLine($"To eliminate count: {toEliminate.Count()}");
                foreach (var coordinate in toEliminate)
                {
                    if (coordinate.Row >= 0 && coordinate.Row < fleetGrid.Rows && coordinate.Column >= 0 && coordinate.Column < fleetGrid.Columns)
                    {
                        fleetGrid.EliminateSquare(coordinate.Row, coordinate.Column);
                    }
                    else
                    {
                        Debug.WriteLine($"Invalid elimination coordinate: Row={coordinate.Row}, Column={coordinate.Column}");
                    }
                }
            }

            return fleet;
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
