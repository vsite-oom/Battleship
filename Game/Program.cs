using System;
using System.Windows.Forms;

namespace Vsite.Oom.Battleship.Game
{
    internal static class Program
    {
        /// <summary>
        /// Glavna ulazna toèka za aplikaciju.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}
