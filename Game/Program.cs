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
            // Da biste prilagodili konfiguraciju aplikacije kao �to su postavke visoke DPI rezolucije ili zadani font,
            // pogledajte https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Game());
        }
    }
}
