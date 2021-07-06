using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vsite.Oom.Battleship.Model;
using System.Threading;

namespace Battleships
{
    public abstract class Player : IPlayer, IRunnable
    {
        public Player(FleetControl guiFleet, Fleet fleet)
        {
            GuiFleet = guiFleet;
            Fleet = fleet;
            ShouldStop = false;
            PlayerState = PlayerState.Idle;
            Thread = new Thread(Run);
            Thread.Start();
        }

        public void Run()
        {
            while (!ShouldStop)
            {
                if (PlayerState != PlayerState.Playing)
                {
                    continue;
                }

                TurnLogic();
                Wait();
            }
        }

        public void Wait()
        {
            lock (this)
            {
                PlayerState = PlayerState.Waiting;
            }
        }

        public void Play()
        {
            lock (this)
            {
                PlayerState = PlayerState.Playing;
            }
        }

        public void Stop()
        {
            ShouldStop = true;
            Thread.Abort();
        }

        protected void InvalidateHit(FleetControl guiFleet, Fleet fleet, HitResult hitResult, Square target)
        {
            if (hitResult == HitResult.Missed)
            {
                guiFleet.Miss(target);
                return;
            }

            if (hitResult == HitResult.Hit)
            {
                guiFleet.Hit(target);
                return;
            }

            foreach (var ships in fleet.Ships)
            {
                int index = Array.FindIndex(ships.Squares.ToArray(), item => item.Equals(target));
                if (index != -1)
                {
                    guiFleet.Hit(target);
                    guiFleet.SunkShip(ships.Squares);
                    DecreaseShipCount();
                    break;
                }
            }
        }

        protected abstract void TurnLogic();
        protected abstract void DecreaseShipCount();


        protected FleetControl GuiFleet;
        protected Fleet Fleet;

        private PlayerState PlayerState { get; set; }
        private bool ShouldStop;
        private Thread Thread;
    }
}
