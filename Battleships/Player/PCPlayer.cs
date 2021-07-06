using Vsite.Oom.Battleship.Model;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Battleships
{
    public class PCPlayer : Player
    {
        public event EventHandler InvokeHumanPlayerTurn;
        public event EventHandler EndGameEvent;

        public PCPlayer(FleetControl guiFleet, Fleet fleet)
            : base(guiFleet, fleet)
        {
            GuiFleet = guiFleet;
            Fleet = fleet;
            Gunnery = new Gunnery(10, 10, new List<int> { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 });
        }

        protected override void TurnLogic()
        {
            var target = Gunnery.NextTarget();
            var hitResult = Fleet.Hit(target);
            Gunnery.RecordShootingResult(hitResult);

            InvalidateHit(GuiFleet, Fleet, hitResult, target);

            if (Fleet.AreAllSunken())
            {
                EndGameEvent(this, EventArgs.Empty);
                return;
            }

            InvokeHumanPlayerTurn(this, EventArgs.Empty);
        }

        protected override void DecreaseShipCount()
        {
            GuiFleet.InvalidateShipCount();
        }

        Gunnery Gunnery;
    }
}
