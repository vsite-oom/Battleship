using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vsite.Oom.Battleship.Model;

namespace FinalGUI
{
    enum Shooter
    {
        You,
        Computer
    }

    public class Game
    {
        public class MessageArgs : EventArgs
        {
            public string Message { get; set; }
        }

        public class SquareShotArgs : EventArgs
        {
            public Square SquareShot { get; set; }
        }

        public Game(GameForm form, Fleet playersFleet, Fleet computersFleet, PlayersGrid playersGrid, ComputersGrid computersGrid)
        {
            FirstShooterChosen += form.OnFirstShooterChosen;
            ChangeOfTurn += form.OnChangeOfTurn;
            ComputerShot += playersGrid.OnComputerShoots;
            computersGrid.ClickedSquare += OnClickedSquare;
            gunner = new Gunner(form.rows, form.columns, form.shipLengths);
            shooter = WhoShootsFirst();
            this.playersFleet = playersFleet;
            this.computersFleet = computersFleet;
            this.playersGrid = playersGrid;
            this.computersGrid = computersGrid;
            playersShipsLeft = computersShipsLeft = RulesSingleton.Instance.ShipLengths.Count();
        }

        private Shooter WhoShootsFirst()
        {
            Random random = new Random();
            int i = random.Next(0, 2);
            Shooter shooter = (Shooter)i;
            OnFirstShooterChosen(shooter.ToString());
            return shooter;
        }

        public void StartShooting()
        {
            if (shooter == Shooter.You)
            {
                OnChangeOfTurn("It's Your turn to play!");
            }
            else
            {
                OnChangeOfTurn("Computer shoots...");
                ComputerShoots();
                
            }
        }

        public event EventHandler<MessageArgs> FirstShooterChosen;
        public event EventHandler<MessageArgs> ChangeOfTurn;
        protected virtual void OnFirstShooterChosen(string message)
        {
            FirstShooterChosen?.Invoke(this, new MessageArgs() { Message = message });
        }

        protected virtual void OnChangeOfTurn(string message)
        {
            ChangeOfTurn?.Invoke(this, new MessageArgs() { Message = message });
        }

        private Shooter ChangeShooter(Shooter shooter)
        {
            switch (shooter)
            {
                case Shooter.Computer:
                    shooter = Shooter.You;
                    break;
                case Shooter.You:
                    shooter = Shooter.Computer;
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }
            return shooter;
        }

      
        private void ComputerShoots()
        {
            Square square = gunner.NextTarget();
            HitResult result = playersFleet.Hit(square);
            gunner.ProcessHitResult(result);
            OnComputerShoots(square);
            if (result == HitResult.Sunken)
                --playersShipsLeft;
            shooter = ChangeShooter(shooter);
            StartShooting();
        }

        public event EventHandler<SquareShotArgs> ComputerShot;
        protected virtual void OnComputerShoots(Square square)
        {
            ComputerShot?.Invoke(this, new SquareShotArgs() { SquareShot = square });
        }

        private void OnClickedSquare(object sender, ComputersGrid.ClickedSquareArgs args)
        {
            HitResult result = computersFleet.Hit(args.ClickedSquare);
            if (result == HitResult.Sunken)
                --computersShipsLeft;
            if(sender is ComputersGrid)
                ((ComputersGrid)sender).PlayerClickResult(args.ClickedSquare, result);
            shooter = ChangeShooter(shooter);
            StartShooting();
        }

        private int playersShipsLeft;
        private int computersShipsLeft;
        private readonly Gunner gunner;
        private Shooter shooter;
        private Fleet playersFleet;
        private Fleet computersFleet;
        private PlayersGrid playersGrid;
        private ComputersGrid computersGrid;
    }
}
