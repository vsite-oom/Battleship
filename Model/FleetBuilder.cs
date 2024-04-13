﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class FleetBuilder
    {
        public FleetBuilder(int gridRows, int gridColumns, int[]shipLengths)
        {
            fleetGrid = new Grid(gridRows, gridColumns);
            this.shipsLengths = new List<int>(shipLengths);
        }
        private readonly Grid fleetGrid;
        private readonly List<int> shipsLengths;
    }
}
