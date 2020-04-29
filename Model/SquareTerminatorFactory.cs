using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public enum ShipAdjoning
    {
        None,
        Corners
    }
    public static class SquareTerminatorFactory
    {
        public static ISquareTerminator Create (ShipAdjoning shipAdjoning, int rows, int columns)
        {
            switch(shipAdjoning)
            {
                case ShipAdjoning.None:
                    return new SquareTerminator(rows, columns);
                case ShipAdjoning.Corners:
                    return null;
                default:
                    Debug.Assert(false);
                    return null;
                    
            }
        }
    }
}
