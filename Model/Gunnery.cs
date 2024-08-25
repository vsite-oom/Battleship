using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public enum ShootingTactics  // Služi za praćenje taktike gađanja.
    {
        Random,
        Surrounding,
        Inline
    }

    public class Gunnery
    {
        public Gunnery(int rows, int columns, IEnumerable<int> shipLengths)
        {
            recordGrid = new ShotsGrid(rows, columns);
            this.shipLengths = new List<int>(shipLengths.OrderDescending());
            targetSelector = new RandomTargetSelector(recordGrid, this.shipLengths[0]);  // Inicijalno koristi nasumično gađanje.
        }

        public Square Next()
        {
            target = targetSelector.Next();  // Odabir polja za gađanje.
            return target;
        }

        public void ProcessHitResult(HitResult hitResult)  // Ovisno o trenutnoj taktici gađanja i rezultatu gađanja, obrađuje rezultat i postavlja sljedeću taktiku.
        {
            RecordTargetResult(hitResult);  // Zapisuje rezultat gađanja.

            switch (ShootingTactics)
            {
                case ShootingTactics.Random:
                    {
                        if (hitResult == HitResult.Hit)
                        {
                            ShootingTactics = ShootingTactics.Surrounding;
                            targetSelector = new SurroundingTargetSelector(recordGrid, target, shipLengths[0]);
                        }
                        break;
                    }
                case ShootingTactics.Surrounding:
                    {
                        if (hitResult == HitResult.Hit)
                        {
                            ShootingTactics = ShootingTactics.Inline;
                            targetSelector = new InlineTargetSelector(recordGrid, shipSquares, shipLengths[0]);
                        }
                        else if (hitResult == HitResult.Sunken)
                        {
                            ShootingTactics = ShootingTactics.Random;
                            targetSelector = new RandomTargetSelector(recordGrid, this.shipLengths[0]);
                        }
                        break;
                    }
                case ShootingTactics.Inline:
                    {
                        if (hitResult == HitResult.Sunken)
                        {
                            ShootingTactics = ShootingTactics.Random;
                            targetSelector = new RandomTargetSelector(recordGrid, this.shipLengths[0]);
                        }
                        break;
                    }
                default:  // Za slučaj da netko dodaje nove taktike, a zaboravi ih obraditi u ovom switch-case-u da mu baci exception.
                    {
                        Debug.Assert(false, "Invalid shooting tactics.");
                        break;
                    }
            }
        }

        private void RecordTargetResult(HitResult hitResult)  // Ovisno o rezultatu gađanja, mijenja stanje polja u tablici s rezultatima gađanja i dodaje polja koja su pogođena u listu pogođenih polja.
        {
            switch (hitResult)
            {
                case HitResult.Missed:
                    target.ChangeState(SquareState.Missed);
                    return;
                case HitResult.Hit:
                    target.ChangeState(SquareState.Hit);
                    shipSquares.Add(target);
                    return;
                case HitResult.Sunken:
                    MarkShipSunken();
                    return;
            }
        }

        private void MarkShipSunken()
        {
            shipSquares.Add(target);
            foreach (var square in shipSquares)
            {
                square.ChangeState(SquareState.Sunken);
            }
            var toEliminate = eliminator.ToEliminate(shipSquares, recordGrid.Rows, recordGrid.Columns);
            foreach (var square in toEliminate)
            {
                recordGrid.ChangeSquareState(square.Row, square.Column, SquareState.Eliminated);
            }
            shipSquares.Clear();
        }

        public ShootingTactics ShootingTactics { get; private set; } = ShootingTactics.Random;  // Initially it will be random.

        private readonly ShotsGrid recordGrid;  // Tablica s rezultatima gađanja.

        private readonly List<int> shipLengths = [];  // Duljine preostalih brodova koje treba potopiti.

        private List<Square> shipSquares = new List<Square>();  // Pogođena polja broda.

        private Square target;  // Polje koje se gađa.

        private ITargetSelector targetSelector;  // Odabir sljedećeg polja za gađanje.

        private readonly SquareEliminator eliminator = new SquareEliminator();  // Eliminator polja.
    }
}
