using System.Diagnostics;

namespace Vsite.Oom.Battleship.Model
{
    public static class SquareTerminatorFactory
    {
        public static ISquareTerminator Create(ShipAdjoining adjoining, int rows, int columns)
        {
            switch (adjoining)
            {
                case ShipAdjoining.None:
                    return new SquareTerminator(rows, columns);
                case ShipAdjoining.Corners:
                    return null;
                default:
                    Debug.Assert(false);
                    return null;
            }
        }
    }
}
