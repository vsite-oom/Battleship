using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vsite.Oom.Battleship.Model;

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
        private readonly Grid recordGrid;
        private ITargetSelector targetSelector = new RandomTargetSelector();
        public ShootingTactics ShootingTactics { get; private set; } = ShootingTactics.Random;
        public Gunnery(int rows, int columns, IEnumerable<int> shipLengths)
        {
            recordGrid = new Grid(rows, columns);
        }

        public Square Next()
        {
            target=targetSelector.Next();
            return target;
        }
        public void ProcessHitResult(HitResult hitResult)
        {
            
            switch (ShootingTactics)
            {
                case ShootingTactics.Random:
                    targetSelector = new RandomTargetSelector();
                    break;
                case ShootingTactics.Surrounding:
                    targetSelector = new SurroundingTargetSelector();
                    break;
                case ShootingTactics.Inline:
                    targetSelector = new InlineTargetSelector();
                    break;
            }
        }

    }
}
