using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Vsite.Oom.Battleship.Model.View
{
    public partial class FleetView : Form
    {
        private readonly string panelNamePlayer = "PlayerPanel";
        private readonly string panelNameComputer = "ComputerPanel";
        private readonly int columnsCount = RulesSingleton.Instance.Columns;
        private readonly int rowsCount = RulesSingleton.Instance.Rows;
        private readonly Shipwright shipwright = new Shipwright();

        public FleetView()
        {
            InitializeComponent();
            InitializePanels();
            InitializeComputerFleet();
            AutoSize = true;
        }

        public TableLayoutPanel PlayerPanel { get; set; }
        public TableLayoutPanel ComputerPanel { get; set; }
        public Fleet ComputerFleet { get; set; }
        public Fleet PlayerFleet { get; set; }

        private void CreateFleet_Click(object sender, EventArgs e)
        {
            var shipwright = new Shipwright();
            var fleet = shipwright.CreateFleet();

            if (fleet != null && fleet.Ships.Any())
            {
                ShowPlayerFleet(Color.MidnightBlue, fleet);
                play.Visible = true;
                play.Enabled = true;
            }
            else
            {
                NoPlacementsAlert();
            }
        }

        private void InitializePanels()
        {
            PlayerPanel = InitializePanel(rowsCount, columnsCount, new Point(30, 50), panelNamePlayer, false);
            ComputerPanel = InitializePanel(rowsCount, columnsCount, new Point(600, 50), panelNameComputer, true);
        }

        private TableLayoutPanel InitializePanel(int rowsCount, int columnsCount, Point location, string panelName, bool isComputerPanel)
        {
            TableLayoutPanel panel = new TableLayoutPanel
            {
                AutoSize = true,
                Location = location,
                Name = panelName,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Single,
                ColumnCount = columnsCount + 1,
                RowCount = rowsCount + 1
            };

            var labelTextChar = 'A';
            var labelTextNumber = 1;

            for (int r = 0; r <= rowsCount; ++r)
            {
                for (int c = 0; c <= columnsCount; ++c)
                {
                    if (r == 0 && c == 0)
                    {
                        var label = new Label
                        {
                            Size = new Size(40, 40),
                            Text = string.Empty,
                            Margin = new Padding(0)
                        };
                        panel.Controls.Add(label, c, r);
                    }
                    else if (r == 0)
                    {
                        var label = new Label
                        {
                            Size = new Size(40, 40),
                            Text = labelTextNumber.ToString(),
                            TextAlign = ContentAlignment.MiddleCenter,
                            Margin = new Padding(0)
                        };
                        panel.Controls.Add(label, c, r);
                        ++labelTextNumber;
                    }
                    else if (c == 0)
                    {
                        var label = new Label
                        {
                            Size = new Size(40, 40),
                            Text = labelTextChar.ToString(),
                            TextAlign = ContentAlignment.MiddleCenter,
                            Margin = new Padding(0)
                        };
                        panel.Controls.Add(label, c, r);
                        ++labelTextChar;
                    }
                    else
                    {
                        var btn = new PositionedButton
                        {
                            Size = new Size(40, 40),
                            Name = $"btn_{c}_{r}",
                            Row = r,
                            Column = c,
                        };
                        panel.Controls.Add(btn, c, r);
                        btn.TabStop = false;
                        btn.FlatStyle = FlatStyle.Flat;
                        btn.FlatAppearance.BorderSize = 0;
                        btn.Margin = new Padding(0);
                        if (isComputerPanel)
                        {
                            btn.Click += new EventHandler(PanelButton_Click);
                        }
                    }
                }
            }

            Controls.Add(panel);
            return panel;
        }

        private void PanelButton_Click(object sender, EventArgs e)
        {
            var senderBtn = sender as PositionedButton;
            var square = new Square(senderBtn.Row - 1, senderBtn.Column - 1);
            var hitResult = ComputerFleet.Hit(square);

            switch (hitResult)
            {
                case ShipHitResult.Missed:
                    senderBtn.BackColor = Color.Gray;
                    break;
                case ShipHitResult.Hit:
                    senderBtn.BackColor = Color.Red;
                    break;
                case ShipHitResult.Sunken:
                    foreach (var sunkenSquare in ComputerFleet.Ships.Where(s => s.Squares.Contains(square)).SelectMany(s => s.Squares))
                    {
                        var btn = (Button)ComputerPanel.GetControlFromPosition(sunkenSquare.Column + 1, sunkenSquare.Row + 1);
                        btn.BackColor = Color.DarkRed;
                    }
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }
        }

        private void InitializeComputerFleet()
        {
            var fleet = shipwright.CreateFleet();
            if (fleet != null && fleet.Ships.Any())
            {
                ComputerFleet = fleet;
            }
            else
            {
                NoPlacementsAlert();
            }
        }
       
        private void ShowPlayerFleet(Color color, Fleet fleet)
        {
            ClearFleet(PlayerPanel);

            foreach (var ship in fleet.Ships)
            {
                foreach (var square in ship.Squares)
                {
                    var btn = (Button)PlayerPanel.GetControlFromPosition(square.Column + 1, square.Row + 1);
                    btn.BackColor = color;
                }
            }
        }

        private void ClearFleet(TableLayoutPanel panel)
        {
            foreach (var control in panel.Controls)
            {
                if (control.GetType() == typeof(PositionedButton))
                {
                    var btn = control as Button;
                    btn.BackColor = Color.Transparent;
                }
            }
        }

        private void NoPlacementsAlert()
        {
            string message = "No possible placements.";
            string caption = "No placements";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            MessageBox.Show(message, caption, buttons);
        }

        private void play_Click(object sender, EventArgs e)
        {
            CreateFleet.Enabled = false;
            play.Enabled = false;
            endGame.Visible = true;
        }

        private void endGame_Click(object sender, EventArgs e)
        {
            CreateFleet.Enabled = true;
            play.Visible = false;
            endGame.Visible = false;
            ClearFleet(PlayerPanel);
            ClearFleet(ComputerPanel);
        }
    }
}
