namespace Vsite.Oom.Battleship.Model
{
    public class Ship
    {
               
            public Ship(IEnumerable<Square> squares)
            {
            squares = squares;
            }

            public readonly IEnumerable<Square> squares;
        }
}