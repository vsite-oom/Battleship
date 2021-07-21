using System.Collections.Generic;
using System.Linq;

namespace Vsite.Oom.Battleship.Model {
    public enum HitResult {
        Missed,
        Hit,
        Sunken
    }

    public class Ship {
        private readonly Square[] squares;

        public Ship(IEnumerable<Square> squares) {
            this.squares = squares.ToArray();
        }

        public IEnumerable<Square> Squares {
            get { return squares; }
        }

        public HitResult Hit(Square square) {
            if (!squares.Contains(square)) {
                return HitResult.Missed;
            }

            for (int i = 0; i < squares.Length; ++i) {
                if (squares[i].Equals(square)) {
                    squares[i].SetSquareState(HitResult.Hit);
                }
            }

            if (squares.All(sq => sq.SquareState == SquareState.Hit)) {
                foreach (Square position in squares) {
                    position.SetSquareState(HitResult.Sunken);
                }
                return HitResult.Sunken;
            }
            return HitResult.Hit;
        }
    }
}