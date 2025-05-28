using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model;

public class Fleet
{
    
    private List<Ship> ships = new List<Ship>();
    private readonly List<Ship> ships = [];

    public IEnumerable<Ship> Ships { get { return ships; } }

    public void CreateShip(List<Square> squares)
    public void CreateShip(IEnumerable<Square> squares)
    {
        var ship = new Ship(squares);
        ships.Add(ship);
    }

    public HitResult Hit(int row, int column)
    {
        throw new NotImplementedException();
        foreach (var ship in ships)
        {
            var result = ship.Hit(row, column);
            if (result != HitResult.Missed)
            {
                return result;
            }
        }
        return HitResult.Missed;
    }
}


