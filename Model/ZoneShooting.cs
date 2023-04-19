namespace Vsite.Oom.Battleship.Model
{
    public class ZoneShooting : IShootingTactics
    {
        public ZoneShooting(Grid grid, Square fisrtHit)
        {
            this.grid = grid;
            this.fisrtHit = fisrtHit;
        }

        private readonly Grid grid;
        private readonly Square fisrtHit;

        public Square NextTarget()
        {
            throw new NotImplementedException();
        }
    }
}
