using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vsite.Oom.Battleship.Model;

namespace Vsite.Oom.Battleship.Gui
{
    class PanelComputer : Panel
    {
        public PanelComputer()
        {
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }
        public void InitMembers(ref Fleet f, ref RulesSingleton rules)
        {
            m_rows = rules.Rows + 1;
            m_cols = rules.Columns + 1;
            m_fleet = f;
            gunner = new Gunner(rules.Rows, rules.Columns, rules.ShipLengths);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawGrid(e.Graphics);
            if (!deployed)
                FillShipSquares();
            Invalidate();
        }
        private void DrawGrid(Graphics graphics)
        {
            using (Pen pen = new Pen(Color.Gray))
            {
                m_cell_height = (ClientRectangle.Height - 1) / m_rows;
                m_cell_width = (ClientRectangle.Width - 1) / m_cols;

                for (int i = 0; i <= m_rows; ++i)
                {
                    using (Font f = new Font("Times New Roman", 12, FontStyle.Regular))
                        if (i != 0)
                            graphics.DrawString(Convert.ToChar(64 + i).ToString(), f, Brushes.Black, 0, i * m_cell_height);

                    if (i != 0)
                        graphics.DrawLine(pen, -1 + m_cell_height,
                             i * m_cell_height, ClientRectangle.Width, i * m_cell_height);
                }

                for (int i = 0; i <= m_cols; ++i)
                {
                    using (Font f = new Font("Times New Roman", 12, FontStyle.Regular))
                        if (i != 0)
                            graphics.DrawString((i).ToString(), f, Brushes.Black, i * m_cell_width, 0);

                    if (i != 0)
                        graphics.DrawLine(pen, i * m_cell_width, -1 + m_cell_width,
                            i * m_cell_width, ClientRectangle.Height);
                }
            }
        }
        private void FillShipSquares()
        {
            for (int i = 0; i < 10; ++i)
            {
                for (int j = 0; j < 10; ++j)
                {
                    SquareButton tmpButton = new SquareButton(m_cell_width - 2, m_cell_height - 2, new Square(i, j));
                    tmpButton.Top = i * m_cell_width + m_cell_width + 1;
                    tmpButton.Left = j * m_cell_height + m_cell_height + 1;
                    tmpButton.Width = m_cell_width - 2;
                    tmpButton.Height = m_cell_height - 2;
                    tmpButton.Enabled = false;

                    this.Controls.Add(tmpButton);
                }
            }
            deployed = true;

            foreach (Ship ship in m_fleet.Ships)
            {
                foreach (Square sq in ship.Squares)
                {
                    foreach (SquareButton button in this.Controls)
                    {
                        if (button.GetSquare() == sq)
                            button.BackColor = Color.Green;
                    }
                }
            }
        }
        public void ClearAll()
        {
            Controls.Clear();
        }

        public void GunnerShoot()
        {

            var target = gunner.NextTarget();
            var hResult = m_fleet.Hit(target);
            gunner.ProcessHitResult(hResult);

            foreach (SquareButton button in this.Controls)
            {
                if (target == button.GetSquare())
                    button.GetSquare().SetState(hResult);
            }
            
            foreach (Ship ship in m_fleet.Ships)
            {
                foreach (Square sq in ship.Squares)
                {
                    if (sq.SquareState == SquareState.Sunken)
                        SinkShip(ship);
                }
            }

            //Refresh sve buttone
            RefreshAll();
        }
        private void RefreshAll()
        {
            foreach (SquareButton button in this.Controls)
            {
                switch (button.GetSquare().SquareState)
                {
                    case SquareState.Missed:
                        button.BackColor = Color.Blue;
                        button.Refresh();
                        break;
                    case SquareState.Hit:
                        button.BackColor = Color.Red;
                        button.Refresh();
                        break;
                    case SquareState.Sunken:
                        button.BackColor = Color.DarkRed;
                        button.Refresh();
                        break;
                }
            }
        }

        public void SinkShip(Ship ship)
        {
            foreach (Square sSq in ship.Squares)
            {
                foreach (SquareButton button in this.Controls)
                {
                    if (button.GetSquare() == sSq)
                    {
                        button.GetSquare().SetState(HitResult.Sunken);
                    }
                }
            }
        }

        public bool PlayerShipsLeftAlive()
        {
            foreach(Ship ship in m_fleet.Ships)
            {
                foreach (Square sq in ship.Squares)
                    if (sq.SquareState != SquareState.Sunken)
                        return true;
            }
            return false;
        }

        private static Fleet m_fleet;
        private int m_rows;
        private int m_cols;
        private int m_cell_height;
        private int m_cell_width;
        public bool deployed = false;
        private Gunner gunner;
    }
}
