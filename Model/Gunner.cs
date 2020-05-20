using Model;
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
        public Gunner(int rows, int columns, IEnumerable<int> shipLengths)
        {
            evidenceGrid = new Grid(rows, columns);
            shipsToShoot = new List<int>(
            shipLengths.OrderByDescending(l => l));
            ShootingTactics = ShootingTactics.Random;
            squareTerminator = new SquareTerminator(rows, columns);
        }
        public Square NextTarget()
        {
            // implement correctly
            lastTarget = SelectTarget();
            return lastTarget;
        }
        public void ProcessHitResult(HitResult hitResult)
        {
            evidenceGrid.MarkHitResult(lastTarget, hitResult);
            //record on evidence grid
            switch (hitResult)
            {
                case HitResult.Missed:
                    return;
                case HitResult.Sunken:
                    squaresHit.Add(lastTarget);
                   
                    var toEliminate = squareTerminator.ToEliminate(squaresHit);
                    foreach (var sq in toEliminate)
                        evidenceGrid.MarkHitResult(sq, HitResult.Missed);
                    foreach (var sq in squaresHit)
                        evidenceGrid.MarkHitResult(sq, HitResult.Sunken);
                    evidenceGrid.EliminateSquares(toEliminate);
                    int length = squaresHit.Length;
                    shipsToShoot.Remove(length);
                    squaresHit.Clear();
                    ShootingTactics = ShootingTactics.Random;
                   
                    return;
                case HitResult.Hit:
                    squaresHit.Add(lastTarget);
              
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
            var largestGroup = groups.Where(g => g.Count() == maxCount);

            var mostCommon = largestGroup.Select(g => g.Key);
            if (mostCommon.Count() == 1)
                return mostCommon.First();
            int index = random.Next(0, allCandidates.Count());
            return allCandidates.ElementAt(index);
        }

        private Square SelectFromAround()
        {
            List<IEnumerable<Square>> arround = new List<IEnumerable<Square>>();
            foreach(Direction direction in Enum.GetValues(typeof(Direction)))
            {
                var l = evidenceGrid.GetSquaresNextTo(squaresHit.First(), direction);
                if (l.Count() > 0)
                    arround.Add(l);
            }
            if (arround.Count == 1)
                return arround[0].First();

            //TODO: improve selection so thath only largest list are taken ad candidates

            var ordered = arround.OrderByDescending(ls => ls.Count());
            int maxLen = ordered.First().Count();
            if (maxLen > shipsToShoot[0] - 1)
                maxLen = shipsToShoot[0] - 1;

            var longest = ordered.Where(ls => ls.Count() >= maxLen);

            int index = random.Next(0, arround.Count);
            return arround[index].First();
            

        }

        private Square SelectInline()
        {
           var l = evidenceGrid.GetSquaresInline(squaresHit);
            if (l.Count() == 1)
                return l.ElementAt(0).First();

            //TODO: improve selection so thath only largest list are taken ad candidates

            int index = random.Next(0, l.Count());
            return l.ElementAt(index).First();
        }

     

   

        private Square lastTarget;

        private Grid evidenceGrid;

        private List<int> shipsToShoot;

        private SortedSquares squaresHit = new SortedSquares();

        private ISquareTerminator squareTerminator;

        private Random random = new Random();

        public ShootingTactics ShootingTactics { get; private set; }
    }
}