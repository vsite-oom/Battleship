using System;
using System.Linq;
using Vsite.Oom.Battleship.Model;

namespace TestShootingTactics
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter number of iterations for random shooting tactics test:");
            var s = Console.ReadLine();
            if (int.TryParse(s, out var n))
            {
                TestRandomShootingTactics(n);
            }
            else
            {
                Console.WriteLine("Wrong input.");
            }

            Console.ReadKey();
        }

        public static void TestRandomShootingTactics(int numberOfIterations)
        {
            Console.WriteLine($"Number of iterations: {numberOfIterations}");
            Console.WriteLine("_________________________________________________________________________");

            var shipwright = new Shipwright();
            var hitsBeforeShipIsHitSum = 0;
            for (int i = 0; i < numberOfIterations; i++)
            {
                var fleet = shipwright.CreateFleet();
                var gunner = new Gunner();
                var hitResult = ShipHitResult.Missed;

                int hitsBeforeShipIsHit = 0;
                while (hitResult == ShipHitResult.Missed)
                {
                    var target = gunner.NextTarget();
                    hitResult = fleet.Hit(target);
                    gunner.ProcessHitResult(hitResult);
                    ++hitsBeforeShipIsHit;
                }

                hitsBeforeShipIsHitSum += hitsBeforeShipIsHit;
            }

            var avarageHitsBeforeShipIsHit = hitsBeforeShipIsHitSum / (decimal)numberOfIterations;
            Console.WriteLine($"Avarage number of hits before first ship is hit with optimization: {avarageHitsBeforeShipIsHit}");

            var random = new Random();
            var hitsBeforeShipIsHitNotOptimizedSum = 0;
            for (int i = 0; i < numberOfIterations; i++)
            {
                var fleet = shipwright.CreateFleet();
                var grid = new Grid(10, 10);
                var hitResult = ShipHitResult.Missed;

                var hitsBeforeShipIsHit = 0;
                while (hitResult == ShipHitResult.Missed)
                {
                    var target = NextTargetRandomWithoutOptimization(grid, random);
                    hitResult = fleet.Hit(target);
                    grid.MarkHitResult(target, hitResult);
                    ++hitsBeforeShipIsHit;
                }

                hitsBeforeShipIsHitNotOptimizedSum += hitsBeforeShipIsHit;
            }

            var averageHitsBeforeShipIsHitNotOptimized = hitsBeforeShipIsHitNotOptimizedSum / (decimal)numberOfIterations;
            Console.WriteLine($"Avarage number of hits before first ship is hit without optimization: {averageHitsBeforeShipIsHitNotOptimized}");
        }

        public static Square NextTargetRandomWithoutOptimization(Grid evidenceGrid, Random random)
        {
            int shipLength = 1;
            var placements = evidenceGrid.GetAvailablePlacements(shipLength);
            var allCandidates = placements.SelectMany(seq => seq);
            var index = random.Next(0, allCandidates.Count());
            return allCandidates.ElementAt(index);
        }
    }
}
