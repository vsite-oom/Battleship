using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
    public class Gunner
    {
        public Gunner(int rows, int columns, IEnumerable<int> shipLengths)
        {
            evidenceGrid = new Grid(rows, columns);
            shipsToShoot = new List<int>(shipLengths.OrderByDescending(l => l));
            ShootingTactics = ShootingTactics.Random;
            squareTerminator = new SquareTerminator(rows, columns);
        }
        public Square NextTarget()
        {
            lastTarget = SelectTarget();
            return lastTarget;
        }
        public void ProcessHitResult(HitResult hitResult)
        {
            evidenceGrid.MarkHitResult(lastTarget, hitResult);
            if (hitResult == HitResult.Missed)
                return;
            squaresHit.Add(lastTarget);
            if (hitResult == HitResult.Sunken)
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

                int length = squaresHit.Length;
                shipsToShoot.Remove(length);
                squaresHit.Clear();
                
            }
            ChangeTactics(hitResult);
            
            
            //modify shooting tactics
            //if missed-no change
            //if 1st change to sorrounding
            //if 2nd change to inline
            //if sunken change to random
        }

        private void ChangeTactics(HitResult hitResult)
        {
            if (hitResult == HitResult.Sunken)
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

        private Square lastTarget;
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
        private Square SelectRandomly()
        {
            var placements=evidenceGrid.GetAvailablePlacements(shipsToShoot[0]);
            var allcandidates=placements.SelectMany(seq=>seq);
            var groups = allcandidates.GroupBy(sq => sq);
            var maxCount = groups.Max(g => g.Count());
            var largestGroups = groups.Where(g => g.Count() == maxCount);
            var mostCommon = largestGroups.Select(g => g.Key);
            if (mostCommon.Count()== 1){
                return mostCommon.First();
            }
            int index=random.Next(0, allcandidates.Count());
            return mostCommon.ElementAt(index);
        }

        private Square SelectFromArround()
        {
            List<IEnumerable<Square>> arround = new List<IEnumerable<Square>>();
            foreach(Direction direction in Enum.GetValues(typeof(Direction))){
                var l = evidenceGrid.GetSquaresNextTo(squaresHit.First(), direction);
                if (l.Count() > 0)
                {
                    arround.Add(l);
                }
            }
            if (arround.Count == 1)
            {
                return arround[0].First();
                //TODO:improve selection so that only largest list are taken as candidates
            }
            
            int index = random.Next(0,arround.Count);
            return arround[index].First();
        }

        private Square SelectInline()
        {
            var l=evidenceGrid.GetSquaresInLine(squaresHit);
            if (l.Count() == 1)
            {
                return l.ElementAt(0).First();
            }
            //l.OrderByDescending(ls => ls.Count());
            int index = random.Next(0, l.Count());
            return l.ElementAt(index).First();
        }


        
        private Grid evidenceGrid;
        private List<int> shipsToShoot;
        private SortedSquares squaresHit = new SortedSquares();
        private Random random = new Random();
        private ISquareTerminator squareTerminator;
        public ShootingTactics ShootingTactics { get; private set; }
    }
}
