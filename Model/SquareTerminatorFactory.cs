using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public static class SquareTerminatorFactory
    {
        public enum ShipAdJoininig
        {
            None,
            Corners
        }
        public static ISquareTerminator Create(ShipAdJoininig adjoininig, int rows, int columns)
        {
            switch (adjoininig)
            {
                case ShipAdJoininig.None:
                    return new SquareTerminator(rows, columns);
                case ShipAdJoininig.Corners:
                    return null;
                default:
                    Debug.Assert(false);
                    return null;
            }

        }
    }
}
