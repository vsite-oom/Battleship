namespace Vsite.Oom.Battleship.Model
{
    public class Square
    {

        public Square(int row, int column)
        {
            this.Row = row;
            this.Column = column;
        }

        public readonly int Row;
        public readonly int Column;

        private bool hit = false;
        public bool IsHit => hit;

        public void Hit()
        {
            hit = true;
        }

        
    }
}
