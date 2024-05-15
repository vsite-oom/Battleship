namespace Vsite.Oom.Battleship.Model;

public class ShotsGrid(int rows, int columns) : Grid(rows, columns)
{
    public override bool IsSquareAvailable(int row, int column)
    {
        throw new NotImplementedException();
    }
    
}