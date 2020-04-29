using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public enum ShipADjoining
    {
        None,
        Corners
    }
    public static class SquareTerminatorFactory
    {
        public static ISquareTerminator Create(ShipADjoining adjoining, int rows, int columns)
        {
            switch (adjoining)
            {
                case ShipADjoining.None:
                    return new SquareTerminator(rows, columns);
                case ShipADjoining.Corners:
                    return null;
                default:
                    Debug.Assert(false);
                    return null;
            }
                
        }
    }
}
