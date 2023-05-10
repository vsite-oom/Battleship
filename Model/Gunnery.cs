using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public enum CurrentShootingTacttics
    {
        Random,
        Zone,
        Line
    }
    public class Gunnery
    {
        private readonly Grid grid;
        
        private IShootingTactics shootingTactics;

        private List<int> shipLenghts;
        public CurrentShootingTacttics CurrentShootingTacttics { get; set; }
        public GameRules GameRules { get; set; }
        List<Square> targetSquares= new List<Square>();

        public Gunnery(GameRules rules)
        {
            GameRules = rules;
            grid = new Grid(GameRules.GridRows, GameRules.GridColumns);
            shipLenghts = new List<int>(GameRules.ShipLenghts);
            CurrentShootingTacttics=ChangeToRandom();
            shootingTactics = new RandomShooting(grid,shipLenghts);
            //CurrentShootingTacttics = CurrentShootingTacttics.Random;
        }
        public Square nextTarget()
        {
            targetSquares.Add(shootingTactics.NextTarget());
            return targetSquares.Last();
        }

        public void ProcessHitResult(HitResult result)
        {
            //RecordHitResult(result);

            ChangeTactics(result);
            
            
        }
        public CurrentShootingTacttics ChangeToLine()
        {
            shootingTactics = new LineShooting(grid, targetSquares.Where(w=>w.squareState==SquareState.Hit), shipLenghts);
            return CurrentShootingTacttics.Line;
        }
        public CurrentShootingTacttics ChangeToZone()
        {
            //shootingTactics=new ZoneShooting(grid,targetSquares.Last(),shipLenghts);
            return CurrentShootingTacttics.Zone;
        }
        public CurrentShootingTacttics ChangeToRandom()
        {
            shootingTactics = new RandomShooting(grid, shipLenghts);
            //CurrentShootingTacttics = CurrentShootingTacttics.Random;
            return CurrentShootingTacttics.Random;
        }

        private void ChangeTactics(HitResult result)
        {

            CurrentShootingTacttics = (CurrentShootingTacttics, result) switch
            {
                (CurrentShootingTacttics.Random, HitResult.Hit) => ChangeToZone(),
                (CurrentShootingTacttics.Zone, HitResult.Hit) => ChangeToLine(),
                (CurrentShootingTacttics.Zone, HitResult.Sunk) or (CurrentShootingTacttics.Line, HitResult.Sunk) => ChangeToRandom(),
                _ => CurrentShootingTacttics
            };


            
        }

        private void RecordHitResult(HitResult result)
        {
            if (result == HitResult.Sunk) 
            {
                var toEliminate=GameRules.terminator.ToEliminate(targetSquares.Where(w => w.squareState == SquareState.Hit));
                foreach (var square in toEliminate)
                {
                    grid.Eliminate(square.row, square.column);
                }
                foreach (var square in targetSquares){
                    grid.MarkSquare(square.row, square.column, result);
                }
                shipLenghts.Remove(targetSquares.Count(c=>c.squareState==SquareState.Hit));
                targetSquares.Clear();
            }
            else
            {
                var lastTarget = targetSquares.Last();
                grid.MarkSquare(lastTarget.row, lastTarget.column, result);
            }
            
        }
    }
}
