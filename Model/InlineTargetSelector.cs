using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class InlineTargetSelector : ITargetSelector
    {
        public InlineTargetSelector(ShotsGrid grid, IEnumerable<Square> squaresHit, int shipLength)
        {
            this.grid = grid;
            this.squaresHit = squaresHit;
            this.shipLength = shipLength;
        }

        private readonly ShotsGrid grid;

        private readonly IEnumerable<Square> squaresHit;

        private readonly int shipLength;

        private readonly Random random = new Random();
        
        public Square Next()
        {
            var sorted = squaresHit.OrderBy(sq => sq.Row + sq.Column);  // Sortiramo po zbroju redka i stupca da dobijemo pravac u kojem se brod nalazi.
            var directionCandidates = new List<IEnumerable<Square>>();  // Kandidati za pravac u kojem se brod nalazi.

            // Horizontalno
            if (sorted.First().Row == sorted.Last().Row)  // Ako su prvo i zadnje polje na istom retku, brod je vodoravno.
            {
                var left = grid.GetSquaresInDirection(sorted.First().Row, sorted.First().Column, Direction.Leftwards);
                if (left.Any())
                {
                    directionCandidates.Add(left);  // Niz polja koja su lijevo od prvog polja.
                }
                var right = grid.GetSquaresInDirection(sorted.Last().Row, sorted.Last().Column, Direction.Rightwards);
                if (right.Any())
                {
                    directionCandidates.Add(right);  // Niz polja koja su desno od zadnjeg polja.
                }
            }
            // Vertikalno
            else
            {
                var up = grid.GetSquaresInDirection(sorted.First().Row, sorted.First().Column, Direction.Upwards);
                if (up.Any())
                {
                    directionCandidates.Add(up);  // Niz polja koja su iznad prvog polja.
                }
                var down = grid.GetSquaresInDirection(sorted.Last().Row, sorted.Last().Column, Direction.Downwards);
                if (down.Any())
                {
                    directionCandidates.Add(down);  // Niz polja koja su ispod zadnjeg polja.
                }
            }

            // Želimo dati prednost smjeru u kojem je najviše polja kanidata. 

            // Grupiramo nizove polja po duljini. Ako su dva niza iste duljine, idu u istu grupu.
            // Svaka grupa ima indeks (kriterij koji smo odabrali za grupiranje, u ovom slučaju
            // duljinu niza), te vrijednost (niz polja).
            var groupedByLength = directionCandidates.GroupBy(l => l.Count());

            // Sortiramo grupe po ključu (duljini niza), od najvećeg prema najmanjem.
            var sortedByLength = groupedByLength.OrderByDescending(g => g.Key);
            
            // Uzimamo prvu grupu, jer su grupe sortirane po duljini niza.
            // U toj grupi su najdulji nizovi.
            var longestDirections = sortedByLength.First();

            // Imamo jednog ili dva kandidata.
            var candidates = longestDirections.Count();
            
            // Ako imamo samo jednog kandidata, biramo njega i vraćamo prvo polje.
            if (candidates == 1)
            {
                return longestDirections.First().First();
            }
            
            // Ako imamo dva kandidata, biramo jednog nasumično.
            int selectedIndex = random.Next(candidates);

            // Vraćamo prvo polje odabranog kandidata.
            return longestDirections.ElementAt(selectedIndex).First();
        }
    }
}
