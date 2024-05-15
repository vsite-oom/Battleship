using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vsite.Oom.Battleship.Model;

namespace vsite.oom.battleship.model
{
    public enum ShootingTactics
    {
        Random,
        Surrounding,
        Inline
    }
    public class Gunnery
    {
        public Gunnery(int rows, int columns, IEnumerable<int> shipLengths)
        {
            recordGrid = new FleetGrid(rows, columns);
            this.shipLengths = new List<int>(shipLengths.OrderByDescending(s => s));
            targetSelector = new RandomTargetSelector(recordGrid, this.shipLengths[0]);
        }
        public Square Next()
        {
            target = targetSelector.Next();
            return target;
        }
        public void ProcessHitResult(HitResult hitResult)
        {
            switch(hitResult)
            {
                case HitResult.Missed:
                    return;
                case HitResult.Hit:
                    switch (ShootingTactics)
                    {
                        case ShootingTactics.Random:
                            ChangeTacticsToSurrounding();
                            return;
                        case ShootingTactics.Surrounding:
                            ChangeTacticsToInline();
                            return;
                        case ShootingTactics.Inline:
                            return;
                        default:

                            return;
                    }
            }
        }
        private void ChangeTacticsToRandom()
        {
            ShootingTactics = ShootingTactics.Random;
            targetSelector = new RandomTargetSelector(recordGrid, shipLengths[0]);
        }
        private void ChangeTacticsToSurrounding()
        {
            ShootingTactics = ShootingTactics.Surrounding;
            targetSelector = new SurroundingTargetSelector();
        }
        private void ChangeTacticsToInline()
        {
            ShootingTactics = ShootingTactics.Surrounding;
            targetSelector = new SurroundingTargetSelector();
        }
        public ShootingTactics ShootingTactics { get; private set; } = ShootingTactics.Random;
        private readonly FleetGrid recordGrid;
        private Square target;
        private readonly List<int> shipLengths;
        private ITargetSelector targetSelector;
    }
}
