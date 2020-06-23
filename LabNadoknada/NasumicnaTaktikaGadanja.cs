using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vsite.Oom.Battleship.Model;

namespace Vsite.Oom.Battleship.Model
{
 public class NasumicnaTaktikaGadanja
    {
        Shipwright sw = new Shipwright();
        int brojPokusaja = 10000;
        public decimal PrviDio()
        {
            
            var missedHitsSum = 0;
            for (int i = 0; i < brojPokusaja; i++)
            {
                var fleet = sw.CreateFleet();
                var gunner = new Gunner();
                var hitResult = HitResult.Missed;

                int missedHitsNum = 0;
                while (hitResult == HitResult.Missed)
                {
                    var target = gunner.NextTarget();
                    hitResult = fleet.Hit(target);
                    gunner.ProcessHitResult(hitResult);
                    ++missedHitsNum;
                }

                missedHitsSum += missedHitsNum;
            }
            return AverageMissedHits(missedHitsSum, brojPokusaja);
        }

         public decimal DrugiDio()
        {
            var random = new Random();
            var missedHitsSum2 = 0;
            for (int i = 0; i < brojPokusaja; i++)
            {
                var fleet = sw.CreateFleet();
                var grid = new Grid(10, 10);
                var hitResult = HitResult.Missed;

                var missedHitsNum = 0;
                while (hitResult == HitResult.Missed)
                {
                    var target = PoljaSaJednakomVjerojatnoscu(grid, random);
                    hitResult = fleet.Hit(target);
                    grid.MarkHitResult(target, hitResult);
                    ++missedHitsNum;
                }

                missedHitsSum2 += missedHitsNum;
            }
            return AverageMissedHits(missedHitsSum2, brojPokusaja);
        }
        public Square PoljaSaJednakomVjerojatnoscu(Grid grid, Random random)
        {
            var availablePlacements = grid.GetAvailablePlacements(1);
            var allAvailablePlacements = availablePlacements.SelectMany(seq => seq);
            var index = random.Next(0, allAvailablePlacements.Count());
            
            return allAvailablePlacements.ElementAt(index);
        }
        static public decimal AverageMissedHits(int missedHitsSum, int brojPokusaja)
        {
            return missedHitsSum / (decimal)brojPokusaja;
        }

        public void Ispis(decimal avgMissedHits, int taktika)
        {
            Console.WriteLine($"Prosječan broj pokušaja do prvog pogotka ta taktiku {taktika}: {avgMissedHits}"); 
        }
    }
}
