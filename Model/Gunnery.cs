using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.OOM.Battleship.Model
{
    public enum ShootingTactics
    {
        Random,
        Surronding,
        Inline
    }
    public class Gunnery
    {
        public readonly ShotsGrid recordGrid;
        public ShootingTactics shootingTactics { get; private set; } = ShootingTactics.Random;
        private ITargetSelector targetSelector;
        public List<int> shipLengths;
        private Square target;
        private readonly SquareEliminator eliminator=new SquareEliminator();
        private List<Square> shipSquares = new List<Square>();

        public void ProcessHitResult(HitResult hitResult)
        {
            ChangeTargetState(hitResult);
            switch (hitResult)
            {
                case HitResult.Missed:
                    return;
                case HitResult.Sunken:
                    {
                        shootingTactics = ShootingTactics.Random;
                        changeTargetSelector();
                        return;
                    }
                case HitResult.Hit:
                    {
                        shootingTactics = (shootingTactics == ShootingTactics.Random) ?
                            shootingTactics = ShootingTactics.Surronding : shootingTactics = ShootingTactics.Inline;
                        
                        changeTargetSelector();
                        return;
                    }
                default:
                    Debug.Assert(false);
                    return;
            }
        }

        private void ChangeTargetState(HitResult hitResult)
        {
            switch(hitResult) { 
                case HitResult.Missed: target.changeState(SquareState.Miss); return;
                case HitResult.Hit: target.changeState(SquareState.Hit); return;
                case HitResult.Sunken: MarkShipSunk(); return;
            
            }
        }

        private void MarkShipSunk()
        {
            shipSquares.Add(target);
            foreach(var square in shipSquares)
            {
                square.changeState(SquareState.Sunk);
            }
            var toEliminate= eliminator.ToEliminate(shipSquares,recordGrid.Rows,recordGrid.Columns);
            foreach(var square in toEliminate)
            {
                recordGrid.GetSquare(square.row, square.col).changeState(SquareState.Eliminated);
            }
            shipSquares.Clear();
        }

        public Gunnery(int rows, int columns, IEnumerable<int> shipLengths)
        {
            recordGrid=new ShotsGrid(rows, columns);
            this.shipLengths =new List<int>(shipLengths.OrderDescending());
            targetSelector = new RandomTargetSelector(recordGrid, this.shipLengths[0]);
            target = targetSelector.Next();
        }
        public Square Next()
        {
            return targetSelector.Next();
        }
        public void changeTargetSelector()
        {
            switch (shootingTactics)
            {
                case ShootingTactics.Random: targetSelector = new RandomTargetSelector(recordGrid, shipLengths[0]); break;
                case ShootingTactics.Surronding: targetSelector = new SurrondingTargetSelector(); break;
                case ShootingTactics.Inline: targetSelector = new InlineTargetSelector(); break;
                default:
                    Debug.Assert(false);
                    return;
            }
        }
    }
}
