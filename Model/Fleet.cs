using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class Fleet
    {
        public List<Ship> ships=new List<Ship>();
        public IEnumerable<Ship> Ships => ships;

        public void CreateShip(IEnumerable<Square> squares)
        {

            ships.Add(new Ship(squares));
        }
        
        public HitResult Fire(Square target)
        {
            
            foreach (var ship in ships)
            {
                var result=ship.Fire(target);
                if(result!= HitResult.Missed)
                {
                    return result;
                }
            }
            return HitResult.Missed;
        }

    }
}
