using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.OOM.Battleship.Model
{
    public enum ShootingTactics
    {
        Random,
        Surronding,
        Inline
    }
    public class Gunnery
    {
        public readonly FleetGrid recordGrid;
        public ShootingTactics shootingTactics { get; private set; } = ShootingTactics.Random;
        private ITargetSelector targetSelector;
        public List<int> shipLengths;

        public void ProcessHitResult(HitResult hitResult)
        {
            switch (hitResult)
            {
                case HitResult.Missed:
                    return;
                case HitResult.Sunken:
                    {
                        shootingTactics = ShootingTactics.Random;
                        changeTargetSelector();
                        return;
                    }
                case HitResult.Hit:
                    {
                        shootingTactics = (shootingTactics == ShootingTactics.Random) ?
                            shootingTactics = ShootingTactics.Surronding : shootingTactics = ShootingTactics.Inline;
                        changeTargetSelector();
                        return;
                    }
                default:
                    Debug.Assert(false);
                    return;
            }
        }
        public Gunnery(int rows, int columns, IEnumerable<int> shipLengths)
        {
            recordGrid=new FleetGrid(rows, columns);
            this.shipLengths =new List<int>(shipLengths.OrderDescending());
            targetSelector = new RandomTargetSelector(recordGrid, this.shipLengths[0]);
        }
        public Square Next()
        {
            return targetSelector.Next();
        }
        public void changeTargetSelector()
        {
            switch (shootingTactics)
            {
                case ShootingTactics.Random: targetSelector = new RandomTargetSelector(recordGrid, shipLengths[0]); break;
                case ShootingTactics.Surronding: targetSelector = new SurrondingTargetSelector(); break;
                case ShootingTactics.Inline: targetSelector = new InlineTargetSelector(); break;
                default:
                    Debug.Assert(false);
                    return;
            }
        }
    }
}
