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

            if (hitResult == HitResult.Missed)
                return;
            squaresHit.Add(lastTarget);
            if(hitResult == HitResult.Sunken)
            {
                var toEliminate = squareTerminator.ToEliminate(squaresHit);
                foreach (var sq in toEliminate)
                {
                    evidenceGrid.MarkHitResult(sq, HitResult.Missed);
                }
                foreach (var sq in squaresHit)
                {
                    evidenceGrid.MarkHitResult(sq, HitResult.Sunken);
                }

                int lenght = squaresHit.Lenght;
                shipsToShoot.Remove(lenght);
                squaresHit.Clear();

            }
            ChangeTactics(hitResult);

            
          
        }

        private void ChangeTactics(HitResult hitResult)
        {
            if(hitResult == HitResult.Sunken)
            {
                ShootingTactics = ShootingTactics.Random;
                return;
            }
            if (hitResult == HitResult.Hit) 
            {
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
            var groups = allCandidates.GroupBy(sq => sq);
            var maxCount = groups.Max(g => g.Count());
            var largestGroups = groups.Where(g => g.Count() == maxCount);
            var mostCommon = largestGroups.Select(g => g.Key);
            if (mostCommon.Count() == 1)
                return mostCommon.First();
            int index = random.Next(0, mostCommon.Count());
            return mostCommon.ElementAt(index);
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
                var l = evidenceGrid.GetSquaresNextTo(squaresHit.First(), direction);
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

        private SortedSquares squaresHit = new SortedSquares();

        private ISquareTerminator squareTerminator;
        public ShootingTactics ShootingTactics { get; private set; }

    }
}
