using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vsite.Oom.Battleship.Model.BattleshipGUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            int buttonSize = panel1.Height / columns;
            AddHorizontalLabels(buttonSize);
            AddVerticalLabels(buttonSize);
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

        int rows = 10;
        int columns = 10;

    }

}
