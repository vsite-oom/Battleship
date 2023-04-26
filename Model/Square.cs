

using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace Vsite.Oom.Battleship.Model
{   
    public enum SquareState{

    Initial,
    Missed,
    Hit,
    Sunk
    }

    public class Square:IEquatable<Square>
    {
        readonly public int row;
        readonly public int column;
        public SquareState squareState {  get; private set; }
        public Square(int row,int column)
        {
            this.row = row;
            this.column = column;
            squareState = SquareState.Initial;
        }
        public void Mark(HitResult hitResult) 
        {

            squareState = (hitResult) switch
            {
                (HitResult.Hit) => SquareState.Hit,
                (HitResult.Missed) => SquareState.Missed,
                (HitResult.Sunk) => SquareState.Sunk,
                
            } ;
            
        }
        public bool Equals(Square other)
        {
            
            return row == other.row && column == other.column&&GetType()==other.GetType();
            
        }
        public override bool Equals(object obj)=> Equals(obj as Square);

        public override int GetHashCode() => HashCode.Combine(row, column);
        

    }
}
