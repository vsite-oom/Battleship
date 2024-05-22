using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.OOM.Battleship.Model
{
    public enum Direction
    {
        Upwards,
        Downwards, 
        Left, 
        Right

    }
    public class ShotsGrid : Grid
    {
        public ShotsGrid(int rows, int columns) : base(rows, columns)
        {
        }

        protected override bool IsSquareAvailable(int row, int col)
        {
            return squares[row,col]?.State==SquareState.Intact;
        }

        public Square GetSquare(int row, int col)
        {
            return squares[row, col]!;
        }
        public void ChangeSquareState(int row, int col,SquareState newState) {
            squares[row, col]!.changeState(newState);
        }

        public IEnumerable<Square> GetSquaresInDirection(int row,int col,Direction dir) {
            throw new NotImplementedException();
        }
    }
}
