using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalGUI
{
    enum Shooter
    {
        You,
        Computer
    }

    public class Game
    {
        public class MessageArgs : EventArgs
        {
            public string Message { get; set; }
        }

        public Game(GameForm form)
        {
            FirstShooterChosen += form.OnFirstShooterChosen;
        }
        public void Play()
        {
            Shooter shooter = WhoShootsFirst();
            

        }

        private Shooter WhoShootsFirst()
        {
            Random random = new Random();
            int i = random.Next(0, 2);
            Shooter shooter = (Shooter)i;
            OnFirstShooterChosen(shooter.ToString());
            return shooter;
        }

        public event EventHandler<MessageArgs> FirstShooterChosen;
        protected virtual void OnFirstShooterChosen(string message)
        {
            FirstShooterChosen?.Invoke(this, new MessageArgs() { Message = message });
        }
    }
}
