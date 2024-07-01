﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vsite.oom.battleship.model
{
    public class Fleet
    {
        private List<Ship> ships = new List<Ship>();
        public IEnumerable<Ship> Ships { get {  return ships; } }

        public void CreateShip(IEnumerable<Square> squares)
        {
            var ship = new Ship(squares);
            ships.Add(ship);
        }

        public HitResult Hit(int row, int column)
        {
            foreach (var ship in Ships)
            {
                var result = ship.Hit(row, column);
                if(result != HitResult.Missed)
                {
                    return result;
                }
            }
            return HitResult.Missed;
        }
    }
}
