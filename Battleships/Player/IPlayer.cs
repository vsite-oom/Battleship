using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships
{
    public enum PlayerState
    {
        Waiting,
        Playing,
        Idle
    }

    interface IPlayer
    {
        void Wait();
        void Play();
    }
}
