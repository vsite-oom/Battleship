using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    // Fleet je kolekcija brodova.
    public class Fleet
    {
        private List<Ship> ships = new List<Ship>();  // "Koristimo listu jer nam je potrebno dodavati brodove u flotu. S IEnumerable ne možemo dodavati elemente".

        public IEnumerable<Ship> Ships { get { return ships; } }  // Koristimo IEnumerable jer ne želimo da se može mijenjati izvana.

        public void CreateShip(IEnumerable<Square> squares)
        {
            var ship = new Ship(squares);
            ships.Add(ship);  // "Mogli smo tu dodati kroz konstruktor bez stvaranja varijable, ali smo radi preglednosti ovako napravili".
        }

        public HitResult Hit(int row, int column)
        {
            foreach (var ship in ships)
            {
                if (ship.Contains(row, column))
                {
                    return ship.Hit(row, column);
                }
            }

            return HitResult.Missed;
        }
    }
}
