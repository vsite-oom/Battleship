using System;
using System.Drawing;
using System.Windows.Forms;

using Vsite.Oom.Battleship.Model;

namespace Boards
{
    class FleetGrid : GridDisplay
    {
        public event EventHandler<string> GameOverEvent;
        public event EventHandler<ShootingCompletedEventArgs> ShootingResponse;
        public FleetGrid()
        {
            for (int r = 0; r < rows; ++r)
            {
                for (int c = 0; c < columns; ++c)
                {
                    buttons[r, c].Enabled = false;
                }
            }

        }

        public void CreateFleet()
        {
            Shipwright sw = new Shipwright(rows, columns);
            fleet = sw.CreateFleet(new int[] { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 });
            ResetButtons();
            PaintShips();
        }
        public void Play(object sender, ShootingCompletedEventArgs e)
        {
            Shoot(new Square(e.row, e.column));
        }

        private void PaintShips()
        {
            foreach (Ship ship in fleet.Ships)
            {
                foreach (Square square in ship.Squares)
                {
                    var button = buttons[square.Row, square.Column];
                    switch (square.SquareState)
                    {
                        case SquareState.Hit:
                            button.BackColor = hitColor;
                            break;
                        case SquareState.Sunken:
                            button.BackColor = sunkenColor;
                            break;
                        default:
                            button.BackColor = shipColor;
                            break;
                    }
                }
            }
        }

        private void Shoot(Square s)
        {
            ShootingCompletedEventArgs e;

            HitResult result = fleet.Hit(new Square(s.Row, s.Column));
            switch (result)
            {
                case HitResult.Missed:
                    buttons[s.Row, s.Column].BackColor = missedColor;
                    break;
                case HitResult.Hit:
                    buttons[s.Row, s.Column].BackColor = hitColor;
                    break;
                case HitResult.Sunken:
                    PaintShips();

                    if (FleetIsSunken()) {                   
                       GameOverEvent?.Invoke(null,"You lost!");
                       return;
                    }
                    break;
                default:
                    buttons[s.Row, s.Column].BackColor = missedColor;
                    break;
            }

            e = new ShootingCompletedEventArgs(s.Row, s.Column, result);
            ShootingResponse?.Invoke(null, e);
        }

        bool FleetIsSunken()
        {
            foreach (Ship ship in fleet.Ships)
            {

                foreach (Square square in ship.Squares)
                {
                    if (!square.Hit)
                        return false;
                }
            }
            return true;
        }

        private static Color shipColor = Color.Blue;
        private static Color hitColor = Color.Red;
        private static Color sunkenColor = Color.Brown;
        private static Color missedColor = Color.White;

        private Fleet fleet = new Fleet();
    }
}