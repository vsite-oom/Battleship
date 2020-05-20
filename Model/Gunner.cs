using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vsite.Oom.Battleship.Model;
using static Vsite.Oom.Battleship.Model.Grid;

namespace Battleships_GUI
{
    public enum ShootingTactics
    {
        Random,
        Surrounding,
        Inline
    }
    public class Gunner
    {
        public Gunner(int rows,int columns, IEnumerable<int> shipLenghts)
        {
            evidenceGrid = new Grid(rows, columns);
            shipsToShoot = new List<int>(shipLenghts.OrderByDescending(l => l));
            ShootingTactics = ShootingTactics.Random;
            squareTerminator = new SquareTerminator(rows, columns);

        }
        public Square NextTarget()
        {
            //TODO: implement correctly
            lastTarget= SelectTarget();
            return lastTarget;
        }

        public void ProcessHitResult(HitResult hitResult)
        {
            evidenceGrid.MarkHitResult(lastTarget, hitResult);
            //recor on evidence grid so we dont shoot the same place
            switch (hitResult)
            {
                case HitResult.Missed:
                    return;
                case HitResult.Sunken:
                    //eliminate squares around the ship
                    squaresHit.Add(lastTarget);

                    var toEliminate=squareTerminator.ToEliminate(squaresHit);
                    foreach(var sq in toEliminate)
                        evidenceGrid.MarkHitResult(sq,HitResult.Missed);
                    foreach (var sq in squaresHit)
                        evidenceGrid.MarkHitResult(sq, HitResult.Sunken);
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
            //modify shooting tactics
            // - if missed ->no change
            // - if first hit -> change to surrounding shooting
            // - if second hit -> change to inline
            // - if sunken -> change to random
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
            //create simple array of sq from arr of arr
            var allCandidates = placements.SelectMany(seq => seq);
            //create groups with individual sq
            var groups=allCandidates.GroupBy(sq => sq);
            //find the number of sq in largest group
            var maxCount = groups.Max(g => g.Count());
            //filter only grouzps that have maxcount elements
            var largestGroups = groups.Where(g => g.Count() == maxCount);
            //fetch keys from each group(i.e. sq that represent the group
            var mostCommon = largestGroups.Select(g => g.Key);
            if (mostCommon.Count() == 1)
                return mostCommon.First();
            int index=random.Next(0, allCandidates.Count());
            return allCandidates.ElementAt(index);
        }

        private Square SelectFromAround()
        {
            List<IEnumerable<Square>> around = new List<IEnumerable<Square>>();
            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                var l = evidenceGrid.GetSquaresNextTo(squaresHit.First(), direction);
                if (l.Count() > 0)
                    around.Add(l);
            }

            if (around.Count == 1)
                return around[0].First();
            //TODO: improve selection so that only largest lists are taken into account
            var ordered = around.OrderByDescending(ls => ls.Count());
            int maxLen = ordered.First().Count();
            if (maxLen > shipsToShoot[0] - 1)
                maxLen = shipsToShoot[0] - 1;
            var longest = ordered.Where(ls => ls.Count() >= maxLen);

            int index = random.Next(0, around.Count);
            return around[index].First();
        }
        private Square SelectInline()
        {
            var l=evidenceGrid.GetSquaresInLine(squaresHit);
            if (l.Count() == 1)
                return l.ElementAt(0).First();
            //TODO koja od listi ima najviše članova ako ima neka najdulja onda se odabere ona 

            // l.OrderByDescending(ls => ls.Count()); //sort po duljinama 
            
            int index = random.Next(0, l.Count());
            return l.ElementAt(index).First();
        }

   
        private Square lastTarget;

        private Grid evidenceGrid;

        private List<int> shipsToShoot;

        private SortedSquares squaresHit = new SortedSquares();

        private Random random = new Random();

        private ISquareTerminator squareTerminator;

        public ShootingTactics ShootingTactics { get; private set; }
    }
}
