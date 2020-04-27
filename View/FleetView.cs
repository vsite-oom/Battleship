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
            var rows = RowsTextBox.Text;
            var columns = ColumnsTextBox.Text;

            if (!string.IsNullOrEmpty(rows) && !string.IsNullOrEmpty(columns))
            {
                if (int.TryParse(columns, out var columnValue) && int.TryParse(rows, out var rowsValue))
                {
                    List<int> shipLenghts = null;
                    if (shipLenghts3.Checked)
                    {
                        shipLenghts = new List<int> { 3, 2, 2, 1, 1, 1 };
                    }
                    else if (shipLenghts4.Checked)
                    {
                        shipLenghts = new List<int> { 4, 3, 3, 2, 2, 2, 1, 1, 1, 1 };
                    }
                    else if (shipLenghts5.Checked)
                    {
                        shipLenghts = new List<int> { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2, 1, 1, 1, 1, 1 };

                    }
                    else if (shiplenghts6.Checked)
                    {
                        shipLenghts = new List<int> { 6, 5, 5, 4, 4, 4, 3, 3, 3, 3, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1 };
                    }
                    if (shipLenghts != null)
                    {
                        var shipwright = new Shipwright(rowsValue, columnValue);

                        var fleet = shipwright.CreateFleet(shipLenghts);

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

                            InitializeFleetView(rowsValue, columnValue, fleet);
                        }
                        else
                        {
                            NoPlacementsAlert();
                        }
                    }
                    else
                    {
                        SelectShipLenghtsAlert();
                    }
                }
                else
                {
                    InvalidInputAlert();
                }
            }
            else
            {
                InvalidInputAlert();
            }
        }

        private void InitializeFleetView(int rowsCount, int columnsCount, Fleet fleet)
        {
            TableLayoutPanel gridLayoutPanel = new TableLayoutPanel();
            gridLayoutPanel.AutoSize = true;
            gridLayoutPanel.Location = new Point(30, 160);
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
                            Size = new Size(30, 30),
                            Text = string.Empty,
                            Margin = new Padding(0)
                        };
                        gridLayoutPanel.Controls.Add(label, c, r);
                    }
                    else if (r == 0)
                    {
                        var label = new Label
                        {
                            Size = new Size(30, 30),
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
                            Size = new Size(30, 30),
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
                            Size = new Size(30, 30),
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

        private void InvalidInputAlert()
        {
            string message = "Plese enter valid number for rows and columns.";
            string caption = "Invalid input";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            MessageBox.Show(message, caption, buttons);
        }

        private void NoPlacementsAlert()
        {
            string message = "No possible placements for this input.";
            string caption = "No placements";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            MessageBox.Show(message, caption, buttons);
        }

        private void SelectShipLenghtsAlert()
        {
            string message = "Select ship lenghts to get placements.";
            string caption = "Select ship lenghts";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            MessageBox.Show(message, caption, buttons);
        }
    }
}
