using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Vsite.Oom.Battleship.Model;

namespace Battleships
{
    class PlayerManager
    {
        public PlayerManager(FleetControl PCFleetControl, FleetControl HumanFleetControl)
        {
            Shipwright shipwright = new Shipwright(10, 10, new List<int> { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 });
            PCFleet = shipwright.CreateFleet();
            PlayerFleet = shipwright.CreateFleet();
            PlayerFleetControl = HumanFleetControl;
            this.PCFleetControl = PCFleetControl;

            HumanPlayerNotificationQueue = new BlockingQueue<ShipButton>();
            HumanPlayer = new HumanPlayer(PCFleetControl, PCFleet, HumanPlayerNotificationQueue);
            PCPlayer = new PCPlayer(PlayerFleetControl, PlayerFleet);

            HumanPlayer.InvokePCPlayerTurn += 
                new EventHandler(delegate (object o, EventArgs e)
                {
                    PCPlayer.Play();
                });

            PCPlayer.InvokeHumanPlayerTurn +=
                new EventHandler(delegate (object o, EventArgs e)
                {
                    HumanPlayer.Play();
                });

            PlayerFleetControl.PlaceShips(PlayerFleet);
            HumanPlayer.Play();
        }

        public event EventHandler EndGameEvent
        {
            add
            {
                PCPlayer.EndGameEvent += value;
                HumanPlayer.EndGameEvent += value;
            }

            remove
            {
                PCPlayer.EndGameEvent -= value;
                HumanPlayer.EndGameEvent -= value;

            }
        }

        public void Cleanup()
        {
            HumanPlayerNotificationQueue.Close();
            PCPlayer.Stop();
            HumanPlayer.Stop();
        }

        public void EnqueuePlayerNotification(ShipButton shipButton)
        {
            HumanPlayerNotificationQueue.Enqueue(shipButton);
        }

        private BlockingQueue<ShipButton> HumanPlayerNotificationQueue;

        private HumanPlayer HumanPlayer;
        private PCPlayer PCPlayer;

        private Fleet PCFleet;
        private Fleet PlayerFleet;

        private FleetControl PCFleetControl;
        private FleetControl PlayerFleetControl;
    }
}
