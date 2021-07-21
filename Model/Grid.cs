using System.Collections.Generic;
using System.Linq;

namespace Vsite.Oom.Battleship.Model {
    public enum Direction {
        Upwards,
        Rightwards,
        Downwards,
        Leftwards
    }

    public class Grid {
        public readonly int rows;
        public readonly int colums;
        private readonly Square?[,] squares;
        private readonly ISquareEliminator squareEliminator = new OnlyShipSquaresEliminator();

        public Grid(int rows, int colums) {
            this.rows = rows;
            this.colums = colums;

            squares = new Square?[rows, colums];

            for (int r = 0; r < rows; ++r) {
                for (int c = 0; c < colums; ++c) {
                    squares[r, c] = new Square(r, c);
                }
            }
        }

        public void RecordResult(Square square, HitResult result) {
            square.SetSquareState(result);
            squares[square.row, square.column] = square;
        }

        public Grid(int rows, int columns, ISquareEliminator squareEliminator) : this(rows, columns) {
            this.squareEliminator = squareEliminator;
        }

        public IEnumerable<IEnumerable<Square>> GetAvailablePlacements(int length) {
            List<List<Square>> result = GetHorizontalPlacements(length);

            if (length > 1) {
                result.AddRange(GetVerticalPlacements(length));
            }

            return result;
        }

        public IEnumerable<Square> GetAvailablePlacementsInDirection(Square from, Direction direction) {
            int deltarow = 0;
            int deltacolumn = 0;
            int count = 0;

            switch (direction) {
                case Direction.Upwards:
                    deltarow = -1;
                    count = from.row + 1;
                    break;

                case Direction.Rightwards:
                    deltacolumn = 1;
                    count = colums - from.column;
                    break;

                case Direction.Downwards:
                    deltarow = 1;
                    count = rows - from.row;
                    break;

                case Direction.Leftwards:
                    deltacolumn = -1;
                    count = from.column + 1;
                    break;
            }

            List<Square> result = new List<Square>();

            int row = from.row + deltarow;
            int column = from.column + deltacolumn;

            for (int i = 1; i < count; ++i) {
                if (squares[row, column].Value.SquareState != SquareState.Default) {
                    break;
                }

                result.Add(squares[row, column].Value);
                row += deltarow;
                column += deltacolumn;
            }
            return result;
        }

        public void Eliminate(IEnumerable<Square> selected) {
            var ToEliminate = squareEliminator.ToEliminate(selected);

            foreach (Square square in ToEliminate) {
                squares[square.row, square.column] = null;
            }
        }

        private List<List<Square>> GetHorizontalPlacements(int length) {
            var result = new List<List<Square>>();

            for (int r = 0; r < rows; ++r) {
                LimitedQueue<Square> gathered = new LimitedQueue<Square>(length);

                for (int c = 0; c < colums; ++c) {
                    if (squares[r, c] != null && squares[r, c].Value.SquareState == SquareState.Default) {
                        gathered.Enqueue(squares[r, c].Value);
                    } else {
                        gathered.Clear();
                    }

                    if (gathered.Count == length) {
                        result.Add(new List<Square>(gathered.ToArray<Square>()));
                    }
                }
            }

            return result;
        }

        private List<List<Square>> GetVerticalPlacements(int length) {
            var result = new List<List<Square>>();

            for (int c = 0; c < colums; ++c) {
                LimitedQueue<Square> gathered = new LimitedQueue<Square>(length);

                for (int r = 0; r < rows; ++r) {
                    if (squares[r, c] != null && squares[r, c].Value.SquareState == SquareState.Default) {
                        gathered.Enqueue(squares[r, c].Value);
                    } else {
                        gathered.Clear();
                    }

                    if (gathered.Count == length) {
                        result.Add(new List<Square>(gathered.ToArray<Square>()));
                    }
                }
            }
            return result;
        }
    }
}