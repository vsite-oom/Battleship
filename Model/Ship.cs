using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public enum HitResult
    {
        Missed,
        Hit,
        Sunken
    }

    public class Ship
    {
        public Ship(IEnumerable<Square> squares)
        {
            Squares = squares;
        }

        public readonly IEnumerable<Square> Squares;  // Stavili smo public jer je potrebno za testiranje i prvo slovo veliko jer po konvenciji je prvo slovo veliko za public member-e.

        public bool Contains(int row, int column)
        {
            return Squares.FirstOrDefault(sq => sq.Row == row && sq.Column == column) != null;
        }

        // Metoda koja provjerava je li brod promašen, pogođen ili potopljen.
        public  HitResult Hit(int row, int column)
        {
            var square = Squares.FirstOrDefault(sq => sq.Row == row && sq.Column == column);
            if (square == null)
            {
                return HitResult.Missed;
            }

            square.Hit();  // Ovdje pozivamo metodu Hit() iz klase Square koja postavlja stanje kvadrata na Hit.

            if (Squares.All(sq => sq.IsHit))
            {
                foreach (var sq in Squares)
                {
                    sq.ChangeState(SquareState.Sunken);
                }
                return HitResult.Sunken;
            }

            return HitResult.Hit;
        }
    }
}
