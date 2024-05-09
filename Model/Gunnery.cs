using System;
using System.Collections.Generic;
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
        public void ProcessHitResult(HitResult hitResult)
        {
            switch (hitResult)
            {
                case HitResult.Sunken:
                    {
                        shootingTactics = ShootingTactics.Random;
                        break;
                    }
                case HitResult.Hit:
                    {
                        shootingTactics = (shootingTactics == ShootingTactics.Random) ?
                            shootingTactics = ShootingTactics.Surronding : shootingTactics = ShootingTactics.Inline;
                        break;
                    }
            }
        }
        public Gunnery(int rows, int columns, IEnumerable<int> shipLengths)
        {
            recordGrid=new Grid(rows, columns);
        }
        public SquareCoordinate Next()
        {
            throw new NotImplementedException();
        }
        
    }
}
