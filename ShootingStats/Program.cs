using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vsite.oom.Battleship.Model;

namespace ShootingStats
{
    class Program
    {
        static void Main(string[] args)
        {
            RulesSingleton rules = RulesSingleton.Instance;

            Shipwright sw = new Shipwright(10, 10);

            int hitNumber = 0;
            for (int i = 0; i < 10000; ++i)
            {
                Fleet fleet = fleet = sw.CreateFleet(new int[] { 2, 2, 2, 2, 3, 3, 3, 4, 4, 5 });
                Gunner gunner = new Gunner(10, 10, new int[] { 2, 2, 2, 2, 3, 3, 3, 4, 4, 5 });
                HitResult result;

                do
                {
                    Square target = gunner.NextTarget();
                    result = fleet.Hit(target);
                    gunner.ProcessHitResult(result);
                    hitNumber++;
                } while (result != HitResult.Hit);
            }
            Console.WriteLine(hitNumber / 10000.0);
            Console.ReadKey();

            hitNumber = 0;
            for (int i = 0; i < 10000; ++i)
            {
                Fleet fleet = fleet = sw.CreateFleet(new int[] { 1 });
                Gunner gunner = new Gunner(10, 10, new int[] { 1 });
                HitResult result;

                do
                {
                    Square target = gunner.NextTarget();
                    result = fleet.Hit(target);
                    gunner.ProcessHitResult(result);
                    hitNumber++;
                } while (result != HitResult.Sunken);
            }
            Console.WriteLine(hitNumber / 10000.0);
            Console.ReadKey();
        }
    }
}