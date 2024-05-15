using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
        private readonly FleetGrid recordGrid;
        private ITargetSelector targetSelector = new RandomTargetSelector();

        public ShootingTactics ShootingTactics { get; private set; } = ShootingTactics.Random;

        public Gunnery(int rows, int columns, IEnumerable<int> shipLengths)
        {
            recordGrid = new FleetGrid(rows, columns);
            this.shipLengths = new List<int>(shipLengths.OrderDescending());
            targetSelector = new RandomTargetSelector(recordGrid, this.shipLengths[0]);
        }

        public Square Next()
        {
            targetSelector.Next();
            return target;
        }

        public void ProcessHit(HitResult hitResult)
        {
            RecordTargetResult(hitResult);

            if (hitResult == HitResult.Hit)
            {

                switch (ShootingTactics)
                {
                    case ShootingTactics.Random:
                        ShootingTactics = ShootingTactics.Surrounding;
                        targetSelector = new SurroundingSelector();
                        break;
                    case ShootingTactics.Surrounding:
                        ShootingTactics = ShootingTactics.Inline;
                        targetSelector = new InlineTargetSelector();
                        break;
                }
            }
            else if (hitResult == HitResult.Sunken)
            {
                ShootingTactics = ShootingTactics.Random;
                targetSelector = new RandomTargetSelector();

            }
private void RecordTargetResult(HitResult hitResult)
        {
           switch (hitResult)
                {
                    case HitResult.Missed:
                     targetChangeState(SquareState.Missed);
                        return;

                    case HitResult.Hit:
                        target.ChangeState(SquareState.Hit);
                        return;
                        
               
                case HitResult.Sunken:
                   MarkShipAsSunken();
                    return;
            }
        }
private void MarkShipAsSunken()
        {
            shipSquares.Add(target);
            foreach (var square in shipSquares)
                {
                square.ChangeState(SquareState.Sunken);
            }
            
            var ToEliminate = eliminator.ToEliminate(shipSquares, recordGrid.Rows, recordGrid.Columns);
                foreach (var square in ToEliminate)
                {
                recordGrid.GetSquare(square.Row, square.Column).ChangeState(SquareState.Eliminated);
            }
            shipSquares.Clear();
        }

        public ShootingTactics ShootingTactics { get; private set; } = ShootingTactics.Random;

            private readonly ShortsGrid shortsGrid;

            private ITargetSelector targetSelector = new RandomTargetSelector();
            private List<Square> shipSquares = new List<Square>();
        private readonly SquareEliminator eliminator = new SquareEliminator();

        }


}
