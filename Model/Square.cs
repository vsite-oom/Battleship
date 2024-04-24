namespace Vsite.Oom.Battleship.Model
{
    public class Square
    {
        public Square(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public readonly int Row;
        public readonly int Column;

        public void Hit()
        {
            hit = true;
        }

        private bool hit = false;
        public bool IsHit => hit;
    }
}
