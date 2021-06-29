using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public enum ShootingTactics
    {
        Random,
        Surrounding,
        Linear
    }

    public class Gunnery
    {
        public Gunnery(int rows, int columns, IEnumerable<int> shipLengths) : this(rows,columns,shipLengths, new SurroundingSquareEliminator(rows, columns))
        {

        }

        public Gunnery(int rows, int columns, IEnumerable<int> shipLengths, ISquareEliminate eliminator )
        {
            var sorted = shipLengths.OrderByDescending(s => s);
            shipsToSink = new List<int>(sorted);
            evidenceGrid = new Grid(rows, columns);
            selectTarget = new RandomShooting(evidenceGrid, shipsToSink.Max());
            Eliminator = eliminator;
        }

        public Square NextTarget()
        {
            lastTarget = selectTarget.NextTarget();
            return lastTarget;
        }

        public void ProcessShootingResult(HitResult result)
        {
            // Mark the result in evidence grid
            switch (result)
            {
                case HitResult.Missed:
                    evidenceGrid.MarkSquare(lastTarget, SquareState.Missed);
                    break;
                case HitResult.Hit:
                    squaresHit.Add(lastTarget);
                    break;
                case HitResult.Sunken:
                    squaresHit.Add(lastTarget);
                    shipsToSink.Remove(squaresHit.Count);
                    var surroundingSquares = Eliminator.ToEliminate(squaresHit);
                    foreach (var square in surroundingSquares)
                        evidenceGrid.MarkSquare(square, SquareState.Missed);
                    foreach (var square in squaresHit)
                        evidenceGrid.MarkSquare(square, SquareState.Sunken);
                    squaresHit.Clear();
                    break;
            }
            if (shipsToSink.Count > 0)
                ChangeTactics(result);
        }

        private void ChangeTactics(HitResult result)
        {
            switch (result)
            {
                case HitResult.Missed:
                    return;
                case HitResult.Hit:
                    if (currentTactics == ShootingTactics.Random)
                    {
                        selectTarget = new SurroundingShooting(evidenceGrid, squaresHit[0], shipsToSink.Max());
                        currentTactics = ShootingTactics.Surrounding;
                        return;
                    }
                    if (currentTactics == ShootingTactics.Surrounding || currentTactics == ShootingTactics.Linear)
                    {
                        selectTarget = new LinearShooting(evidenceGrid, squaresHit, shipsToSink.Max());
                        currentTactics = ShootingTactics.Linear;
                        return;
                    }
                    break;
                case HitResult.Sunken:
                    selectTarget = new RandomShooting(evidenceGrid, shipsToSink.Max());
                    currentTactics = ShootingTactics.Random;
                    break;
            }
        }

        public ShootingTactics CurrentTactics
        {
            get { return currentTactics; }
        }

        private Grid evidenceGrid;
        private List<int> shipsToSink;
        private ISelectTarget selectTarget;
        private ShootingTactics currentTactics = ShootingTactics.Random;
        private List<Square> squaresHit = new List<Square>();
        private Square lastTarget;
        private ISquareEliminate Eliminator;
    }
}
