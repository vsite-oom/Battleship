namespace Vsite.Oom.Battleship.Model
{
    public class RandomShooting : IShootingTactics
    {
        public RandomShooting(Grid grid, IEnumerable<int> shipLengths)
        {
            this.grid = grid;
            this.shipLengths = shipLengths;
        }

        private readonly Grid grid;
        private readonly IEnumerable<int> shipLengths; 

        public Square NextTarget()
        {
            throw new NotImplementedException();
        }
    }
}
