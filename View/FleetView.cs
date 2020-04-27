using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Vsite.Oom.Battleship.Model.View
{
    public partial class FleetView : Form
    {
        private readonly string layoutPanelName = "GridLayoutPanel";

        public FleetView()
        {
            InitializeComponent();
            AutoSize = true;
        }

        private void CreateFleet_Click(object sender, EventArgs e)
        {

            var shipwright = new Shipwright(10, 10);

            var fleet = shipwright.CreateFleet(new List<int>
            {
                5, 4, 4, 3, 3, 3, 2, 2, 2, 2
            });

            if (fleet != null && fleet.Ships.Any())
            {
                var existingPanels = Controls.Find(layoutPanelName, false);
                if (existingPanels.Any())
                {
                    foreach (var panel in existingPanels)
                    {
                        Controls.Remove(panel);
                    }
                }

                InitializeFleetView(10, 10, fleet);
            }
            else
            {
                NoPlacementsAlert();
            }
        }

        private void InitializeFleetView(int rowsCount, int columnsCount, Fleet fleet)
        {
            TableLayoutPanel gridLayoutPanel = new TableLayoutPanel();
            gridLayoutPanel.AutoSize = true;
            gridLayoutPanel.Location = new Point(30, 50);
            gridLayoutPanel.Name = layoutPanelName;
            gridLayoutPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            gridLayoutPanel.ColumnCount = columnsCount + 1;
            gridLayoutPanel.RowCount = rowsCount + 1;

            var labelTextChar = 'A';
            var labelTextNumber = 1;

            for (int r = 0; r <= rowsCount; ++r)
            {
                for (int c = 0; c <= columnsCount;  ++c)
                {
                    if (r == 0 && c == 0)
                    {
                        var label = new Label
                        {
                            Size = new Size(40, 40),
                            Text = string.Empty,
                            Margin = new Padding(0)
                        };
                        gridLayoutPanel.Controls.Add(label, c, r);
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
                        gridLayoutPanel.Controls.Add(label, c, r);
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
                        gridLayoutPanel.Controls.Add(label, c, r);
                        ++labelTextChar;
                    }
                    else
                    {
                        var btn = new Button
                        {
                            Size = new Size(40, 40),
                            Name = $"btn_{c}_{r}"
                        };
                        gridLayoutPanel.Controls.Add(btn, c, r);
                        btn.TabStop = false;
                        btn.FlatStyle = FlatStyle.Flat;
                        btn.FlatAppearance.BorderSize = 0;
                        btn.Margin = new Padding(0);
                    }
                }
            }

            foreach (var ship in fleet.Ships)
            {
                foreach (var square in ship.Squares)
                {
                    var btn = (Button)gridLayoutPanel.GetControlFromPosition(square.Column + 1, square.Row + 1);
                    btn.BackColor = Color.MidnightBlue;
                }
            }

            Controls.Add(gridLayoutPanel);
        }

        private void NoPlacementsAlert()
        {
            string message = "No possible placements for this input.";
            string caption = "No placements";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            MessageBox.Show(message, caption, buttons);
        }
    }
}
