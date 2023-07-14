using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public interface IObserverFascadeBattleShip
    {
        public void Update();
    }
    public abstract class ObservableFascadeBattleShip
    {
        protected List<IObserverFascadeBattleShip> observerList { get; set; }
        protected ObservableFascadeBattleShip()
        {
            observerList=new List<IObserverFascadeBattleShip>();
        }
        public virtual void Add(IObserverFascadeBattleShip observer) {
            observerList.Add(observer); 
        }
        public virtual void Remove(IObserverFascadeBattleShip observer) { 
            observerList.Remove(observer);
        }
        public virtual void Notify() { 
            foreach(IObserverFascadeBattleShip observerFascadeBattleShip in observerList)
            {
                observerFascadeBattleShip.Update();
            }
        }
     }

    public class FascadeBattleShip:ObservableFascadeBattleShip
    {
        public FascadeBattleShip()
        {
            
        }

    }
}
