using System;
using System.Collections.Generic;
using System.Data;
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
        }

        public SquareCoordinate Next()
        {
            throw new NotImplementedException();
        }
        public void ProcessHitResult(HitResult hitResult)
        {
            switch (ShootingTactics)
            {
                case ShootingTactics.Random:
                    {
                        if (hitResult == HitResult.Missed)
                        {
                            ShootingTactics = ShootingTactics.Random;
                        }
                        else if (hitResult == HitResult.Hit)
                        {
                            ShootingTactics = ShootingTactics.Surrounding;
                        }
                        break;
                    }
                case ShootingTactics.Surrounding:
                    {
                        if (hitResult == HitResult.Missed)
                        {
                            ShootingTactics = ShootingTactics.Surrounding;
                        }
                        else if (hitResult == HitResult.Hit)
                        {
                            ShootingTactics = ShootingTactics.Inline;
                        }
                        else if (hitResult == HitResult.Sunken)
                        {
                            ShootingTactics = ShootingTactics.Random;
                        }
                        break;
                    }
                case ShootingTactics.Inline:
                    {
                        if (hitResult == HitResult.Missed || hitResult == HitResult.Hit)
                        {
                            ShootingTactics = ShootingTactics.Inline;
                        }
                        else if (hitResult == HitResult.Sunken)
                        {
                            ShootingTactics = ShootingTactics.Random;
                        }
                        break;
                    }
            }
        }

        public ShootingTactics ShootingTactics { get; private set; } = ShootingTactics.Random;  // Initially it will be random.

        private readonly Grid recordGrid;

        private ITargetSelector targetSelector = new RandomTargetSelector();
    }
}
