using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public enum Direction
    {
        Upwards,
        Rightwards,
        Downwards,
        Leftwards
    }

    public class ShotsGrid : Grid
    {
        private readonly int _rows;
        private readonly int _columns;

        public ShotsGrid(int rows, int columns) : base(rows, columns)
        {
            _rows = rows;
            _columns = columns;
        }

        protected override bool IsSquareAvailable(int row, int column)
        {
            return squares[row, column]?.SquareState == SquareState.Intact;
        }

        public void ChangeSquareState(int row, int column, SquareState newState)
        {
            squares[row, column]!.ChangeState(newState);
        }

        public IEnumerable<Square> GetSquaresInDirection(int row, int column, Direction dir)
        {
            List<Square> squares = new();

            int tempRow = row;
            int tempCol = column;

            int rowDir = dir switch
            {
                Direction.Upwards => -1,
                Direction.Downwards => 1,
                _ => 0
            };

            int colDir = dir switch
            {
                Direction.Rightwards => 1,
                Direction.Leftwards => -1,
                _ => 0
            };

            while (tempRow > 0 && tempRow < _rows - 1 && tempCol > 0 && tempCol < _columns - 1)
            {                
                tempRow += rowDir;
                tempCol += colDir;

                if (!IsSquareAvailable(tempRow, tempCol))
                    break;

                squares.Add(new Square(tempRow, tempCol));
            }

            return squares;

        }
    }
}
