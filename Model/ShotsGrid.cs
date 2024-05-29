using System;
using System.Collections.Generic;
using System.Data.Common;
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
        public ShotsGrid(int rows, int columns) : base(rows, columns)
        { 
        }
        protected override bool IsSquareAvailable(int row, int column)
        {
            return squares[row, column]?.SquareState == SquareState.Intact;
        }
        public void ChangeSquareState(int row, int column, SquareState newState)
        {
            squares[row, column]!.ChangeState(newState);
        }

        public IEnumerable<Square> GetSquaresInDirection(int row, int col, Direction direction)
        {
            var squaresInDirection = new List<Square>();
            switch (direction)
            {
                case Direction.Upwards:
                    for (int r = row - 1; r >= 0; r--)
                    {
                        if (IsSquareAvailable(r, col))
                            squaresInDirection.Add(squares[r, col]!);
                        else
                            break;
                    }
                    break;
                case Direction.Rightwards:
                    for (int c = col + 1; c < Columns; c++)
                    {
                        if (IsSquareAvailable(row, c))
                            squaresInDirection.Add(squares[row, c]!);
                        else
                            break;
                    }
                    break;
                case Direction.Downwards:
                    for (int r = row + 1; r < Rows; r++)
                    {
                        if (IsSquareAvailable(r, col))
                            squaresInDirection.Add(squares[r, col]!);
                        else
                            break;
                    }
                    break;
                case Direction.Leftwards:
                    for (int c = col - 1; c >= 0; c--)
                    {
                        if (IsSquareAvailable(row, c))
                            squaresInDirection.Add(squares[row, c]!);
                        else
                            break;
                    }
                    break;
            }
            return squaresInDirection;
        }
    }
}
