﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class ZoneShooting : IShootingTactics
    {
        public ZoneShooting(Grid grid, Square firstSquareHit)
        {
            this.grid = grid;
            firsthit = firstSquareHit
        }

        private readonly Grid grid;
        private readonly Square firsthit;
        public Square NextTarget()
        {
            throw new NotImplementedException();
        }
    }
}