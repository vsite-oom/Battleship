using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vsite.Oom.Battleship.Model;

namespace FinalGUI
{
    public partial class GameGrid : UserControl
    {
        public GameGrid()
        {
            InitializeComponent();
            int buttonSize = gridPanel.Width / columns;
            AddHorizontalLabels(buttonSize);
            AddVerticalLabels(buttonSize);
            AddButtons(buttonSize);
        }

        void AddHorizontalLabels(int buttonSize)
        {
            int left = gridPanel.Left;
            for (int c = 0; c < columns; ++c)
            {
                Label label = new Label
                {
                    Top = gridPanel.Top - buttonSize,
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
            int top = gridPanel.Top;
            for (int r = 0; r < rows; ++r)
            {
                Label label = new Label
                {
                    Top = top,
                    Left = gridPanel.Left - buttonSize,
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
            int top = gridPanel.Top - buttonSize;
            for (int r = 0; r < rows; ++r)
            {
                int left = gridPanel.Left - buttonSize;
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
                    gridPanel.Controls.Add(button);
                    buttons[r, c] = button;
                    left += buttonSize;
                }
                top += buttonSize;
            }
        }

        readonly int rows = RulesSingleton.Instance.Rows;
        readonly int columns = RulesSingleton.Instance.Columns;
        protected Button[,] buttons;
        Color buttonColor = SystemColors.ControlLight;
    }
}
