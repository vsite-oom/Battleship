using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public enum ShootingTactics { 
        Random,
        Surrounding,
        Inline

    }
    public class Gunner
    {
        public Gunner(int rows, int columns, IEnumerable<int> shipLenghts) {
            evidenceGrid = new Grid(rows, columns);
            shipsToShoot = new List<int>(shipLenghts.OrderByDescending(l => l));
            ShootingTactics = ShootingTactics.Random;
            squareTerminator = new SquareTerminator(rows, columns);

        }
        public Square NextTarget() {
            //TODO: implement

            lastTarget = SelectTarget();
            return lastTarget;
        }

        public void ProcessHitResult(HitResult hitResult) {

            evidenceGrid.MarkHitResult(lastTarget, hitResult);
            switch (hitResult)
            {
                case HitResult.Missed:
                    return;
                case HitResult.Sunken:
                    squaresHit.Add(lastTarget);
                    squaresHit = squaresHit.OrderBy(s => s.Row + s.Column).ToList();
                    var toEliminate = squareTerminator.ToEliminate(squaresHit);
                    foreach(var sq in toEliminate)
                    {
                        evidenceGrid.MarkHitResult(sq, HitResult.Missed);
                    }
                    foreach (var sq in squaresHit)
                    {
                        evidenceGrid.MarkHitResult(sq, HitResult.Sunken);
                    }

                    int lenght = squaresHit.Count();
                    shipsToShoot.Remove(lenght);
                    squaresHit.Clear();
                    ShootingTactics = ShootingTactics.Random;
                    return;
                case HitResult.Hit:
                    squaresHit.Add(lastTarget);
                    squaresHit = squaresHit.OrderBy(s => s.Row + s.Column).ToList();
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
            switch (ShootingTactics) {
                case ShootingTactics.Random:
                    return SelectRandomly();
                case ShootingTactics.Surrounding:
                    return SelectFromAround();
                case ShootingTactics.Inline:
                    return SelectInline();
                default:
                    Debug.Assert(false);
                    return null;
            }
               
        }
        private Square SelectRandomly()
        {
            var placements = evidenceGrid.GetAvailablePlacements(shipsToShoot[0]);
            var allCandidates = placements.SelectMany(seq => seq);
            int index = random.Next(0, allCandidates.Count());
            return allCandidates.ElementAt(index);
        }
        private Square SelectInline()
        {
            var l = evidenceGrid.GetSquaresInLine(squaresHit);
            if (l.Count() == 1)
                return l.ElementAt(0).First();

            l.OrderByDescending(ls => ls.Count());
            int index = random.Next(0, l.Count());
            return l.ElementAt(index).First();

        }
        private Square SelectFromAround()
        {
            List<IEnumerable<Square>> around = new List<IEnumerable<Square>>();
            foreach (Direction direction in Enum.GetValues(typeof(Direction))) {
                var l = evidenceGrid.GetSquaresNextTo(lastTarget, direction);
                if (l.Count() > 0)
                    around.Add(l);
            }
            if (around.Count == 1)
                return around[0].First();

            int index = random.Next(0, around.Count);
            return around[index].First();
        }



        private Square lastTarget;

        private Grid evidenceGrid;

        private Random random = new Random();

        private List<int> shipsToShoot;

        private List<Square> squaresHit = new List<Square>();

        private ISquareTerminator squareTerminator;
        public ShootingTactics ShootingTactics { get; private set; }

    }
}
