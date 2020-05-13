using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Vsite.Oom.Battleship.Model.Ship;

namespace Vsite.Oom.Battleship.Model
{
    public enum ShootingTactics
    {
        Random,
        Surrounding,
        Inline
    }
    public class Gunner
    {
        public Gunner(int rows, int colums, IEnumerable<int> shipLengths)
        {
            evidenceGrid = new Grid(rows, colums);
            shipsToShoot = new List<int>(shipLengths.OrderByDescending(l => l));
            ShootingTactics = ShootingTactics.Random;
            squareTerminator = new SquareTerminator(rows, colums);
        }
        public Square NextTarget()
        {
            //TODO: implement correctly
            lastTarget = SelectTarget();
            return lastTarget;
        }

        public void ProcesHitResult(HitResult hitResult)
        {
            evidenceGrid.MarkHitResult(lastTarget, hitResult);
            // record on evidence grid
            // modify shooting tactics
            // - if missed no change
            // - if first hit change to line shooting
            // - if second hit change to inline
            // - if sunk change to random
            switch (hitResult)
            {
                case HitResult.Missed:
                    return;
                case HitResult.Sunk:
                    squaresHit.Add(lastTarget);
                    squaresHit.OrderBy(s => s.Row + s.Column);
                    foreach (var sq in squareTerminator.ToEliminate(squaresHit))
                    {
                        evidenceGrid.MarkHitResult(sq, HitResult.Missed);
                    }
                    foreach (var sq in squaresHit)
                    {
                        evidenceGrid.MarkHitResult(sq, HitResult.Hit);
                    }
                    shipsToShoot.Remove(squaresHit.Count());
                    squaresHit.Clear();
                    ShootingTactics = ShootingTactics.Random;
                    return;
                case HitResult.Hit:
                    squaresHit.Add(lastTarget);
                    squaresHit.OrderBy(s => s.Row + s.Column);
                    switch (ShootingTactics)
                    {
                        case ShootingTactics.Random:
                            ShootingTactics = ShootingTactics.Surrounding;
                            return;

                        case ShootingTactics.Surrounding:
                            ShootingTactics = ShootingTactics.Inline;
                            return;
                        case ShootingTactics.Inline:
                            return;

                    }
                    break;
            }

        }

    

        private Square SelectTarget()
        {
            switch (ShootingTactics)
            {
                case ShootingTactics.Random:
                    return SelectRandomly();
                case ShootingTactics.Surrounding:
                    return SelectFromArround();
                case ShootingTactics.Inline:
                    return SelectInline();
                default:
                    Debug.Assert(false);
                    return null;
            }
        }

        private Square SelectInline()
        {
            var l = evidenceGrid.GetSquaresInLine(squaresHit);
            if (l.Count() == 1)
                return l.ElementAt(0).First();
           
                var index = random.Next(0, l.Count());
                return l.ElementAt(index).First();  
        }

        private Square SelectFromArround()
        {
            List<IEnumerable<Square>> around = new List<IEnumerable<Square>>();

            foreach(Direction direction in Enum.GetValues(typeof(Direction)))
            {
                var l = evidenceGrid.GetSquaresNextTo(lastTarget, direction);
                if (l.Count() > 0)
                    around.Add(l);
            }
            if (around.Count() == 1)
                return around[0].First();

            //TODO: improve list selection to take only largest list
            var index = random.Next(0, around.Count());

            return around[index].First();
        }

        private Square SelectRandomly()
        {
            var placements = evidenceGrid.GetAvailablePlacements(shipsToShoot[0]).SelectMany(s=> s);           
            var index = random.Next(0, placements.Count());
            return placements.ElementAt(index);
            

        }
               
        private Random random = new Random();
        private Square lastTarget;
        private Grid evidenceGrid;
        private List<int> shipsToShoot;
        private List<Square> squaresHit = new List<Square>();
        private ISquareTerminator squareTerminator;
        public ShootingTactics ShootingTactics { get; private set; }
    }
}
