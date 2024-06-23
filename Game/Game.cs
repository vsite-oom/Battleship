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
        Fleet enemyFleet;
        Gunnery playerGunnery;
        Gunnery enemyGunnery;

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
                var shipLengths = new[] { 5, 4, 4, 3, 3, 2, 2, 2, 2 };

                // Create player fleet
                var playerFleetBuilder = new FleetBuilder(10, 10, shipLengths);
                playerFleet = playerFleetBuilder.CreateFleet();

                // Create enemy fleet
                var enemyFleetBuilder = new FleetBuilder(10, 10, shipLengths);
                enemyFleet = enemyFleetBuilder.CreateFleet();

                playerGunnery = new Gunnery(10, 10, shipLengths);
                enemyGunnery = new Gunnery(10, 10, shipLengths);

                RenderFleet(panel_Host, playerFleet);
                EnableEnemyGrid(true);
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

            if (btn.Parent == panel_Enemy)
            {
                ProcessPlayerShot(btn, info);
            }
        }

        void ProcessPlayerShot(Button btn, ButtonInfo info)
        {
            int row = info.Row;
            int col = info.Column;

            var result = enemyFleet.Hit(row, col);
            playerGunnery.ProcessHitResult(result);

            switch (result)
            {
                case HitResult.Missed:
                    btn.BackColor = Color.Blue;
                    break;
                case HitResult.Hit:
                    btn.BackColor = Color.Red;
                    break;
                case HitResult.Sunken:
                    btn.BackColor = Color.Black;
                    MarkSurroundingSquaresAsEliminated(enemyFleet.Ships.First(ship => ship.Squares.All(sq => sq.IsHit)).Squares);
                    break;
            }

            btn.Enabled = false;

            if (result == HitResult.Sunken && enemyFleet.Ships.All(ship => ship.Squares.All(sq => sq.IsHit)))
            {
                MessageBox.Show("You won!");
                ResetBattleFields();
            }
            else
            {
                OpponentMove();
            }
        }

        void MarkSurroundingSquaresAsEliminated(IEnumerable<Square> sunkenShipSquares)
        {
            var toEliminate = new SquareEliminator().ToEliminate(sunkenShipSquares, 10, 10);
            foreach (var square in toEliminate)
            {
                var btn = panel_Enemy.Controls.OfType<Button>().FirstOrDefault(b =>
                {
                    var info = b.Tag as ButtonInfo;
                    return info.Row == square.Row && info.Column == square.Column;
                });

                if (btn != null && btn.BackColor == Color.White)
                {
                    btn.BackColor = Color.Gray;
                    btn.Enabled = false;
                }
            }
        }

        void RenderFleet(Panel grid, Fleet fleet, bool isEnemy = false)
        {
            foreach (var ship in fleet.Ships)
            {
                foreach (var square in ship.Squares)
                {
                    var btn = grid.Controls.OfType<Button>().FirstOrDefault(b =>
                    {
                        var info = b.Tag as ButtonInfo;
                        return info.Row == square.Row && info.Column == square.Column;
                    });

                    if (btn != null)
                    {
                        if (!isEnemy)
                        {
                            btn.BackColor = Color.Green;
                        }
                    }
                }
            }
        }

        void OpponentMove()
        {
            var target = enemyGunnery.Next();
            var result = playerFleet.Hit(target.Row, target.Column);
            enemyGunnery.ProcessHitResult(result);

            var btn = panel_Host.Controls.OfType<Button>().FirstOrDefault(b =>
            {
                var info = b.Tag as ButtonInfo;
                return info.Row == target.Row && info.Column == target.Column;
            });

            if (btn != null)
            {
                switch (result)
                {
                    case HitResult.Missed:
                        btn.BackColor = Color.Blue;
                        break;
                    case HitResult.Hit:
                        btn.BackColor = Color.Red;
                        break;
                    case HitResult.Sunken:
                        btn.BackColor = Color.Black;
                        break;
                }
            }

            if (result == HitResult.Sunken && playerFleet.Ships.All(ship => ship.Squares.All(sq => sq.IsHit)))
            {
                MessageBox.Show("Opponent won!");
                ResetBattleFields();
            }
            else
            {
                EnableEnemyGrid(true);
            }
        }

        void EnableEnemyGrid(bool enable)
        {
            foreach (var btn in panel_Enemy.Controls.OfType<Button>())
            {
                if (btn.BackColor == Color.White)
                {
                    btn.Enabled = enable;
                }
            }
        }

        void ResetBattleFields()
        {
            button_StartStop.Tag = 0;
            button_StartStop.Text = "Start";
            button_StartStop.ForeColor = Color.ForestGreen;

            foreach (Button button in panel_Host.Controls.OfType<Button>())
            {
                button.BackColor = Color.White;
                button.Enabled = true;
            }

            foreach (Button button in panel_Enemy.Controls.OfType<Button>())
            {
                button.BackColor = Color.White;
                button.Enabled = false;
            }
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
            public int Column { get; }

            public ButtonInfo(int row, int col)
            {
                Row = row;
                Column = col;
            }
        }
    }
}
