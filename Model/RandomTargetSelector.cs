using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class RandomTargetSelector : ITargetSelector
    {
        private int rows;
        private int columns;
        private readonly Random random = new Random();

        public RandomTargetSelector(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;
        }

        public SquareCoordinate Next()
        {
            LastHitCoordinate = new SquareCoordinate(random.Next(rows), random.Next(columns));
            return LastHitCoordinate;
        }
        public SquareCoordinate LastHitCoordinate { get; private set; }
    }
}
