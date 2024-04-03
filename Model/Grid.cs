using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class Grid
    {
        public Grid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;

            // Rezerviranje prostora.
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
            return GetHorizontalAvailablePlacements(length);
        }

        private IEnumerable<IEnumerable<Square>> GetHorizontalAvailablePlacements(int length)
        {
            List<IEnumerable<Square>> result = new List<IEnumerable<Square>>();

            for (int r = 0; r < Rows; r++)  // Idemo po redovima.
            {
                int counter = 0;  // Na pocetku reda imamo brojac kojeg postavljamo na 0.
                for (int c = 0; c < Columns; c++)  // U svakom redu idemo polje po polje (stupac po stupac).
                {
                    if (squares[r, c] != null)  // Ako je polje slobodno...
                    {
                        ++counter;  // ...povećamo brojač.
                        if (counter >= length)  // Ako je brojač veći ili jednak traženoj duljini...
                        {
                            List<Square> temp = new List<Square>();  // Lista koju popunimo poljima u koja stane brod.
                            for (int c1 = c - length + 1; c1 <= c; ++c1)
                            {
                                temp.Add(squares[r, c1]!);
                            }
                            result.Add(temp);  // Dodamo listu u rezultat.
                        }
                    }
                    else  // Ako smo naletili na eliminirano polje, brojač se vraća na 0.
                    {
                        counter = 0;
                    }
                }
            }
            return result;
        }
    }
}
