using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Boards
{
    public partial class GridDisplay : UserControl
    {
        public GridDisplay()
        {
            InitializeComponent();
            int size = CreateButtons();
            AddLabels(size);
        }

        private void AddLabels(int size)
        {
            int y = panelMain.Top;
            for (int r = 0; r < rows; ++r)
            {
                Label label = new Label
                {
                    Top = y,
                    Left = panelMain.Left - size,
                    Width = size,
                    Height = size,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Text = (r + 1).ToString()
                };
                Controls.Add(label);
                y += size;
            }
            int x = panelMain.Left;
            for (int c = 0; c < columns; ++c)
            {
                Label label = new Label
                {
                    

                    Top = panelMain.Top - size,
                    Left = x,
                    Width = size,
                    Height = size,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Text = ((char)(c + 'A')).ToString()
                };
                Controls.Add(label);
                x += size;
            }
        }

        private int CreateButtons()
        {
            buttons = new Button[rows, columns];
            int buttonSize = Math.Min(panelMain.Width / columns, panelMain.Height / rows);

            int x0 = (panelMain.Width - columns * buttonSize) / 2;
            int y = (panelMain.Height - rows * buttonSize) / 2;
            for (int r = 0; r < rows; ++r)
            {
                int x = x0;
                for (int c = 0; c < columns; ++c)
                {
                    Button button = new Button
                    {
                        Top = y,
                        Left = x,
                        Width = buttonSize,
                        Height = buttonSize,
                        BackColor = buttonColor
                    };
                    buttons[r, c] = button;
                    panelMain.Controls.Add(button);
                    x += buttonSize;
                }
                y += buttonSize;
            }
            return buttonSize;
        }

        protected void ResetButtons()
        {
            for (int r = 0; r < rows; ++r)
            {
                for (int c = 0; c < columns; ++c)
                {
                    buttons[r, c].BackColor = buttonColor;
                    
                }
            }
        }

        protected Button[,] buttons;
        protected int rows = 10;
        protected int columns = 10;

        protected static Color buttonColor = SystemColors.ControlLight;
    }
}
