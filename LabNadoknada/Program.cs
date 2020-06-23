using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vsite.Oom.Battleship.Model;

namespace LabNadoknada
{
    class Program
    {
        static void Main(string[] args)
        {
            NasumicnaTaktikaGadanja taktikaGadanja = new NasumicnaTaktikaGadanja();
            decimal prviDio = taktikaGadanja.PrviDio();
            decimal drugiDio = taktikaGadanja.DrugiDio();
            taktikaGadanja.Ispis(prviDio, 1);
            taktikaGadanja.Ispis(drugiDio, 2);
            
            Console.ReadLine();
        }
    }
}
