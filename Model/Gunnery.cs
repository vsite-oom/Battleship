using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vsite.Oom.Battleship.Model;

namespace Vsite.Oom.Battleship
{
    public enum ShootingTactics
    {
        Random,
        Surrounding,
        Inline
    }
    public class Gunnery
    {
        public Gunnery(int rows, int columns, IEnumerable<int>shipLengths)
        {
            recordGrid = new Grid(rows, columns);
        }
        public SquareCoordinate Next()
        {
            throw new NotImplementedException();
        }

        public void ProcessHitResult(HitResult hitResult)
        {
            throw new NotImplementedException();
        }
        public ShootingTactics ShootingTactics { get; private set; } = ShootingTactics.Random;
        private readonly Grid recordGrid;

        private ITargetSelector targetSelector=new RandomTargetSelector();
    }
}
