using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Drawing;
using Vsite.Oom.Battleship.Model;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Forms.VisualStyles;

namespace Vsite.Oom.Battleship.Gui
{
    class PanelPlayer : Panel
    {
        public PanelPlayer()
        {
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            BackColor = Color.FromArgb(255, 15, 122, 189);
            BorderStyle = BorderStyle.Fixed3D;
            
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawRectangle(
                new Pen( 
                    new SolidBrush(Color.Red), 2), e.ClipRectangle);
            DrawGrid(e.Graphics);
            if (!deployed)
                FillShipSquares();
            if (winner == 1)
            {
                lbl.Text = "You won";
                winner = 0;
            }
            else if (winner == 2)
            {
                lbl.Text = "You lost";
                winner = 0;
            }
            Invalidate();
        }

        public void InitMembers(ref Fleet f, ref RulesSingleton rules, ref PanelComputer pc, ref Label lbl)
        {
            m_rows = rules.Rows + 1;
            m_cols = rules.Columns + 1;
            m_fleet = f;
            this.pc = pc;
            this.lbl = lbl;
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
                    SquareButton tmpButton = new SquareButton(m_cell_width - 2, m_cell_height - 2, new Square(i, j))
                    {
                        Top = i * m_cell_width + m_cell_width + 1,
                        Left = j * m_cell_height + m_cell_height + 1,
                        Width = m_cell_width - 2,
                        Height = m_cell_height - 2
                    };

                    tmpButton.MouseDown += Button_Click;
                    this.Controls.Add(tmpButton);
                }
            }
            deployed = true;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            SquareButton targetButton = (SquareButton)sender;
            Square square = targetButton.GetSquare();

            //Process HitREsult            
            //Change SquareState in button's Square
            square.SetState(ProcessHitResult(square));

            //Refresh all buttons and set apropriate color
            RefreshAllButtons();

            //If none left alive, player wins
            CoputerShipsLeftALive();

            //Computer shoots
            pc.GunnerShoot();

            //If none left alive, computer wins
            PlayerShipsLeftAlive();
        }

        private HitResult ProcessHitResult(Square square)
        {
            HitResult hr = m_fleet.Hit(square);

            //Check if ship is sunk
            foreach (Ship ship in m_fleet.Ships)
            {
                foreach (Square sq in ship.Squares)
                {
                    if (sq.SquareState == SquareState.Sunken)
                        SinkShip(ship);
                }
            }
            return hr;
        }

        private void RefreshAllButtons()
        {
            foreach (SquareButton button in this.Controls)
            {
                switch (button.GetSquare().SquareState)
                {
                    case SquareState.Missed:
                        button.BackColor = Color.Blue;
                        button.Enabled = false;
                        break;
                    case SquareState.Hit:
                        button.BackColor = Color.Red;
                        button.Enabled = false;
                        break;
                    case SquareState.Sunken:
                        button.BackColor = Color.DarkRed;
                        button.Enabled = false;
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
                        sSq.SetState(HitResult.Sunken);
                    }
                } 
            }
        }

        private void PlayerShipsLeftAlive()
        {
            bool flag = pc.PlayerShipsLeftAlive();
            if (!flag)
            {
                foreach (SquareButton button in this.Controls)
                    button.Enabled = false;
                ClearAll();
                winner = 2;
            }
        }

        private void CoputerShipsLeftALive()
        {
            foreach(Ship ship in m_fleet.Ships)
            {
                foreach(Square sq in ship.Squares)
                {
                    if (sq.SquareState != SquareState.Sunken)
                        return;
                }
            }
            winner = 1;
            LockButtons();
        }

        void LockButtons()
        {
            foreach (SquareButton button in this.Controls)
                button.Enabled = false;
            ClearAll();
        }

        public void ClearAll()
        {
            Controls.Clear();
        }
        private static Fleet m_fleet;
        private int m_rows;
        private int m_cols;
        private int m_cell_height;
        private int m_cell_width;
        public bool deployed = false;
        private PanelComputer pc;
        private int winner = 0;
        private Label lbl;
    }
}
