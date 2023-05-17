namespace Vsite.Oom.Battleship.Model

{
    public class Ship
    {
        public Ship(IEnumerable<Square> squares)
        {
            Squares = squares;
        }

        public readonly IEnumerable<Square> Squares;

        public HitResult Fire(Square target)
        {
            var found = Squares.FirstOrDefault(s => s == target);
            if (found == null)
            {
                return HitResult.Missed;
            }
            found.Mark(HitResult.Hit);
            if (Squares.All(s => s.SquareState == SquareState.Hit))
            {
                foreach (var square in Squares)
                {
                    square.Mark(HitResult.Sunk);
                }
                return HitResult.Sunk;
            }
            return HitResult.Hit;
        }
    }
}
