namespace Vsite.Oom.Battleship.Model
{
    public enum SquareState
    {
        Intact,
        Eliminated,
        Missed,
        Hit,
        Sunken
    }

    public class Square
    {
        public readonly int Row;
        public readonly int Column;
        public bool IsHit => (int)SquareState >= (int)SquareState.Hit;
        public SquareState SquareState { get; set; }

        public Square(int row, int column)
        {
            Row = row;
            Column = column;
            SquareState = SquareState.Intact;
        }

        public void Hit()
        {
            SquareState = SquareState.Hit;
        }

        public void ChangeState(SquareState newState)
        {
            if ((int)newState > (int)SquareState)
            {
                SquareState = newState;
            }
        }


        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Square other = (Square)obj;
            return Row == other.Row && Column == other.Column;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Row, Column);
        } 
    }
}
