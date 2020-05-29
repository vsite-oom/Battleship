using System;
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

        private void CreateFleet_Click(object sender, EventArgs e)
        {
            var shipwright = new Shipwright();
            var fleet = shipwright.CreateFleet();

            if (fleet != null && fleet.Ships.Any())
            {
                ShowFleet(PlayerPanel, Color.MidnightBlue, fleet);
            }
            else
            {
                NoPlacementsAlert();
            }
        }

        private void InitializePanels()
        {
            PlayerPanel = InitializePanel(rowsCount, columnsCount, new Point(30, 50), panelNamePlayer);
            ComputerPanel = InitializePanel(rowsCount, columnsCount, new Point(600, 50), panelNameComputer);
        }

        private TableLayoutPanel InitializePanel(int rowsCount, int columnsCount, Point location, string panelName)
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
                        var btn = new Button
                        {
                            Size = new Size(40, 40),
                            Name = $"btn_{c}_{r}"
                        };
                        panel.Controls.Add(btn, c, r);
                        btn.TabStop = false;
                        btn.FlatStyle = FlatStyle.Flat;
                        btn.FlatAppearance.BorderSize = 0;
                        btn.Margin = new Padding(0);
                    }
                }
            }

            Controls.Add(panel);
            return panel;
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
       
        private void ShowFleet(TableLayoutPanel panel, Color color, Fleet fleet)
        {
            foreach (var control in panel.Controls)
            {
                if (control.GetType() == typeof(Button))
                {
                    var btn = control as Button;
                    btn.BackColor = Color.Transparent;
                }
            }

            foreach (var ship in fleet.Ships)
            {
                foreach (var square in ship.Squares)
                {
                    var btn = (Button)panel.GetControlFromPosition(square.Column + 1, square.Row + 1);
                    btn.BackColor = color;
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
    }
}
