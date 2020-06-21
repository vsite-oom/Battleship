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
using System.Diagnostics;

namespace FinalGUI
{
    public partial class ComputersGrid : GameGrid
    {
        public class ClickedSquareArgs : EventArgs
        {
            public Square ClickedSquare { get; internal set; }
        }

        public ComputersGrid()
        {
            InitializeComponent();
            for (int r = 0; r < rows; ++r)
            {
                for (int c = 0; c < columns; ++c)
                {
                    buttons[r, c].Click += WhichSquareIsIt;
                }
            }
        }

        private void WhichSquareIsIt(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                Button button = (Button)sender;
                for (int r = 0; r < rows; ++r)
                {
                    for (int c = 0; c < columns; ++c)
                    {
                        if (button == buttons[r, c])
                        {
                            OnClickedSquare(new Square(r, c));
                            return;
                        }

                    }
                }
            }
        }

        public event EventHandler<ClickedSquareArgs> ClickedSquare;

        protected virtual void OnClickedSquare(Square clickedSquare)
        {
            ClickedSquare?.Invoke(this, new ClickedSquareArgs() { ClickedSquare = clickedSquare });
        }

        internal void PlayerClickResult(Square clickedSquare, HitResult result)
        {
            Button button = buttons[clickedSquare.Row, clickedSquare.Column];
            switch (result)
            {
                case HitResult.Missed:
                    button.BackColor = missedColor;
                    button.ForeColor = shipColor;
                    break;
                case HitResult.Hit:
                    button.BackColor = hitColor;
                    button.Text = "Hit";
                    button.ForeColor = missedColor;
                    break;
                case HitResult.Sunken:
                    button.BackColor = sunkenColor;
                    button.Text = "Sunk";
                    button.ForeColor = missedColor;
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }


        }
    }
}

