namespace Vsite.Oom.Battleship.Model
{
    public class SurroundingSquaresTerminator : ISquaresTerminator
    {
        public SurroundingSquaresTerminator(int gridRows, int gridColumns)
        {
            this.gridRows = gridRows;
            this.gridColumns = gridColumns;
        }

        private readonly int gridRows;
        private readonly int gridColumns;

        public IEnumerable<Square> ToEliminate(IEnumerable<Square> ShipSquare) 
        {
            throw new NotImplementedException();
        }
    }
}
