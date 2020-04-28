using Vsite.Oom.Battleship.Model;
using System;
using System.Text;
using System.Linq;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace FleetArangementGUI
{
    public partial class FleetsGrid : Control
    {
        public FleetsGrid()
        {
            InitializeComponent();
            ResizeRedraw = true;
        }

        public void DefineGrid(int rows, int columns)
        {
            if (this.Columns == columns && this.Rows == rows)
                return;
            this.Rows = rows;
            this.Columns = columns;
            Invalidate();
        }

        public void SetupFleet(Fleet fleet)
        {
            this.fleet = fleet;
            Invalidate();
        }

        public void DrawGrid(PaintEventArgs e)
        {
            DrawVerticalLines(e);
            DrawHorizontalLines(e);
        }

        private int SquareHeight
        {
            get { return (ClientRectangle.Height - 1) / Columns; }
        }

        private int SquareWidth
        {
            get { return (ClientRectangle.Width - 1) / Rows;  }
        }

        private void DrawVerticalLines(PaintEventArgs e)
        {
            int x = 0;
            int y = 0;
            int z = SquareHeight * Rows;
            for(int c = 0; c <= Columns; ++c)
            {
                e.Graphics.DrawLine(drawLines, x, y, x, z);
                x += SquareWidth;
            }
        }

        private void DrawHorizontalLines(PaintEventArgs e)
        {
            int x = 0;
            int y = 0;
            int z = SquareWidth * Columns;
            for(int r = 0; r <= Rows; ++r)
            {
                e.Graphics.DrawLine(drawLines, x, y, z, y);
                y += SquareHeight;
            }
        }

        private void DrawBackground(PaintEventArgs e)
        {
            int height = SquareHeight * Rows + 1;
            int width = SquareWidth * Columns + 1;
            e.Graphics.FillRectangle(SystemBrushes.Window, 0, 0, width, height);
        }

        private void DrawFleet(PaintEventArgs e)
        {
            foreach(Ship s in fleet.Ships)
            {
                foreach (Square square in s.Squares)
                    DrawShipsSquare(e, square);
            }
        }

        private void DrawShipsSquare(PaintEventArgs e, Square square)
        {
            int column = square.Column;
            int c = column * SquareHeight;
            int row = square.Row;
            int r = row * SquareWidth;
            e.Graphics.FillRectangle(shipsColor, r, c, SquareWidth, SquareHeight);
            e.Graphics.DrawRectangle(drawLines, r, c, SquareWidth, SquareHeight);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            DrawGrid(e);
            DrawBackground(e);
            if (fleet != null)
                DrawFleet(e);
            base.OnPaint(e);
        }

        private int Columns = 10;
        private int Rows = 10;
        private Fleet fleet;
        private Brush shipsColor = new SolidBrush(Color.Blue);
        private Pen drawLines = new Pen(SystemColors.ActiveBorder);
    }
}
