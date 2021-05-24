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
        public Gunnery(int rows, int columns, IEnumerable<int> shipLengths)
        {
            evidenceGrid = new Grid(rows, columns);
            var sorted = shipLengths.OrderByDescending(sl => sl).ToArray();
            shipsToShoot = sorted.ToList();
            targetSelect = new RandomShooting(evidenceGrid, shipsToShoot[0]);
        }

        public Square NextTarget()
        {
            LastTarget = targetSelect.NextTarget();
            return LastTarget;
        }

        public void RecordShooting(HitResult result)
        {
            evidenceGrid.RecordResult(LastTarget, result);
            if (result == HitResult.Missed)
                return;
            lastHits.Add(LastTarget);
            if (result == HitResult.Sunken)
            {
                // mark all squares around lastHits as missed
                SurroundingSquaresEliminator eliminator = new SurroundingSquaresEliminator(10,10);
                var toEliminate = lastHits.ToList();
                toEliminate.Sort(delegate(Square s1, Square s2) { return (s1.Column + s1.Row).CompareTo(s2.Row + s2.Column); });
                IEnumerable<Square> squares = eliminator.ToEliminate(toEliminate);
                foreach (Square square in squares)
                {
                    square.SetSquareState(HitResult.Missed);
                    
                }
                evidenceGrid.Eliminate(squares);
                // mark all squares in LastHits as Sunken uz pomoc surroundingSquaresEliminator
                foreach (Square squareSunken in lastHits)
                    squareSunken.SetSquareState(HitResult.Sunken);
            }
            ChangeTactics(result);
        }

        private void ChangeTactics(HitResult result)
        {
            if (HitResult.Missed == result && ShootingTactics == ShootingTactics.Random)
            {
                targetSelect = new RandomShooting(evidenceGrid, shipsToShoot[0]);
                shootingTactics = ShootingTactics.Random;
            }
            else if (HitResult.Hit == result && ShootingTactics == ShootingTactics.Random)
            {
                targetSelect = new SurroundingShooting(evidenceGrid, lastHits[0], shipsToShoot[0]);
                shootingTactics = ShootingTactics.Surrounding;
            }
            else if (HitResult.Missed == result && ShootingTactics == ShootingTactics.Surrounding)
            {
                targetSelect = new SurroundingShooting(evidenceGrid, lastHits[0], shipsToShoot[0]);
                shootingTactics = ShootingTactics.Surrounding;
            }
            else if(HitResult.Hit == result && ShootingTactics == ShootingTactics.Surrounding)
            {
                targetSelect = new LinearShooting(evidenceGrid, lastHits, shipsToShoot[0]);
                shootingTactics = ShootingTactics.Linear;
            }
            else if(HitResult.Sunken == result && ShootingTactics == ShootingTactics.Surrounding)
            {
                int sunkenShipLength = lastHits.Count;
                shipsToShoot.Remove(sunkenShipLength);
                lastHits.Clear();
                if (shipsToShoot.Count() != 0)
                {
                    targetSelect = new RandomShooting(evidenceGrid, shipsToShoot[0]);
                    shootingTactics = ShootingTactics.Random;
                }
            }
            else if(HitResult.Missed == result && ShootingTactics == ShootingTactics.Linear)
            {
                targetSelect = new LinearShooting(evidenceGrid, lastHits, shipsToShoot[0]);
                shootingTactics = ShootingTactics.Linear;
            }
            else if(HitResult.Sunken == result && ShootingTactics == ShootingTactics.Linear)
            {
                int sunkenShipLength = lastHits.Count;
                shipsToShoot.Remove(sunkenShipLength);
                lastHits.Clear();
                if (shipsToShoot.Count() != 0)
                {
                    targetSelect = new RandomShooting(evidenceGrid, shipsToShoot[0]);
                    shootingTactics = ShootingTactics.Random;
                }
            }

            // if result is missed stay in random shooting tactic
            // else if hit then surrounding shooting targetSelect = new SurroundingShooting(itd, itd);
            // if hit then linear shooting targetSelect = new LinearShooting(...);, otherwise stay in surrounding
            // if result is sunken, change back to random shooting targetSelect = new RandomShooting(...);
        }

        public ShootingTactics ShootingTactics
        {
            get { return shootingTactics; }
        }

        private Grid evidenceGrid;
        private List<int> shipsToShoot;
        private Square LastTarget;
        private List<Square> lastHits = new List<Square> { };
        private ITargetSelect targetSelect;
        private ShootingTactics shootingTactics = ShootingTactics.Random;
    }
}
