namespace Vsite.Oom.Battleship.Model
{
    public class GameRules
    {
        public readonly int GridColumns = 10;
        public readonly int GridRows = 10;
        public readonly IEnumerable<int> ShipLengths = new List<int> { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 };
        public readonly ISquaresTerminator Terminator;
    }
}
