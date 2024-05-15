using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public Gunnery(int rows, int columns, IEnumerable<int> shipLengths)
        {
            recordGrid = new Grid(rows, columns);
            this.shipLengths = new List<int>(shipLengths.OrderDescending());
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
                    switch(ShootingTactics)
                    {
                        case ShootingTactics.Random:
                            ChangeTacticsToSorruounding();
                            return;
                        case ShootingTactics.Surrounding:
                            ChangeTacticsToInline();
                            break;
                        case ShootingTactics.Inline:
                            return;
                        default:
                            Debug.Assert(false);
                            return;
                    }
                    return;
                case HitResult.Sunken:
                    ChangeTacticsToRandom();
                    return;
            }
        }

        private void ChangeTacticsToRandom()
        {
            ShootingTactics = ShootingTactics.Random;
            targetSelector = new RandomTargetSelector(recordGrid, shipLengths[0]);
        }

        private void ChangeTacticsToInline()
        {
            ShootingTactics = ShootingTactics.Inline;
            targetSelector = new InlineTargetSelector();
        }

        private void ChangeTacticsToSorruounding()
        {
            ShootingTactics = ShootingTactics.Surrounding;
            targetSelector = new InlineTargetSelector();
        }

        public ShootingTactics ShootingTactics { get; private set; } = ShootingTactics.Random;

        private readonly Grid recordGrid;
        private readonly List<int> shipLengths = [];
        private ITargetSelector targetSelector;
        private Square target;
    }
}
