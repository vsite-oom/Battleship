using Vsite.Oom.Battleship.Model;

namespace Battleship.GUI.Models
{
    public class GameModel
    {
        private readonly Vsite.Oom.Battleship.Model.FleetGrid gameGrid;
        private readonly int numberOfRows;
        private readonly int numberOfColumns;
        private readonly int difficulty;

        public GameModel (int rows, int columns, int difficulty = 0)
        {
            numberOfRows = rows;
            numberOfColumns = columns;
            gameGrid = new Vsite.Oom.Battleship.Model.FleetGrid (numberOfRows, numberOfColumns);
            this.difficulty = difficulty;
        }
    }
}
