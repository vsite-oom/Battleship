using System.Collections.Generic;
using System.Linq;

namespace Vsite.Oom.Battleship.Model
{
    public enum Direction
    {
        Upwards,
        Rightwards,
        Downards,
        Leftwards,
    }

    public class ShotsGrid : Grid
    {
        public ShotsGrid(int rows, int columns) : base(rows, columns)
        {
        }

        protected override bool IsSquareAvailable(int row, int column)
        {
            return squares[row, column]?.SquareState == SquareState.Intact;
        }

        public void ChangeSquareState(int row, int column, SquareState newState)
        {
            squares[row, column].ChangeState(newState);
        }

        public IEnumerable<Square> GetSquaresInDirection(int row, int column, Direction direction)
        {
            var squaresInDirection = new List<Square>();

            switch (direction)
            {
                case Direction.Upwards:
                    for (int r = row - 1; r >= 0; r--)
                    {
                        squaresInDirection.Add(squares[r, column]);
                    }
                    break;
                case Direction.Rightwards:
                    for (int c = column + 1; c < Columns; c++)
                    {
                        squaresInDirection.Add(squares[row, c]);
                    }
                    break;
                case Direction.Downards:
                    for (int r = row + 1; r < Rows; r++)
                    {
                        squaresInDirection.Add(squares[r, column]);
                    }
                    break;
                case Direction.Leftwards:
                    for (int c = column - 1; c >= 0; c--)
                    {
                        squaresInDirection.Add(squares[row, c]);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }

            return squaresInDirection;
        }
    }
}
