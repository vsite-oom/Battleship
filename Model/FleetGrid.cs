using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class FleetGrid
    {
        public FleetGrid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;

            // Rezerviranje prostora za dvodimenzionalno polje squares.
            squares = new Square[Rows, Columns];

            // Inicijalizacija pojedinih polja.
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    squares[r, c] = new Square(r, c);
                }
            }
        }

        public readonly int Rows;
        public readonly int Columns;

        // Dvodimenzionalno polje. Squarevi mogu biti null, to će nam
        // trebati kasnije da ih možemo eliminirati.
        public readonly Square?[,] squares;

        // Prema testu moramo imati svojstvo Squares koje će vratiti
        // jednodimenzionalnu kolekciju Squareova.
        // Budući da ne želimo otkrivati interne detalje implementacije
        // iskoristiti ćemo IEnumerable sučelje u kojem će biti
        // pojedinačni Squareovi i taj property će biti Squares.
        // To će nam biti getter.
        // Castamo dvodimenzionalni niz squares u jednodimenzionalni niz
        // Square, ali želimo dobiti samo squareove koji su trenutno u
        // mreži, ne želimo one koje su null reference.
        public IEnumerable<Square> Squares
        {
            get { return squares.Cast<Square>().Where(s => s != null); }
        }

        public IEnumerable<IEnumerable<Square>> GetAvailablePlacements(int length)
        {
            return GetHorizontalAvailablePlacements(length).Concat<IEnumerable<Square>>(GetVerticalAvailablePlacements(length));
        }

        private IEnumerable<IEnumerable<Square>> GetHorizontalAvailablePlacements(int length)
        {
            List<IEnumerable<Square>> result = new List<IEnumerable<Square>>();

            for (int r = 0; r < Rows; r++)  // Idemo po redovima.
            {
                var queue = new LimitedQueue<Square>(length);
                for (int c = 0; c < Columns; c++)  // U svakom redu idemo polje po polje (stupac po stupac).
                {
                    if (squares[r, c] != null)  // Ako je polje slobodno...
                    {
                        queue.Enqueue(squares[r, c]!);  // ...dodamo ga u red.
                        if (queue.Count() == length)  // Ako smo dosegli traženi broj polja u redu...
                        {
                            result.Add(queue.ToArray());  // ...kopiramo elemente reda u niz i dodamo u result.
                        }
                    }
                    else  // Ako smo naletili na eliminirano polje...
                    {
                        queue.Clear();  // ...ispraznimo red i krećemo iz početka.
                    }
                }
            }
            return result;
        }

        private IEnumerable<IEnumerable<Square>> GetVerticalAvailablePlacements(int length)
        {
            List<IEnumerable<Square>> result = new List<IEnumerable<Square>>();

            for (int c = 0; c < Columns; c++)  // Idemo po stupcima.
            {
                var queue = new LimitedQueue<Square>(length);
                for (int r = 0; r < Rows; r++)  // U svakom stupcu idemo polje po polje (red po red).
                {
                    if (squares[r, c] != null)  // Ako je polje slobodno...
                    {
                        queue.Enqueue(squares[r, c]!);  // ...dodamo ga u red.
                        if (queue.Count() == length)  // Ako smo dosegli traženi broj polja u redu...
                        {
                            result.Add(queue.ToArray());  // ...kopiramo elemente reda u niz i dodamo u result.
                        }
                    }
                    else  // Ako smo naletili na eliminirano polje...
                    {
                        queue.Clear();  // ...ispraznimo red i krećemo iz početka.
                    }
                }
            }
            return result;
        }

        public void EliminateSquare(int row, int column)  // Metoda za eliminiranje polja na koja smo postavili brod.
        {
            squares[row, column] = null;
        }
    }
}
