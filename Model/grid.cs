using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    using placement = IEnumerable<Square>;
    public enum Direction
    {
        Up,
        Right,
        Down,
        Left
    }
    public class Grid
    {
        public Grid(int rw, int cl)
        {
            Rw = rw;
            Cl = cl;
            squares = new Square[Rw,Cl];
            for (int r = 0; r < Rw; ++r)
            {
                for(int c = 0; c < Cl; ++c)
                {
                    squares[r, c] = new Square(r, c);
                }
            }
        }
        public IEnumerable<placement> GetAvailablePlacements(int len)
        {
            if (len != 1)
            {
                return GetAvailableHorizontalPlacements(len).Concat(GetAvailableVerticalPlacements(len));
            }
            List<List<Square>> res = new List<List<Square>>();
            for (int r = 0; r < Rw; ++r)
            {
                for (int c = 0; c < Cl; ++c)
                {
                    if (IsAvailable(r, c))
                    {
                        res.Add(new List<Square> { squares[r, c] });
                    }
                }
            }
            return res;
        }
        public void eliminateSquares(placement toElim)
        {
            foreach (var sq in toElim)
            {
                squares[sq.row, sq.column] = null;
            }
        }

        public void MarkHitResult(Square square,HitResult hitResult)
        {
            squares[square.row, square.column].SetState(hitResult);

        }
        public IEnumerable<Square> GetSquaresNextTo(Square square,Direction direction)
        {
            List<Square> result = new List<Square>();
            int row = square.row;
            int column = square.column;
            int deltaRow = 0;
            int deltaColumn = 0;
            int maxCount = 0;

            switch (direction)
            {
                case Direction.Right:
                    ++column;
                    ++deltaColumn;
                    maxCount = Cl - column;
                    break;
                case Direction.Down:
                    ++row;
                    ++deltaRow;
                    maxCount = Rw - row;
                    break;
               case Direction.Left:
                    maxCount = column;
                    --column;
                    --deltaColumn;
                    break;
                case Direction.Up:
                    maxCount = row;
                    --row;
                    --deltaRow;
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }
             for(int i = 0;i < maxCount && IsAvailable(row, column); ++i)
            {
                result.Add(squares[row, column]);
                row += deltaRow;
                column += deltaColumn;
            }
            return result;
        }
        public IEnumerable<IEnumerable<Square>> GetSquaresInline(IEnumerable<Square> squaresHit)
        {
            List<IEnumerable<Square>> result = new List<placement>();

            //for horizontal ship
            if (squaresHit.First().row == squaresHit.Last().row)
            {
                var l = GetSquaresNextTo(squaresHit.First(), Direction.Left);
                if (l.Count() > 0)
                    result.Add(l);

                l = GetSquaresNextTo(squaresHit.Last(), Direction.Right);
                if (l.Count() > 0)
                    result.Add(l);
            }
            //for vertical
            else if (squaresHit.First().column == squaresHit.Last().column)
            {
                var l = GetSquaresNextTo(squaresHit.First(), Direction.Up);
                if (l.Count() > 0)
                    result.Add(l);

                l = GetSquaresNextTo(squaresHit.Last(), Direction.Down);
                if (l.Count() > 0)
                    result.Add(l);
            }
            else
                Debug.Assert(false);

            return result;

        }
        private IEnumerable<placement> GetAvailableHorizontalPlacements(int len)
        {
            var res = new List<List<Square>>();
            for(int r = 0; r < Rw; ++r)
            {
                limitedQueue<Square> pass = new limitedQueue<Square>(len);
                for (int c = 0; c < Cl; ++c)
                {

                    if (IsAvailable(r, c))
                    {
                        pass.Enqueue(squares[r, c]);
                    }
                    else
                    {
                        pass.Clear();
                    }
                    if (pass.Count == len)
                    {
                        res.Add(pass.ToList());
                    }
                }
            }
            return res;
        }
        private IEnumerable<placement> GetAvailableVerticalPlacements(int len)
        {
            var res = new List<List<Square>>();
            for (int c = 0; c < Cl; ++c)
            {
                limitedQueue<Square> pass = new limitedQueue<Square>(len);
                for (int r = 0; r < Rw; ++r)
                {
                    if (IsAvailable(r, c))
                    {
                        pass.Enqueue(squares[r, c]);
                    }
                    else
                    {
                        pass.Clear();
                    }
                    if (pass.Count == len)
                    {
                        res.Add(pass.ToList());
                    }
                }
            }
            return res;
        }
        private bool IsAvailable(int row,int column)
        {
            return squares[row, column] != null && squares[row, column].SquareState == SquareState.None;
        }
        private Square[,] squares;
        public readonly int Rw;
        public readonly int Cl;
       

    }
}
