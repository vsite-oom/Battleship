using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class InlineTargetSelector : ITargetSelector
    {
        private readonly List<SquareCoordinate> inlineCoordinates;
        private int currentIndex = 0;

        public InlineTargetSelector(SquareCoordinate start)
        {
            inlineCoordinates = new List<SquareCoordinate>
            {
                new SquareCoordinate(start.Row - 1, start.Column),
                new SquareCoordinate(start.Row + 1, start.Column),
                new SquareCoordinate(start.Row, start.Column - 1),
                new SquareCoordinate(start.Row, start.Column + 1)
            };
        }

        public SquareCoordinate Next()
        {
            if (currentIndex < inlineCoordinates.Count)
            {
                LastHitCoordinate = inlineCoordinates[currentIndex];
                currentIndex++;
                return LastHitCoordinate;
            }
            throw new InvalidOperationException("Nemate vise koordinata");
        }
        public SquareCoordinate LastHitCoordinate { get; private set; }
    }
}
