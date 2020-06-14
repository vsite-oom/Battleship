using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vsite.Oom.Battleship.Model.PartialGUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            int buttonSize = panel1.Width / columns;
            AddHorizontalLabels(buttonSize);
            AddVerticalLabels(buttonSize);
            AddButtons(buttonSize);
        }

        void AddHorizontalLabels(int buttonSize)
        {
            int left = panel1.Left;
            for (int c = 0; c < columns; ++c)
            {
                Label label = new Label
                {
                    Top = panel1.Top - buttonSize,
                    Left = left,
                    Width = buttonSize,
                    Height = buttonSize,
                    Text = ((char)(c + 'A')).ToString(),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                Controls.Add(label);
                left += buttonSize;
            }
        }

        void AddVerticalLabels(int buttonSize)
        {
            int top = panel1.Top;
            for (int r = 0; r < rows; ++r)
            {
                Label label = new Label
                {
                    Top = top,
                    Left = panel1.Left - buttonSize,
                    Width = buttonSize,
                    Height = buttonSize,
                    Text = (r + 1).ToString(),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                Controls.Add(label);
                top += buttonSize;
            }
        }

        void AddButtons(int buttonSize)
        {
            buttons = new Button[rows, columns];
            int top = panel1.Top - buttonSize;
            for (int r = 0; r < rows; ++r)
            {
                int left = panel1.Left - buttonSize;
                for (int c = 0; c < columns; ++c)
                {
                    Button button = new Button
                    {
                        Top = top,
                        Left = left,
                        Width = buttonSize,
                        Height = buttonSize,
                        BackColor = buttonColor,
                        Text = "",
                        TextAlign = ContentAlignment.MiddleCenter
                    };
                    panel1.Controls.Add(button);
                    buttons[r, c] = button;
                    left += buttonSize;
                }
                top += buttonSize;
            }
        }

        void FillGrid(Fleet fleet)
        {
            List<Ship> ships = new List<Ship>(fleet.Ships);
            for (int i = 0; i < ships.Count(); ++i)
            {
                foreach (var square in ships[i].Squares)
                {
                    buttons[square.Row, square.Column].BackColor = shipColor;
                }
            }
        }

        void ClearGrid()
        {
            for (int r = 0; r < rows; ++r)
            {
                for (int c = 0; c < columns; ++c)
                {
                    buttons[r, c].BackColor = buttonColor;
                }
            }
        }

        int rows = 10;
        int columns = 10;
        Button[,] buttons;
        Fleet fleet = new Fleet();
        Color shipColor = Color.Red;
        Color buttonColor = SystemColors.ControlLight;

        private void button1_Click(object sender, EventArgs e)
        {
            ClearGrid();
            Shipwright sw = new Shipwright(rows, columns);
            fleet = sw.CreateFleet(new int[] { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 });
            FillGrid(fleet);
        }
    }
}
