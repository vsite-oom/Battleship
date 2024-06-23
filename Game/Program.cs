using System;
using System.Windows.Forms;

namespace Vsite.Oom.Battleship.Game
{
    internal static class Program
    {
        /// <summary>
        /// Glavna ulazna to�ka za aplikaciju.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}
