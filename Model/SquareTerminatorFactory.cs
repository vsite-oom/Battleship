using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
     public enum ShipAdJoining
    {
        None,
        Corners
    }
    public static class SquareTerminatorFactory
    {
        public static ISquareTerminator Create(ShipAdJoining adJoining,int rows,int columns)
        {
            switch (adJoining)
            {
                case ShipAdJoining.None:
                    return new squareTerminator(rows, columns);
                case ShipAdJoining.Corners:
                    return null;
                default:
                    Debug.Assert(false);
                    return null;
            }
        }
    }
}
