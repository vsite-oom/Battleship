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
        public readonly Grid recordGrid;
        public ShootingTactics shootingTactics { get; private set; } = ShootingTactics.Random;
        private ITargetSelector targetSelector=new RandomTargetSelector();
        private Square target;

        public void ProcessHitResult(HitResult hitResult)
        {
            switch (hitResult)
            {
                case HitResult.Missed:
                    return;
                case HitResult.Sunken:
                    {
                        shootingTactics = ShootingTactics.Random;
                        targetSelector = new RandomTargetSelector();
                        return;
                    }
                case HitResult.Hit:
                    {
                        shootingTactics = (shootingTactics == ShootingTactics.Random) ?
                            shootingTactics = ShootingTactics.Surronding : shootingTactics = ShootingTactics.Inline;
                        return;
                    }
                default:
                    Debug.Assert(false);
                    return;
            }
        }
        public Gunnery(int rows, int columns, IEnumerable<int> shipLengths)
        {
            recordGrid=new Grid(rows, columns);
        }
        public Square Next()
        {
            return targetSelector.Next();
        }
        
    }
}
