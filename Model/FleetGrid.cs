using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class FleetGrid : Grid
    {
        public FleetGrid(int rows, int columns) : base(rows, columns)
        {
        }

        // Prema testu moramo imati svojstvo Squares koje će vratiti
        // jednodimenzionalnu kolekciju Squareova.
        // Budući da ne želimo otkrivati interne detalje implementacije
        // iskoristiti ćemo IEnumerable sučelje u kojem će biti
        // pojedinačni Squareovi i taj property će biti Squares.
        // To će nam biti getter.
        // Castamo dvodimenzionalni niz squares u jednodimenzionalni niz
        // Square, ali želimo dobiti samo squareove koji su trenutno u
        // mreži, ne želimo one koje su null reference.
        public override IEnumerable<Square> Squares
        {
            get { return squares.Cast<Square>().Where(s => s != null); }
        }

        public void EliminateSquare(int row, int column)  // Metoda za eliminiranje polja na koja smo postavili brod.
        {
            squares[row, column] = null;
        }

        protected override bool IsSquareAvailable(int row, int column)
        {
            return squares[row, column] != null;
        }
    }
}
