using System;
using Vsite.Oom.Battleship.Model;
using System.Linq;

namespace Battleships
{
    public class HumanPlayer : Player
    {
        public event EventHandler InvokePCPlayerTurn;
        public event EventHandler EndGameEvent;

        public HumanPlayer(FleetControl guiFleet, Fleet fleet, BlockingQueue<ShipButton> queue)
            : base(guiFleet, fleet)
        {
            GuiFleet = guiFleet;
            Fleet = fleet;
            NotificationQueue = queue;
        }

        protected override void TurnLogic()
        {
            ShipButton button = NotificationQueue.Dequeue();

            var row = button.Row;
            var col = button.Column;
            var target = new Square(row, col);
            var hitResult = Fleet.Hit(target);

            InvalidateHit(GuiFleet, Fleet, hitResult, target);

            if (Fleet.AreAllSunken())
            {
                EndGameEvent(this, EventArgs.Empty);
                return;
            }

            InvokePCPlayerTurn(this, EventArgs.Empty);
        }

        protected override void DecreaseShipCount()
        {
            GuiFleet.InvalidateShipCount();
        }

        private BlockingQueue<ShipButton> NotificationQueue;
    }
}
