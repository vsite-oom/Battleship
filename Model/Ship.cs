using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public enum HitResult
    {
        Missed,
        Hit,
        Sunken
    }

    public class Ship
    {
        public Ship(IEnumerable<Square> squares)
        {
            this.squares = squares.ToArray();
        }

        public IEnumerable<Square> Squares
        {
            get { return squares; }
        }

        public HitResult Hit(Square square)
        {
            foreach(var a in squares)
            {
                if (a.Column == square.Column && a.Row == square.Row)
                {
                    int NumberOfHitSquares = 0;
                    foreach(var b in squares)
                    {
                        if (b.SquareState == SquareState.Hit)
                        {
                            ++NumberOfHitSquares;
                        }
                    }
                    if (NumberOfHitSquares == squares.Length - 1)
                    {
                        for (int i = 0; i < squares.Length; ++i)
                        {
                            this.squares[i].SetSquareState(HitResult.Sunken);
                        }
                        square.SetSquareState(HitResult.Sunken);
                        return HitResult.Sunken;
                    }
                    square.SetSquareState(HitResult.Hit);
                    for (int i = 0; i < squares.Length; ++i)
                    {
                        if (a.Column == this.squares[i].Column && a.Row == this.squares[i].Row)
                            this.squares[i].SetSquareState(HitResult.Hit);
                    }
                    return HitResult.Hit;
                }
            }
            square.SetSquareState(HitResult.Missed);
            for (int i = 0; i < squares.Length; ++i)
            {
                if (this.squares[i].SquareState == SquareState.Default)
                    this.squares[i].SetSquareState(HitResult.Missed);
            }
            return HitResult.Missed;


            // check if square belongs to this ship
            // if not: return HitResult.Missed
            // if yes:
            //      1. if all other squares are hit: return HitResult.Sunken
            //         and mark all squares sunken
            //      2. else, mark the square hit and return HitResult.Hit
        }

        private Square[] squares;
    }
}
