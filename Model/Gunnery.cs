using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public enum ShootingTactics
    {
        Random,
        Surrounding,
        Inline
    }

    public class Gunnery
    {
        private readonly List<int> shipLengths = [];
        private readonly FleetGrid recordGrid;
        private Square target;
        private ITargetSelector targetSelector;

        public ShootingTactics ShootingTactics { get; private set; } = ShootingTactics.Random;

        public Gunnery(int rows, int columns, IEnumerable<int> shipLengths)
        {
            recordGrid = new FleetGrid(rows, columns);
            this.shipLengths = new List<int>(shipLengths.OrderDescending());

            targetSelector = new RandomTargetSelector(recordGrid, this.shipLengths[0]);
        }

        public Square Next()
        {
            target = targetSelector.Next();
            return target;
        }

        public void ProcessHit(HitResult hitResult)
        {
            if (hitResult == HitResult.Hit)
            {
                switch (ShootingTactics)
                {
                    case ShootingTactics.Random:
                        ShootingTactics = ShootingTactics.Surrounding;
                        targetSelector = new SurroundingTargetSelector();
                        break;
                    case ShootingTactics.Surrounding:
                        ShootingTactics = ShootingTactics.Inline;
                        targetSelector = new InlineTargetSelector();
                        break;
                }
            }
            else if (hitResult == HitResult.Sunken)
            {
                ShootingTactics = ShootingTactics.Random;
                targetSelector = new RandomTargetSelector(recordGrid, shipLengths[0]);
            }
        }
    }
}
