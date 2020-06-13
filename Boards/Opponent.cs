using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vsite.Oom.Battleship.Model;

namespace Boards
{

    class Opponent
    {
        public event EventHandler<string> GameOverEvent;
        public event EventHandler<ShootingCompletedEventArgs> ShootingCompleted;
        public Opponent(int rows, int columns, IEnumerable<int> shipLengths)
        {
            Shipwright sw = new Shipwright(rows, columns);
            fleet = sw.CreateFleet(shipLengths);
            gunner = new Gunner(rows, columns, shipLengths);
            this.rows = rows;
            this.columns = columns;
            totalShips = shipLengths.Count();
            shipsLeft = shipLengths.Count();
        }//constructor

        public static Opponent getNewOpponent()
        {
            //creating new opponent
            int rows = 10;
            int columns = 10;
            int[] shipLengths = new int[] { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 };

            return new Opponent(rows, columns, shipLengths);
        }
        public void Play()
        {
            Shoot();
        } //Play pc's turn. Shoot.
        private void Shoot()
        {
            Square square = gunner.NextTarget();
            ShootingCompletedEventArgs args = new ShootingCompletedEventArgs(square.Row, square.Column, HitResult.Missed);
            ShootingCompleted?.Invoke(this, args);


        } //Shoot a target and notify Person's fleetGrid. 
        public void ShootResponse(object sender, ShootingCompletedEventArgs e)
        {
            gunner.ProcessHitResult(e.hitResult);
            if (e.hitResult == HitResult.Sunken)
                --shipsLeft;

        } //recieve shooting resposne from Person's fleetGrid.
        public void ShootIncomingRequest(object sender, EventArgs e)
        {
            ShootingCompletedEventArgs args;

            Button btn = (Button)sender;
            String[] rc = btn.Text.Split(' ');
            int row;
            int.TryParse(rc[2], out row);
            row -= 1;
            char[] x = rc[1].ToCharArray();
            int col = x[0] - 'A';


            HitResult result = ShootOpponentFleet(new Square(row, col));
            if (result == HitResult.Sunken)
            {

                foreach (Ship ship in fleet.Ships)
                {
                    if (ship.Squares.Contains(new Square(row, col)))
                    {
                        foreach (Square sq in ship.Squares)
                        {
                            //invoking changes on evidencegrid
                            args = new ShootingCompletedEventArgs(sq.Row, sq.Column, result);
                            ShootingCompleted?.Invoke(sender, args);
                        }
                    }
                }
               
                if(FleetIsSunken())
                    GameOverEvent?.Invoke(null, "You won!");
            }
            else
            {
                args = new ShootingCompletedEventArgs(row, col, result);
                ShootingCompleted?.Invoke(sender, args);
            }


        }//recieve a shot.
        private HitResult ShootOpponentFleet(Square square)
        {
           
            return fleet.Hit(square);
        }//shoots pc's fleet. (this)
        bool FleetIsSunken()
        {
            int shipsSunken = fleet.Ships.Count(s => s.Squares.All(sq => sq.SquareState == SquareState.Sunken));
            return shipsSunken == totalShips;
        }

        private readonly Fleet fleet;
        private readonly Gunner gunner;
        private readonly int rows;
        private readonly int columns;
        private readonly int totalShips;
        private int shipsLeft;
    }
}