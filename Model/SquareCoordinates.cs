namespace Vsite.Oom.Battleship.Model
{
    public struct SquareCoordinates
    {
        public SquareCoordinates(int row, int col)
        {
            Row = row;
            Column = col;
        }

        public readonly int Row;
        public readonly int Column;
    }
}
