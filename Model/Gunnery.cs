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
            switch (ShootingTactics)
            {
                case ShootingTactics.Random:
                    {
                        if (hitResult == HitResult.Hit)
                        {
                            ShootingTactics = ShootingTactics.Surrounding;
                            targetSelector = new SurroundingTargetSelector();
                        }
                        break;
                    }
                case ShootingTactics.Surrounding:
                    {
                        if (hitResult == HitResult.Hit)
                        {
                            ShootingTactics = ShootingTactics.Inline;
                            targetSelector = new InlineTargetSelector();
                        }
                        else if (hitResult == HitResult.Sunken)
                        {
                            ShootingTactics = ShootingTactics.Random;
                            targetSelector = new RandomTargetSelector(recordGrid, this.shipLengths[0]);
                        }
                        break;
                    }
                case ShootingTactics.Inline:
                    {
                        if (hitResult == HitResult.Sunken)
                        {
                            ShootingTactics = ShootingTactics.Random;
                            targetSelector = new RandomTargetSelector(recordGrid, this.shipLengths[0]);
                        }
                        break;
                    }
            }
        }

        public ShootingTactics ShootingTactics { get; private set; } = ShootingTactics.Random;  // Initially it will be random.

        private readonly Grid recordGrid;

        private List<int> shipLengths = [];

        private Square target;

        private ITargetSelector targetSelector;
    }
}
